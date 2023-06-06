// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using Retries;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the ExponentialRetryPolicy class.
    /// </summary>
    [TestClass]
    public class ExponentialRetryPolicyTests
    {
        /// <summary>
        /// The maximum number of attempts.
        /// </summary>
        private const int MaxAttempts = 5;

        /// <summary>
        /// The exponential retry policy we will test.
        /// </summary>
        private ExponentialBackOffRetryPolicy exponentialRetryPolicy;

        /// <summary>
        /// Initializes the tests.
        /// </summary>
        [TestInitialize]
        public void PrepareTests()
        {
            this.exponentialRetryPolicy = new ExponentialBackOffRetryPolicy(MaxAttempts);
        }

        /// <summary>
        /// Ensures the back off period grows exponentially.
        /// </summary>
        [TestMethod]
        public void ExponentialRetryPolicyTests_VerifyBackOffIsExponential()
        {
            Assert.AreEqual(this.exponentialRetryPolicy.BackOffTime(0), TimeSpan.FromSeconds(0));
            Assert.AreEqual(this.exponentialRetryPolicy.BackOffTime(1), TimeSpan.FromSeconds(0.5));
            Assert.AreEqual(this.exponentialRetryPolicy.BackOffTime(2), TimeSpan.FromSeconds(1.5));
            Assert.AreEqual(this.exponentialRetryPolicy.BackOffTime(3), TimeSpan.FromSeconds(3.5));
            Assert.AreEqual(this.exponentialRetryPolicy.BackOffTime(4), TimeSpan.FromSeconds(7.5));
            Assert.AreEqual(this.exponentialRetryPolicy.BackOffTime(5), TimeSpan.FromSeconds(15.5));
        }

        /// <summary>
        /// Ensure the policy does not permit more than the configured number of attempts.
        /// </summary>
        [TestMethod]
        public void ExponentialRetryPolicyTests_VerifyExponentialRetryTimes()
        {
            Assert.AreEqual(this.exponentialRetryPolicy.MaxRetries, MaxAttempts);

            Assert.IsTrue(this.exponentialRetryPolicy.ShouldRetry(0));
            Assert.IsTrue(this.exponentialRetryPolicy.ShouldRetry(1));
            Assert.IsTrue(this.exponentialRetryPolicy.ShouldRetry(4));
            Assert.IsTrue(this.exponentialRetryPolicy.ShouldRetry(5));

            Assert.IsFalse(this.exponentialRetryPolicy.ShouldRetry(6));
            Assert.IsFalse(this.exponentialRetryPolicy.ShouldRetry(7));
            Assert.IsFalse(this.exponentialRetryPolicy.ShouldRetry(57));
        }
    }
}