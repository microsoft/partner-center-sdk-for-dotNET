// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.Domains
{
    using System.Globalization;
    using System.Threading.Tasks;
    using ErrorHandling;
    using Exceptions;
    using Moq;
    using Network;
    using Network.Fakes;
    using Newtonsoft.Json;
    using PartnerCenter.Domains;
    using QualityTools.Testing.Fakes;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for DomainOperations
    /// </summary>
    [TestClass]
    public class DomainOperationsTests
    {
        /// <summary>
        /// Ensures that check domain availability pass in the right values to the proxy and actually calls the proxy
        /// network methods for an available domain.
        /// </summary>
        /// <returns> A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task DomainOperationsTests_VerifyDomainExists_AvailableDomain()
        {
            var domain = "domain";

            int proxyHeadAsyncCalls = 0;
            
            var domainOperations = new DomainOperations(Mock.Of<IPartner>(), domain);

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<string, string>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<string, string> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    var expectedResourcePath = string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CheckDomainAvailability.Path, domain);
                    Assert.AreEqual(expectedResourcePath, resourcePath);
                };

                // divert calls to HaadAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<string, string>.AllInstances.HeadAsync = (PartnerServiceProxy<string, string> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyHeadAsyncCalls++;
                    return Task.FromResult<bool>(true);
                };

                // call both sync and async versions of the domain availability checks API
                var result = await domainOperations.ExistsAsync();
                Assert.IsTrue(result);
                result = domainOperations.Exists();
                Assert.IsTrue(result);

                // ensure the proxy HeadAsync() was called twice
                Assert.AreEqual(2, proxyHeadAsyncCalls);
            }
        }

        /// <summary>
        /// Ensures that check domain availability pass in the right values to the proxy and actually calls the proxy
        /// network methods for a non-available domain.
        /// </summary>
        /// <returns> A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task DomainOperationsTests_VerifyDomainExists_NotAvailableDomain()
        {
            var domain = "domain";

            int proxyHeadAsyncCalls = 0;

            var domainOperations = new DomainOperations(Mock.Of<IPartner>(), domain);

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<string, string>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<string, string> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    var expectedResourcePath = string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CheckDomainAvailability.Path, domain);
                    Assert.AreEqual(expectedResourcePath, resourcePath);
                };

                // divert calls to HaadAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<string, string>.AllInstances.HeadAsync = (PartnerServiceProxy<string, string> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyHeadAsyncCalls++;
                    throw new PartnerException(string.Empty, null, PartnerErrorCategory.NotFound);
                };

                // call both sync and async versions of the domain availability checks API
                var result = await domainOperations.ExistsAsync();
                Assert.IsFalse(result);
                result = domainOperations.Exists();
                Assert.IsFalse(result);

                // ensure the proxy HeadAsync() was called twice
                Assert.AreEqual(2, proxyHeadAsyncCalls);
            }
        }
    }
}