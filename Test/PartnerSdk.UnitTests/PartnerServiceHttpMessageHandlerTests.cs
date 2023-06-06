// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Moq;
    using Network;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the PartnerServiceHttpMessageHandler class.
    /// </summary>
    [TestClass]
    public sealed class PartnerServiceHttpMessageHandlerTests : IDisposable
    {
        /// <summary>
        /// The expected partner token.
        /// </summary>
        private const string ExpectedPartnerToken = "SomeToken";

        /// <summary>
        /// The expected accept.
        /// </summary>
        private const string ExpectedAccept = "application/json";

        /// <summary>
        /// The expected content type.
        /// </summary>
        private const string ExpectedContentType = "application/json";

        /// <summary>
        /// The expected if match.
        /// </summary>
        private const string ExpectedIfMatch = "\"123456789\"";

        /// <summary>
        /// The expected request id.
        /// </summary>
        private readonly Guid expectedRequestId = Guid.NewGuid();

        /// <summary>
        /// The expected correlation id.
        /// </summary>
        private readonly Guid expectedCorrelationId = Guid.NewGuid();

        /// <summary>
        /// The expected locale.
        /// </summary>
        private readonly string expectedLocale = "ar-SA";

        /// <summary>
        /// A mock partner service proxy.
        /// </summary>
        private readonly Mock<IPartnerServiceProxy<string, string>> mockPartnerServiceProxy = new Mock<IPartnerServiceProxy<string, string>>();

        /// <summary>
        /// A partner service HTTP message handler.
        /// </summary>
        private PartnerServiceHttpMessageHandler<string, string> messageHandler;

        /// <summary>
        /// The HTTP request.
        /// </summary>
        private HttpRequestMessage request;

        /// <summary>
        /// Disposed of the object.
        /// </summary>
        public void Dispose()
        {
            this.request.Dispose();
            this.messageHandler.Dispose();
        }

        /// <summary>
        /// Initializes the stage for the tests.
        /// </summary>
        [TestInitialize]
        public void PrepareTests()
        {
            this.request = new HttpRequestMessage();

            // setup the mock partner service proxy
            this.mockPartnerServiceProxy.Setup(proxy => proxy.Partner.Credentials.PartnerServiceToken).Returns(ExpectedPartnerToken);
            this.mockPartnerServiceProxy.Setup(proxy => proxy.Accept).Returns(ExpectedAccept);
            this.mockPartnerServiceProxy.Setup(proxy => proxy.RequestId).Returns(this.expectedRequestId);
            this.mockPartnerServiceProxy.Setup(proxy => proxy.CorrelationId).Returns(this.expectedCorrelationId);
            this.mockPartnerServiceProxy.Setup(proxy => proxy.Locale).Returns(this.expectedLocale);
            this.mockPartnerServiceProxy.Setup(proxy => proxy.ContentType).Returns(ExpectedContentType);
            this.mockPartnerServiceProxy.Setup(proxy => proxy.IfMatch).Returns(string.Empty);

            // setup the HTTP message handler
            this.messageHandler = new PartnerServiceHttpMessageHandler<string, string>(this.mockPartnerServiceProxy.Object);
        }

        /// <summary>
        /// Ensures the required request headers are added to the HTTP request.
        /// </summary>
        [TestMethod]
        public void PartnerServiceHttpMessageHandlerTests_VerifyRequestHeaders()
        {
            this.messageHandler.InjectHeaders(this.request);

            this.EnsureRequiredHeadersAreSet();
            Assert.AreEqual(this.request.Headers.IfMatch.Count, 0);
        }

        /// <summary>
        /// Ensures the required request headers including the content are added to the HTTP request.
        /// </summary>
        [TestMethod]
        public void PartnerServiceHttpMessageHandlerTests_VerifyRequestHeadersWithContent()
        {
            // set some content in the request
            this.request.Content = new StringContent("Some Content");

            // act
            this.messageHandler.InjectHeaders(this.request);

            // ensure the standard headers and the content type are set and are unalterted from the request.
            this.EnsureRequiredHeadersAreSet();
            Assert.IsNotNull(this.request.Content.Headers.ContentType);
            Assert.AreEqual(ExpectedContentType, this.request.Content.Headers.ContentType.MediaType);
        }

        /// <summary>
        /// Ensures the required request headers including the if match are added to the HTTP request.
        /// </summary>
        [TestMethod]
        public void PartnerServiceHttpMessageHandlerTests_VerifyRequestHeadersWithIfMatch()
        {
            // let the mock proxy return an if match
            this.mockPartnerServiceProxy.Setup(proxy => proxy.IfMatch).Returns(ExpectedIfMatch);

            // act
            this.messageHandler.InjectHeaders(this.request);

            // ensure the standard headers and the if match header are set
            this.EnsureRequiredHeadersAreSet();

            var enumerator = this.request.Headers.IfMatch.GetEnumerator();
            enumerator.MoveNext();

            Assert.AreEqual(enumerator.Current.Tag, ExpectedIfMatch);
        }

        /// <summary>
        /// Ensures the required headers are set to the expected values.
        /// </summary>
        private void EnsureRequiredHeadersAreSet()
        {
            Assert.IsNotNull(this.request.Headers.Authorization);
            Assert.AreEqual(this.request.Headers.Authorization.Scheme, "Bearer");
            Assert.AreEqual(this.request.Headers.Authorization.Parameter, ExpectedPartnerToken);

            Assert.IsNotNull(this.request.Headers.Accept);
            Assert.IsTrue(this.request.Headers.Accept.Contains(new MediaTypeWithQualityHeaderValue(ExpectedAccept)));

            Assert.IsTrue(this.request.Headers.Contains("MS-RequestId"));
            var requestId = this.request.Headers.GetValues("MS-RequestId").FirstOrDefault();
            Assert.AreEqual(this.expectedRequestId.ToString(), requestId);

            Assert.IsTrue(this.request.Headers.Contains("MS-CorrelationId"));
            var correlationId = this.request.Headers.GetValues("MS-CorrelationId").FirstOrDefault();
            Assert.AreEqual(this.expectedCorrelationId.ToString(), correlationId);

            Assert.IsTrue(this.request.Headers.Contains("X-Locale"));
            var locale = this.request.Headers.GetValues("X-Locale").FirstOrDefault();
            Assert.AreEqual(this.expectedLocale, locale);

            Assert.IsTrue(this.request.Headers.Contains("MS-PartnerCenter-Client"));
            var partnerCenterClient = this.request.Headers.GetValues("MS-PartnerCenter-Client").FirstOrDefault();
            Assert.AreEqual(PartnerService.Instance.Configuration.PartnerCenterClient, partnerCenterClient);
        }
    }
}
