// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.CustomerLicenses
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ErrorHandling;
    using Models.Licenses;
    using Moq;
    using Network;
    using Network.Fakes;
    using Newtonsoft.Json;
    using PartnerCenter;
    using PartnerCenter.Models;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using SubscribedSkus;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class is for testing customer subscribed products operations.
    /// </summary>
    [TestClass]
    public class CustomerSubscribedSkusTests
    {
        /// <summary>
        /// The expected customer id.
        /// </summary>
        private static readonly Guid ExpectedCustomerId = Guid.NewGuid();

        /// <summary>
        /// The customer subscribed products operations instance under test.
        /// </summary>
        private static CustomerSubscribedSkuCollectionOperations customerSubscribedSkuOperations;

        /// <summary>
        /// Ensures that customer subscribed products passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        [TestMethod]
        public void CustomerSubscribedSkusTests_VerifyCustomerSubscribedSkusCustomerIdNullFails()
        {
            try
            {
                customerSubscribedSkuOperations = new CustomerSubscribedSkuCollectionOperations(Mock.Of<IPartner>(), null);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("customerId must be set.", ex.Message);
            }
        }

        /// <summary>
        /// Ensures that customer subscribed products passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerSubscribedSkusTests_VerifyCustomerSubscribedSkus()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerSubscribedSkuOperations = new CustomerSubscribedSkuCollectionOperations(mockPartnerOperations.Object, ExpectedCustomerId.ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerSubscribedSkus.Path, ExpectedCustomerId.ToString()), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>>.AllInstances.GetAsync = (PartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;

                    // return the user object.
                    return Task.FromResult<ResourceCollection<SubscribedSku>>(null);
                };

                // call both sync and async versions of the get licenses API.
                await customerSubscribedSkuOperations.GetAsync();
                customerSubscribedSkuOperations.Get();

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that customer subscribed products passes in the right values to the proxy and actually calls the proxy for Group1 license group.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerSubscribedSkusTests_VerifyCustomerGroup1SubscribedSkus()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerSubscribedSkuOperations = new CustomerSubscribedSkuCollectionOperations(mockPartnerOperations.Object, ExpectedCustomerId.ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>>.AllInstances.GetAsync = (PartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;

                    // Verify that uri parameter was added for Group1.
                    Assert.AreEqual(1, jsonProxy.UriParameters.Count);
                    Assert.IsTrue(jsonProxy.UriParameters.Contains(new KeyValuePair<string, string>("licenseGroupIds", LicenseGroupId.Group1.ToString())));

                    // return the user object.
                    return Task.FromResult<ResourceCollection<SubscribedSku>>(null);
                };

                // call both sync and async versions of the get licenses API.
                List<LicenseGroupId> groups = new List<LicenseGroupId>() { LicenseGroupId.Group1 };
                await customerSubscribedSkuOperations.GetAsync(groups);
                customerSubscribedSkuOperations.Get(groups);

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that customer subscribed products passes in the right values to the proxy and actually calls the proxy for Group2 license group.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerSubscribedSkusTests_VerifyCustomerGroup2SubscribedSkus()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerSubscribedSkuOperations = new CustomerSubscribedSkuCollectionOperations(mockPartnerOperations.Object, ExpectedCustomerId.ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>>.AllInstances.GetAsync = (PartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;

                    // Verify that uri parameter was added for Group2.
                    Assert.AreEqual(1, jsonProxy.UriParameters.Count);
                    Assert.IsTrue(jsonProxy.UriParameters.Contains(new KeyValuePair<string, string>("licenseGroupIds", LicenseGroupId.Group2.ToString())));

                    // return the user object.
                    return Task.FromResult<ResourceCollection<SubscribedSku>>(null);
                };

                // call both sync and async versions of the get licenses API.
                List<LicenseGroupId> groups = new List<LicenseGroupId>() { LicenseGroupId.Group2 };
                await customerSubscribedSkuOperations.GetAsync(groups);
                customerSubscribedSkuOperations.Get(groups);

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that customer subscribed products passes in the right values to the proxy and actually calls the proxy for Group1 and Group2 license groups.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerSubscribedSkusTests_VerifyCustomerGroup1AndGroup2SubscribedSkus()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerSubscribedSkuOperations = new CustomerSubscribedSkuCollectionOperations(mockPartnerOperations.Object, ExpectedCustomerId.ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>>.AllInstances.GetAsync = (PartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;

                    // Verify that two uri parameter were added. One for Group1 and one for Group2.
                    Assert.AreEqual(2, jsonProxy.UriParameters.Count);
                    Assert.IsTrue(jsonProxy.UriParameters.Contains(new KeyValuePair<string, string>("licenseGroupIds", LicenseGroupId.Group1.ToString())));
                    Assert.IsTrue(jsonProxy.UriParameters.Contains(new KeyValuePair<string, string>("licenseGroupIds", LicenseGroupId.Group2.ToString())));

                    // return the user object.
                    return Task.FromResult<ResourceCollection<SubscribedSku>>(null);
                };

                // call both sync and async versions of the get licenses API.
                List<LicenseGroupId> groups = new List<LicenseGroupId>() { LicenseGroupId.Group1, LicenseGroupId.Group2 };
                await customerSubscribedSkuOperations.GetAsync(groups);
                customerSubscribedSkuOperations.Get(groups);

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }
    }
}