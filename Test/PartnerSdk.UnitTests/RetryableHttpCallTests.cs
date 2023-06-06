// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Moq;
    using Network;
    using Retries;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="RetryableHttpCall"/> class.
    /// </summary>
    [TestClass]
    public class RetryableHttpCallTests
    {
        /// <summary>
        /// The retry policy under test.
        /// </summary>
        private readonly LinearBackOffRetryPolicy retryPolicy = new LinearBackOffRetryPolicy(TimeSpan.FromMilliseconds(50), 2);

        /// <summary>
        /// A mock HTTP operation.
        /// </summary>
        private readonly Mock<Func<Task<HttpResponseMessage>>> mockHttpOperation = new Mock<Func<Task<HttpResponseMessage>>>();

        /// <summary>
        /// The retryable HTTP call under test.
        /// </summary>
        private RetryableHttpCall retryableHttpCall;

        /// <summary>
        /// Initializes the tests.
        /// </summary>
        [TestInitialize]
        public void PrepareTests()
        {
            this.retryableHttpCall = new RetryableHttpCall(this.retryPolicy);            
        }

        /// <summary>
        /// Verifies that running a successful operation only invokes it once and returns the expected results.
        /// </summary>
        /// <returns>A task.</returns>
        [TestMethod]
        public async Task RetryableHttpCallTests_VerifySuccessfulOperation()
        {
            // mock the operation to return a successful response
            this.mockHttpOperation.Setup(operation => operation())
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            // ensure that the result is successful
            Assert.AreEqual(HttpStatusCode.OK, (await this.retryableHttpCall.Execute(this.mockHttpOperation.Object)).StatusCode);

            // ensure the operation was called only once
            this.mockHttpOperation.Verify(operation => operation(), Times.Once);
        }

        /// <summary>
        /// Verifies that running a failed operation which should not be retried is actually not retried and
        /// returns the failed response.
        /// </summary>
        /// <returns>A task.</returns>
        [TestMethod]
        public async Task RetryableHttpCallTests_VerifyNonRetryableFailedOperation()
        {
            HttpStatusCode[] nonRetryableHttpCodes =
            {
                HttpStatusCode.BadRequest,
                HttpStatusCode.Unauthorized,
                HttpStatusCode.Forbidden,
                HttpStatusCode.NotFound,
                HttpStatusCode.Conflict
            };

            foreach (var httpCode in nonRetryableHttpCodes)
            {
                // mock the operation to return a response which should not be retried
                this.mockHttpOperation.Setup(operation => operation())
                    .Returns(Task.FromResult(new HttpResponseMessage(httpCode)));

                // ensure that the result matches
                Assert.AreEqual(httpCode, (await this.retryableHttpCall.Execute(this.mockHttpOperation.Object)).StatusCode);

                // ensure the operation was called only once
                this.mockHttpOperation.Verify(operation => operation(), Times.Once);
                this.mockHttpOperation.ResetCalls();
            }
        }

        /// <summary>
        /// Verifies that running a failed operation is retried according to the policy and that the
        /// failed response is returned.
        /// </summary>
        /// <returns>A task.</returns>
        [TestMethod]
        public async Task RetryableHttpCallTests_VerifyRetryableFailedOperation()
        {
            var stopWatch = new Stopwatch();

            // mock the operation to return a 500 which should be retried
            this.mockHttpOperation.Setup(operation => operation())
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError)))
                .Callback(() =>
                {
                    if (stopWatch.IsRunning == false)
                    {
                        // this is the first call, start the timer
                        stopWatch.Start();
                    }
                    else
                    {
                        // ensure that the current call was invoked after the configured back off time
                        Assert.IsTrue(stopWatch.Elapsed > this.retryPolicy.BackOff);
                        stopWatch.Restart();
                    }
                });

            // ensure that the result is a 500
            Assert.AreEqual(HttpStatusCode.InternalServerError, (await this.retryableHttpCall.Execute(this.mockHttpOperation.Object)).StatusCode);

            stopWatch.Stop();

            // ensure the operation was called the number of times permitted by the retry policy
            this.mockHttpOperation.Verify(operation => operation(), Times.Exactly(this.retryPolicy.MaxRetries + 1));
        }

        /// <summary>
        /// Ensures that running an operation which throws an exception is retired according to the policy
        /// and that the exception will be eventually thrown.
        /// </summary>
        /// <returns>A task.</returns>
        [TestMethod]
        public async Task RetryableHttpCallTests_VerifyCrashingOperation()
        {
            // mock the operation to return a response which should not be retried
            this.mockHttpOperation.Setup(operation => operation())
                .Throws(new InvalidOperationException());

            // ensure that an invalid exception will be thrown after the max retries have been performed
            try
            {
                await this.retryableHttpCall.Execute(this.mockHttpOperation.Object);
                Assert.Fail("RetryableHttpCallTests_VerifyCrashingOperation: an InvalidOperationException should have been thrown.");
            }
            catch (InvalidOperationException)
            {
                this.mockHttpOperation.Verify(operation => operation(), Times.Exactly(this.retryPolicy.MaxRetries + 1));
            }
        }

        /// <summary>
        /// Ensures that running an operation which throws an exception and upon retry succeeds actually
        /// returns the successful response.
        /// </summary>
        /// <returns>A task.</returns>
        [TestMethod]
        public async Task RetryableHttpCallTests_VerifyCrashingThenSuccessfulOperation()
        {
            int numberOfCalls = 0;

            // mock the operation to return a response which should not be retried
            this.mockHttpOperation.Setup(operation => operation())
                .Callback(() =>
                {
                    if (++numberOfCalls == 1)
                    {
                        // let the operation fail the first time it is called
                        throw new InvalidOperationException();
                    }
                })
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            // ensure that the result is successful
            Assert.AreEqual(HttpStatusCode.OK, (await this.retryableHttpCall.Execute(this.mockHttpOperation.Object)).StatusCode);

            // ensure the operation was called twice
            this.mockHttpOperation.Verify(operation => operation(), Times.Exactly(2));
        }
    }
}
