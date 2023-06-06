// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// SDK Configuration tests.
    /// </summary>
    [TestClass]
    public class SdkConfigurationTests
    {
        /// <summary>
        /// Ensures that the SDK properly loads its configuration.
        /// </summary>
        [TestMethod]
        public void SdkConfigurationTest()
        {
            Assert.IsNotNull(PartnerService.Instance.Configuration);
            Assert.IsFalse(string.IsNullOrEmpty(PartnerService.Instance.Configuration.PartnerServiceApiRoot));
            Assert.IsFalse(string.IsNullOrEmpty(PartnerService.Instance.Configuration.PartnerServiceApiVersion));
            Assert.IsTrue(PartnerService.Instance.Configuration.DefaultMaxRetryAttempts is int);
            Assert.IsTrue(PartnerService.Instance.Configuration.DefaultAuthenticationTokenExpiryBufferInSeconds is int);
            Assert.IsNotNull(PartnerService.Instance.Configuration.Apis);
        }
    }
}
