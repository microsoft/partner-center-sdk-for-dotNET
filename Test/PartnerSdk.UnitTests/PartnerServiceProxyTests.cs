// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using ErrorHandling;
    using Exceptions;
    using Models.Subscriptions;
    using Moq;
    using Moq.Protected;
    using Network;
    using Newtonsoft.Json;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the ServiceProxy class.
    /// </summary>
    [TestClass]
    public class PartnerServiceProxyTests
    {
        /// <summary>
        /// The expected context.
        /// </summary>
        private static IRequestContext expectedContext;

        /// <summary>
        /// A preset success JSON response.
        /// </summary>
        private static string expectedSuccessJsonHttpResponse;

        /// <summary>
        /// A preset success string response.
        /// </summary>
        private static string expectedSuccessStringHttpResponse;

        /// <summary>
        /// A preset subscription.
        /// </summary>
        private static Subscription expectedSubscription;

        /// <summary>
        /// Mocks partner credentials.
        /// </summary>
        private readonly Mock<IPartnerCredentials> credentialsMock = new Mock<IPartnerCredentials>();

        /// <summary>
        /// Mocks the HTTP client used to perform HTTP requests.
        /// </summary>
        private readonly Mock<HttpMessageHandler> httpClientMock = new Mock<HttpMessageHandler>();

        /// <summary>
        /// Mocks the JSON partner service proxy.
        /// </summary>
        private Mock<PartnerServiceProxy<string, Subscription>> proxyMock;

        /// <summary>
        /// Tracks the number of times a refresh credentials handler was invoked.
        /// </summary>
        private int refreshCredentialsCallBackInvokationCount;

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
            expectedSubscription = new Subscription()
            {
                Id = "1",
                Quantity = 7,
                FriendlyName = "Sample Subscription"
            };

            expectedSuccessJsonHttpResponse = JsonConvert.SerializeObject(expectedSubscription);
            expectedSuccessStringHttpResponse = "Some non-structured text";
            expectedContext = RequestContextFactory.Instance.Create(Guid.NewGuid(), Guid.NewGuid(), "ru-RU");
        }

        /// <summary>
        /// Initializes the tests.
        /// </summary>
        [TestInitialize]
        public void PrepareTests()
        {
            this.refreshCredentialsCallBackInvokationCount = 0;

            Mock<IPartner> partnerOperations = new Mock<IPartner>();
            partnerOperations.Setup(partner => partner.Credentials).Returns(this.credentialsMock.Object);
            partnerOperations.Setup(partner => partner.RequestContext).Returns(expectedContext);

            this.proxyMock = new Mock<PartnerServiceProxy<string, Subscription>>(partnerOperations.Object, string.Empty, null, null);
            this.ResetMockProxyHttpClient();

            this.SetCredentialsExpired(false);
        }

        /// <summary>
        /// Ensures that the proxy passes requests with non expired tokens.
        /// </summary>
        /// <returns>A task.</returns>
        [TestMethod]
        public async Task PartnerServiceProxyTests_VerifyWithNonExpiredToken()
        {
            // let the mock HTTP client return a successful response
            this.MockHttpResponse(HttpStatusCode.OK, expectedSuccessJsonHttpResponse);

            // verify that the GET, POST and PATCH requests went all the way through
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.GetAsync());
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.PostAsync(string.Empty));
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.PatchAsync(string.Empty));
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.PutAsync(string.Empty));
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.DeleteAsync());
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.HeadAsync());
        }

        /// <summary>
        /// Ensures that the proxy throws a <see cref="PartnerException"/> when the credentials
        /// are expired and no refresh handler was set.
        /// </summary>
        /// <returns>A task.</returns>
        [TestMethod]
        public async Task PartnerServiceProxyTests_VerifyExpiredTokenWithNoRefreshHandler()
        {
            // simulate expired credentials
            this.SetCredentialsExpired(true);

            // deregesiter any refresh handlers
            PartnerService.Instance.RefreshCredentials = null;

            // execute all types of operations and expect them to fail
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.GetAsync(), PartnerErrorCategory.Unauthorized, "Proxy.GetAsync() should have failed on expired token with no refresh handler");
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PostAsync(string.Empty), PartnerErrorCategory.Unauthorized, "Proxy.PostAsync() should have failed on expired token with no refresh handler");
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PatchAsync(string.Empty), PartnerErrorCategory.Unauthorized, "Proxy.PatchAsync() should have failed on expired token with no refresh handler");
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PutAsync(string.Empty), PartnerErrorCategory.Unauthorized, "Proxy.PutAsync() should have failed on expired token with no refresh handler");
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.DeleteAsync(), PartnerErrorCategory.Unauthorized, "Proxy.DeleteAsync() should have failed on expired token with no refresh handler");
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.HeadAsync(), PartnerErrorCategory.Unauthorized, "Proxy.HeadAsync() should have failed on expired token with no refresh handler");
        }

        /// <summary>
        /// Tests the scenario of successfully refreshing expired credentials.
        /// </summary>
        /// <returns>A task.</returns>
        [TestMethod]
        public async Task PartnerServiceProxyTests_VerifyExpiredTokenWithRefreshHandlerOk()
        {
            // simulate a successful response
            this.MockHttpResponse(HttpStatusCode.OK, expectedSuccessJsonHttpResponse);

            // register a valid refresh handler
            PartnerService.Instance.RefreshCredentials += this.RefreshCrednetialsOk;

            // verify that the requests went all the way through and that the refresh callback was invoked for each
            this.SetCredentialsExpired(true);
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.GetAsync(), 1);

            this.SetCredentialsExpired(true);
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.PostAsync(string.Empty), 2);

            this.SetCredentialsExpired(true);
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.PatchAsync(string.Empty), 3);

            this.SetCredentialsExpired(true);
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.PutAsync(string.Empty), 4);

            this.SetCredentialsExpired(true);
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.DeleteAsync(), 5);

            this.SetCredentialsExpired(true);
            await this.EnsureProxyOperationSucceeds(() => this.proxyMock.Object.HeadAsync(), 6);
        }

        /// <summary>
        /// Tests the scenario where the refresh handler crashes and fails to update an expired token.
        /// </summary>
        /// <returns>A task which completes when the test is done.</returns>
        [TestMethod]
        public async Task PartnerServiceProxyTests_VerifyExpiredTokenWithRefreshHandlerCrashes()
        {
            // simulate expired credentials
            this.SetCredentialsExpired(true);

            // simulate a successful response
            this.MockHttpResponse(HttpStatusCode.OK, expectedSuccessJsonHttpResponse);

            // register a refresh handler which will crash
            PartnerService.Instance.RefreshCredentials += this.RefreshCrednetialsCrashes;

            // execute all operations and make sure they fail
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.GetAsync(), PartnerErrorCategory.Unauthorized, "Proxy.GetAsync() should have failed on expired token with crashing refresh handler", 1);
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PostAsync(string.Empty), PartnerErrorCategory.Unauthorized, "Proxy.PostAsync() should have failed on expired token with crashing refresh handler", 2);
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PatchAsync(string.Empty), PartnerErrorCategory.Unauthorized, "Proxy.PatchAsync() should have failed on expired token with crashing refresh handler", 3);
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PutAsync(string.Empty), PartnerErrorCategory.Unauthorized, "Proxy.PutAsync() should have failed on expired token with crashing refresh handler", 4);
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.DeleteAsync(), PartnerErrorCategory.Unauthorized, "Proxy.DeleteAsync() should have failed on expired token with crashing refresh handler", 5);
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.HeadAsync(), PartnerErrorCategory.Unauthorized, "Proxy.HeadAsync() should have failed on expired token with crashing refresh handler", 6);
        }

        /// <summary>
        /// Tests the scenario where a refresh handler returns an expired token.
        /// </summary>
        /// <returns>A task which completes when the test is done.</returns>
        [TestMethod]
        public async Task PartnerServiceProxyTests_VerifyExpiredTokenWithRefreshHandlerReturnsExpiredToken()
        {
            // simulate expired credentials
            this.SetCredentialsExpired(true);

            // simulate a successful response
            this.MockHttpResponse(HttpStatusCode.OK, expectedSuccessJsonHttpResponse);

            // register a refresh handler which will not update the token
            PartnerService.Instance.RefreshCredentials += this.RefreshCrednetialsReturnsExpiredToken;

            // execute all operations and make sure they fail
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.GetAsync(), PartnerErrorCategory.Unauthorized, "Proxy.GetAsync() should have failed on expired token", 1);
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PostAsync(string.Empty), PartnerErrorCategory.Unauthorized, "Proxy.PostAsync() should have failed on expired token", 2);
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PatchAsync(string.Empty), PartnerErrorCategory.Unauthorized, "Proxy.PatchAsync() should have failed on expired token", 3);
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PutAsync(string.Empty), PartnerErrorCategory.Unauthorized, "Proxy.PutAsync() should have failed on expired token", 4);
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.DeleteAsync(), PartnerErrorCategory.Unauthorized, "Proxy.DeleteAsync() should have failed on expired token", 5);
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.HeadAsync(), PartnerErrorCategory.Unauthorized, "Proxy.HeadAsync() should have failed on expired token", 6);
        }

        /// <summary>
        /// Verifies that the proxy throws the correct exception type when it receives a successful
        /// response from the service but can't deserialize it.
        /// </summary>
        /// <returns>A task which completes when the test is done.</returns>
        [TestMethod]
        public async Task PartnerServiceProxyTests_VerifySuccessfulHttpResponseWithInvalidJson()
        {
            // let the mock HTTP client return a successful non Json response
            this.MockHttpResponse(HttpStatusCode.OK, expectedSuccessStringHttpResponse);

            // execute all operations and make sure they fail with response parsing error category
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.GetAsync(), PartnerErrorCategory.ResponseParsing, "Proxy.GetAsync() should have failed on with ResponseParsing error");
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PostAsync(string.Empty), PartnerErrorCategory.ResponseParsing, "Proxy.PostAsync() should have failed on with ResponseParsing error");
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PatchAsync(string.Empty), PartnerErrorCategory.ResponseParsing, "Proxy.PatchAsync() should have failed on with ResponseParsing error");
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.PutAsync(string.Empty), PartnerErrorCategory.ResponseParsing, "Proxy.PutAsync() should have failed on with ResponseParsing error");
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.DeleteAsync(), PartnerErrorCategory.ResponseParsing, "Proxy.DeleteAsync() should have failed on with ResponseParsing error");
            await this.EnsureProxyOperationFails(() => this.proxyMock.Object.HeadAsync(), PartnerErrorCategory.ResponseParsing, "Proxy.HeadAsync() should have failed on with ResponseParsing error");
        }

        /// <summary>
        /// Verifies that updating the request and correlation id works as expected.
        /// </summary>
        /// <returns>A task which completes when the test is done.</returns>
        [TestMethod]
        public async Task PartnerServiceProxyTests_VerifyRequestAndCorrelationUpdatesWork()
        {
            // ensure that reading the ids work
            Assert.AreEqual(this.proxyMock.Object.CorrelationId, expectedContext.CorrelationId);
            Assert.AreEqual(this.proxyMock.Object.RequestId, expectedContext.RequestId);

            // update the ids
            Guid updatedCorrelationId = Guid.NewGuid();
            Guid updatedRequestId = Guid.NewGuid();

            this.proxyMock.Object.CorrelationId = updatedCorrelationId;
            this.proxyMock.Object.RequestId = updatedRequestId;

            // ensure they are set
            Assert.AreEqual(this.proxyMock.Object.CorrelationId, updatedCorrelationId);
            Assert.AreEqual(this.proxyMock.Object.RequestId, updatedRequestId);

            // simulate a failed operation
            this.MockHttpResponse(HttpStatusCode.BadRequest, string.Empty);

            try
            {
                this.ResetMockProxyHttpClient();
                await this.proxyMock.Object.GetAsync();
                Assert.Fail("The operation should have failed.");
            }
            catch (PartnerException expectedException)
            {
                // ensure the updated values were reported in the exception
                Assert.AreEqual(expectedException.Context.CorrelationId, updatedCorrelationId);
                Assert.AreEqual(expectedException.Context.RequestId, updatedRequestId);
            }
        }

        /// <summary>
        /// Verifies failed service response scenarios.
        /// </summary>
        /// <returns>A task which completes when the test is done.</returns>
        [TestMethod]
        public async Task PartnerServiceProxyTests_VerifyFailedHttpResponseScenarios()
        {
            // mock the error handler used by the proxy
            var errorHandlerMock = new Mock<IFailedPartnerServiceResponseHandler>();
            this.proxyMock.Protected().Setup<IFailedPartnerServiceResponseHandler>("ErrorHandler").Returns(errorHandlerMock.Object);

            await this.EnsureProxyOperationFailureIsProperlyHandled(
                () =>
                {
                    // let the mock HTTP client return forbidden (403) error
                    this.MockHttpResponse(HttpStatusCode.Forbidden, string.Empty);

                    // execute a GET request
                    return this.proxyMock.Object.GetAsync();
                },
                errorHandlerMock,
                1);

            await this.EnsureProxyOperationFailureIsProperlyHandled(
                () =>
                {
                    // let the mock HTTP client return conflict (409) error
                    this.MockHttpResponse(HttpStatusCode.Conflict, string.Empty);

                    // execute a POST request
                    return this.proxyMock.Object.PostAsync(string.Empty);
                },
                errorHandlerMock,
                2);

            await this.EnsureProxyOperationFailureIsProperlyHandled(
                () =>
                {
                    // let the mock HTTP client return service unavailable (503) error
                    this.MockHttpResponse(HttpStatusCode.Conflict, string.Empty);

                    // execute a PATCH request
                    return this.proxyMock.Object.PatchAsync(string.Empty);
                },
                errorHandlerMock,
                3);

            await this.EnsureProxyOperationFailureIsProperlyHandled(
                () =>
                {
                    // let the mock HTTP client return service unavailable (503) error
                    this.MockHttpResponse(HttpStatusCode.Conflict, string.Empty);

                    // execute a PUT request
                    return this.proxyMock.Object.PutAsync(string.Empty);
                },
                errorHandlerMock,
                4);

            await this.EnsureProxyOperationFailureIsProperlyHandled(
                () =>
                {
                    // let the mock HTTP client return service unavailable (503) error
                    this.MockHttpResponse(HttpStatusCode.Conflict, string.Empty);

                    // execute a DELETE request
                    return this.proxyMock.Object.DeleteAsync();
                },
                errorHandlerMock,
                5);

            await this.EnsureProxyOperationFailureIsProperlyHandled(
                () =>
                {
                    // let the mock HTTP client return service unavailable (503) error
                    this.MockHttpResponse(HttpStatusCode.Conflict, string.Empty);

                    // execute a HEAD request
                    return this.proxyMock.Object.HeadAsync();
                },
                errorHandlerMock,
                6);
        }

        /// <summary>
        /// Validates that the subscriptions match.
        /// </summary>
        /// <param name="expected">The expected.</param>
        /// <param name="actual">The actual.</param>
        private static void EnsureSubscriptionsAreEqual(Subscription expected, Subscription actual)
        {
            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.Quantity, expected.Quantity);
            Assert.AreEqual(actual.FriendlyName, expected.FriendlyName);
        }

        /// <summary>
        /// Sets the partner credentials to expired or non expired.
        /// </summary>
        /// <param name="expired">True to expire, false to make active.</param>
        private void SetCredentialsExpired(bool expired)
        {
            this.credentialsMock.Setup(credentials => credentials.IsExpired()).Returns(expired);
        }

        /// <summary>
        /// This provides a new HTTP client mock object if more than one requests will execute in the same test. This avoids disposing
        /// the previous HTTP client mock object.
        /// </summary>
        private void ResetMockProxyHttpClient()
        {
            this.proxyMock.Protected().Setup<HttpClient>("BuildHttpClient").Returns(new HttpClient(this.httpClientMock.Object));
            this.proxyMock.Protected()
                .Setup<Uri>("BuildPartnerServiceApiUri")
                .Returns(new Uri(PartnerService.Instance.ApiRootUrl));
        }

        /// <summary>
        /// Executes a proxy operation and ensures it returns the expected subscription result.
        /// </summary>
        /// <param name="proxyOperation">The proxy operation to execute.</param>
        /// <param name="expectedRefreshInvocations">The number of refresh credentials invocations expected.</param>
        /// <returns>A task.</returns>
        private async Task EnsureProxyOperationSucceeds(Func<Task<Subscription>> proxyOperation, int expectedRefreshInvocations = 0)
        {
            this.ResetMockProxyHttpClient();
            EnsureSubscriptionsAreEqual(expectedSubscription, await proxyOperation.Invoke());
            Assert.AreEqual(this.refreshCredentialsCallBackInvokationCount, expectedRefreshInvocations);
        }

        /// <summary>
        /// Executes a proxy operation and ensures it returns the expected subscription result.
        /// </summary>
        /// <param name="proxyOperation">The proxy operation to execute.</param>
        /// <param name="expectedRefreshInvocations">The number of refresh credentials invocations expected.</param>
        /// <returns>A task.</returns>
        private async Task EnsureProxyOperationSucceeds(Func<Task> proxyOperation, int expectedRefreshInvocations = 0)
        {
            this.ResetMockProxyHttpClient();
            await proxyOperation.Invoke();
            Assert.AreEqual(this.refreshCredentialsCallBackInvokationCount, expectedRefreshInvocations);
        }

        /// <summary>
        /// Executes the given proxy operation and ensures it fails with the expected error category and that the refresh credentials callback has been
        /// invoked the expected number of times.
        /// </summary>
        /// <param name="proxyOperation">The proxy operation to execute.</param>
        /// <param name="expectedErrorCategory">The expected error category.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <param name="expectedRefreshInvokations">The number of refresh credentials invocations expected.</param>
        /// <returns>A task.</returns>
        private async Task EnsureProxyOperationFails(Func<Task<Subscription>> proxyOperation, PartnerErrorCategory expectedErrorCategory, string failureMessage, int expectedRefreshInvokations = 0)
        {
            try
            {
                this.ResetMockProxyHttpClient();
                await proxyOperation.Invoke();
                Assert.Fail(failureMessage);
            }
            catch (PartnerException expectedException)
            {
                Assert.AreEqual(expectedException.ErrorCategory, expectedErrorCategory);
                Assert.AreEqual(this.refreshCredentialsCallBackInvokationCount, expectedRefreshInvokations);
                Assert.AreEqual(expectedException.Context.CorrelationId, expectedContext.CorrelationId);
                Assert.AreEqual(expectedException.Context.RequestId, expectedContext.RequestId);
            }
        }

        /// <summary>
        /// Executes the given proxy operation and ensures it fails with the expected error category and that the refresh credentials callback has been
        /// invoked the expected number of times.
        /// </summary>
        /// <param name="proxyOperation">The proxy operation to execute.</param>
        /// <param name="expectedErrorCategory">The expected error category.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <param name="expectedRefreshInvokations">The number of refresh credentials invocations expected.</param>
        /// <returns>A task.</returns>
        private async Task EnsureProxyOperationFails(Func<Task> proxyOperation, PartnerErrorCategory expectedErrorCategory, string failureMessage, int expectedRefreshInvokations = 0)
        {
            try
            {
                this.ResetMockProxyHttpClient();
                await proxyOperation.Invoke();
                Assert.Fail(failureMessage);
            }
            catch (PartnerException expectedException)
            {
                Assert.AreEqual(expectedException.ErrorCategory, expectedErrorCategory);
                Assert.AreEqual(this.refreshCredentialsCallBackInvokationCount, expectedRefreshInvokations);
                Assert.AreEqual(expectedException.Context.CorrelationId, expectedContext.CorrelationId);
                Assert.AreEqual(expectedException.Context.RequestId, expectedContext.RequestId);
            }
        }

        /// <summary>
        /// Executes the given proxy operation and ensures that the error handler has been invoked.
        /// </summary>
        /// <param name="proxyOperation">The proxy operation to execute.</param>
        /// <param name="errorHandlerMock">The error handler mock object.</param>
        /// <param name="errorHandlerInvocations">The number of error handling invocations expected.</param>
        /// <returns>A task.</returns>
        private async Task EnsureProxyOperationFailureIsProperlyHandled(Func<Task<Subscription>> proxyOperation, Mock<IFailedPartnerServiceResponseHandler> errorHandlerMock, int errorHandlerInvocations)
        {
            this.ResetMockProxyHttpClient();

            try
            {
                await proxyOperation.Invoke();
            }
            catch (PartnerException)
            {
                // do nothing
            }

            // ensure the error handler has been called
            errorHandlerMock.Verify(isCalled => isCalled.HandleFailedResponse(It.IsAny<HttpResponseMessage>(), It.IsAny<IRequestContext>()), Times.Exactly(errorHandlerInvocations));
        }

        /// <summary>
        /// Executes the given proxy operation and ensures that the error handler has been invoked.
        /// </summary>
        /// <param name="proxyOperation">The proxy operation to execute.</param>
        /// <param name="errorHandlerMock">The error handler mock object.</param>
        /// <param name="errorHandlerInvocations">The number of error handling invocations expected.</param>
        /// <returns>A task.</returns>
        private async Task EnsureProxyOperationFailureIsProperlyHandled(Func<Task> proxyOperation, Mock<IFailedPartnerServiceResponseHandler> errorHandlerMock, int errorHandlerInvocations)
        {
            this.ResetMockProxyHttpClient();

            try
            {
                await proxyOperation.Invoke();
            }
            catch (PartnerException)
            {
                // do nothing
            }

            // ensure the error handler has been called
            errorHandlerMock.Verify(isCalled => isCalled.HandleFailedResponse(It.IsAny<HttpResponseMessage>(), It.IsAny<IRequestContext>()), Times.Exactly(errorHandlerInvocations));
        }

        /// <summary>
        /// A sample refresh credentials call back which refreshes the credentials successfully.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="context">The partner context.</param>
        /// <returns>A task which completes when the refresh is done.</returns>
        private async Task RefreshCrednetialsOk(IPartnerCredentials credentials, IRequestContext context)
        {
            await new TaskFactory().StartNew(() =>
            {
                this.credentialsMock.Setup(cred => cred.IsExpired()).Returns(false);
                this.refreshCredentialsCallBackInvokationCount++;
            });
        }

        /// <summary>
        /// A sample refresh credentials call back which crashes.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="context">The partner context.</param>
        /// <returns>A task which completes when the refresh is done.</returns>
        private async Task RefreshCrednetialsCrashes(IPartnerCredentials credentials, IRequestContext context)
        {
            await new TaskFactory().StartNew(() =>
            {
                this.refreshCredentialsCallBackInvokationCount++;
                throw new ArgumentException("Some exception");
            });
        }

        /// <summary>
        /// A sample refresh credentials call back which does not update the token.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="context">The partner context.</param>
        /// <returns>A task which completes when the refresh is done.</returns>
        private async Task RefreshCrednetialsReturnsExpiredToken(IPartnerCredentials credentials, IRequestContext context)
        {
            await new TaskFactory().StartNew(() =>
            {
                this.refreshCredentialsCallBackInvokationCount++;
            });
        }

        /// <summary>
        /// Sets the HTTP response returned by the mock.
        /// </summary>
        /// <param name="statusCode">The desired status code.</param>
        /// <param name="content">The desired content.</param>
        private void MockHttpResponse(HttpStatusCode statusCode, string content)
        {
            // configure the HTTP client mock
            this.httpClientMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(content)
                }));
        }
    }
}