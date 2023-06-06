// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using Retries;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the LinearRetryPolicy class.
    /// </summary>
    [TestClass]
    public class LinearRetryPolicyTests
    {
        /// <summary>
        /// The maximum number of attempts.
        /// </summary>
        private const int MaxAttempts = 5;

        /// <summary>
        /// The back off period between retries.
        /// </summary>
        private readonly TimeSpan backOffPeriod = TimeSpan.FromSeconds(10);

        /// <summary>
        /// The linear retry policy we will test.
        /// </summary>
        private LinearBackOffRetryPolicy linearRetryPolicy;

        /// <summary>
        /// Initializes the tests.
        /// </summary>
        [TestInitialize]
        public void PrepareTests()
        {
            this.linearRetryPolicy = new LinearBackOffRetryPolicy(this.backOffPeriod, MaxAttempts);
        }

        /// <summary>
        /// Ensures the back off period remains the same.
        /// </summary>
        [TestMethod]
        public void LinearRetryPolicyTests_VerifyBackOffIsConstant()
        {
            Assert.AreEqual(this.linearRetryPolicy.BackOff, this.backOffPeriod);

            Assert.AreEqual(this.linearRetryPolicy.BackOffTime(0), this.backOffPeriod);
            Assert.AreEqual(this.linearRetryPolicy.BackOffTime(1), this.backOffPeriod);
            Assert.AreEqual(this.linearRetryPolicy.BackOffTime(2), this.backOffPeriod);
            Assert.AreEqual(this.linearRetryPolicy.BackOffTime(3), this.backOffPeriod);
            Assert.AreEqual(this.linearRetryPolicy.BackOffTime(4), this.backOffPeriod);
            Assert.AreEqual(this.linearRetryPolicy.BackOffTime(5), this.backOffPeriod);
        }

        /// <summary>
        /// Ensure the policy does not permit more than the configured number of attempts.
        /// </summary>
        [TestMethod]
        public void LinearRetryPolicyTests_VerifyLinearRetryTimes()
        {
            Assert.AreEqual(this.linearRetryPolicy.MaxRetries, MaxAttempts);

            Assert.IsTrue(this.linearRetryPolicy.ShouldRetry(0));
            Assert.IsTrue(this.linearRetryPolicy.ShouldRetry(1));
            Assert.IsTrue(this.linearRetryPolicy.ShouldRetry(4));
            Assert.IsTrue(this.linearRetryPolicy.ShouldRetry(5));

            Assert.IsFalse(this.linearRetryPolicy.ShouldRetry(6));
            Assert.IsFalse(this.linearRetryPolicy.ShouldRetry(7));
            Assert.IsFalse(this.linearRetryPolicy.ShouldRetry(57));
        }
    }
}