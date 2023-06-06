// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ErrorHandling;
    using Exceptions;
    using Newtonsoft.Json.Linq;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="DefaultPartnerServiceErrorHandler"/> handler.
    /// </summary>
    [TestClass]
    public class DefaultPartnerServiceErrorHandlerTests
    {
        /// <summary>
        /// The expected context.
        /// </summary>
        private static IRequestContext expectedContext;

        /// <summary>
        /// The error handler we will test.
        /// </summary>
        private readonly DefaultPartnerServiceErrorHandler errorHandler = new DefaultPartnerServiceErrorHandler();

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
            DefaultPartnerServiceErrorHandlerTests.expectedContext = RequestContextFactory.Instance.Create(Guid.NewGuid(), Guid.NewGuid(), "de-DE");
        }
        
        /// <summary>
        /// Verifies that when the error handler receives a response with a body set to an APIFault
        /// object, it would actually expose that object in the Partner exception it generates and it would
        /// propagate the context object properly.
        /// </summary>
        /// <returns>The test task.</returns>
        [TestMethod]
        public async Task DefaultPartnerServiceErrorHandlerTests_VerifyResponseWithApiFaultObject()
        {
            // Arrange
            JObject testErrorPayload = new JObject();
            testErrorPayload.Add("code", "2000");
            testErrorPayload.Add("description", "Customer search only allows starts with and equals operation.");
            testErrorPayload.Add("data", new JArray("Field1", "Field2"));
            testErrorPayload.Add("source", "PartnerApiServiceControllers");
            
            HttpResponseMessage cannedHttpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(testErrorPayload.ToString())
            };

            // Act
            var generatedException = await this.errorHandler.HandleFailedResponse(cannedHttpResponse, DefaultPartnerServiceErrorHandlerTests.expectedContext);

            // Assert
            Assert.IsNotNull(generatedException);
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.BadInput);
            Assert.IsNotNull(generatedException.ServiceErrorPayload);
            Assert.AreEqual(generatedException.ServiceErrorPayload.ErrorCode, testErrorPayload["code"].ToString());
            Assert.AreEqual(generatedException.ServiceErrorPayload.ErrorMessage, testErrorPayload["description"].ToString());

            var cannedErrorData = new List<object>(testErrorPayload["data"].Values<string>());
            var actualErrorData = new List<object>(generatedException.ServiceErrorPayload.ErrorData);

            Assert.AreEqual(cannedErrorData.Count, actualErrorData.Count);

            for (int i = 0; i < cannedErrorData.Count; ++i)
            {
                Assert.AreEqual(cannedErrorData[i], actualErrorData[i]);
            }

            Assert.AreEqual(generatedException.Context.CorrelationId, DefaultPartnerServiceErrorHandlerTests.expectedContext.CorrelationId);
            Assert.AreEqual(generatedException.Context.RequestId, DefaultPartnerServiceErrorHandlerTests.expectedContext.RequestId);
            Assert.AreEqual(generatedException.Context.Locale, DefaultPartnerServiceErrorHandlerTests.expectedContext.Locale);
        }

        /// <summary>
        /// Verifies that the error handler sets the exception message to the response if the response
        /// could not be parsed into an <see cref="ApiFault"/> object and ensures that the context is properly passed.
        /// </summary>
        /// <returns>The test task.</returns>
        [TestMethod]
        public async Task DefaultPartnerServiceErrorHandlerTests_VerifyHttpResponse()
        {
            // Arrange
            string sampleResponseContent = "Some non-structured content.";

            HttpResponseMessage cannedHttpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(sampleResponseContent)
            };

            // Act
            var generatedException = await this.errorHandler.HandleFailedResponse(cannedHttpResponse, DefaultPartnerServiceErrorHandlerTests.expectedContext);

            // Assert
            Assert.IsNotNull(generatedException);
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.BadInput);
            Assert.IsNull(generatedException.ServiceErrorPayload);
            Assert.AreEqual(sampleResponseContent, generatedException.Message);

            Assert.AreEqual(generatedException.Context.CorrelationId, DefaultPartnerServiceErrorHandlerTests.expectedContext.CorrelationId);
            Assert.AreEqual(generatedException.Context.RequestId, DefaultPartnerServiceErrorHandlerTests.expectedContext.RequestId);
            Assert.AreEqual(generatedException.Context.Locale, DefaultPartnerServiceErrorHandlerTests.expectedContext.Locale);
        }

        /// <summary>
        /// Verify that the error categories are properly set for HTTP response codes.
        /// </summary>
        /// <returns>The test task.</returns>
        [TestMethod]
        public async Task DefaultPartnerServiceErrorHandlerTests_VerifyErrorCategories()
        {
            var generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.BadRequest));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.BadInput);

            generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.Unauthorized);

            generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.Forbidden));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.Forbidden);

            generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.NotFound));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.NotFound);

            generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.Conflict));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.AlreadyExists);

            generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.ServerError);

            generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.RequestUriTooLong));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.ServerError);

            generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.ServerError);

            generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.ServerBusy);

            generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.RequestTimeout));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.RequestTimeout);

            generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.GatewayTimeout));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.GatewayTimeout);
        }

        /// <summary>
        /// Ensures that passing a successful response to the error handler throws an argument exception.
        /// </summary>
        /// <returns>The test task.</returns>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task DefaultPartnerServiceErrorHandlerTests_VerifySuccessfulResponseHandling()
        {
            var generatedException = await this.errorHandler.HandleFailedResponse(new HttpResponseMessage(HttpStatusCode.OK));
            Assert.AreEqual(generatedException.ErrorCategory, PartnerErrorCategory.ServerError);
        }
    }
}
