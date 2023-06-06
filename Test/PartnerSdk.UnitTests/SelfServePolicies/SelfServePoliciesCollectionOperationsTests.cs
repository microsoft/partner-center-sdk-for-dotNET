// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.SelfServePolicies
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.SelfServePolicies;
    using Microsoft.Store.PartnerCenter.SelfServePolicies;
    using Moq;
    using Network;
    using Network.Fakes;
    using PartnerCenter;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class is for testing self serve policy collection operations.
    /// </summary>
    [TestClass]
    public class SelfServePoliciesCollectionOperationsTests
    {
        /// <summary>
        /// Ensures that get self service policy passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task SelfServePolicyOperationsTests_VerifyGetServePolicy()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            var id = Guid.NewGuid().ToString();
            var partnerId = Guid.NewGuid().ToString();
            var customerId = Guid.NewGuid().ToString();
            var selfServePolicy = GetSelfServePolicy(partnerId, customerId);

            var selfServePolicyOperations = new SelfServePoliciesCollectionOperations(mockPartnerOperations.Object, id);
            int proxyCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<SelfServePolicy, ResourceCollection<SelfServePolicy>>.AllInstances.GetAsync = (PartnerServiceProxy<SelfServePolicy, ResourceCollection<SelfServePolicy>> jsonProxy) =>
                {
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetSelfServePolicies.Path, jsonProxy.ResourcePath);

                    // increment the number of the calls.
                    proxyCalls++;
                    return Task.FromResult<ResourceCollection<SelfServePolicy>>(null);
                };

                // call both sync and async versions of the delete customer user API.
                await selfServePolicyOperations.GetAsync(id);
                selfServePolicyOperations.Get(id);

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that get self service policy passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task SelfServePolicyOperationsTests_VerifyCreateServePolicy()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            var id = Guid.NewGuid().ToString();
            var partnerId = Guid.NewGuid().ToString();
            var customerId = Guid.NewGuid().ToString();
            var selfServePolicy = GetSelfServePolicy(partnerId, customerId);

            var selfServePolicyOperations = new SelfServePoliciesCollectionOperations(mockPartnerOperations.Object, id);
            int proxyCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<SelfServePolicy, SelfServePolicy>.AllInstances.PostAsyncT0 = (jsonProxy, newSelfServePolicyy) =>
                {
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.CreateSelfServePolicy.Path, jsonProxy.ResourcePath);
                    this.ValidateContractSelfServePolicy(selfServePolicy, newSelfServePolicyy);

                    // increment the number of the calls.
                    proxyCalls++;
                    return Task.FromResult<SelfServePolicy>(newSelfServePolicyy);
                };

                // call both sync and async versions of the delete customer user API.
                await selfServePolicyOperations.CreateAsync(selfServePolicy);
                selfServePolicyOperations.Create(selfServePolicy);

                // ensure the proxy PostAsync() was called twice.
                Assert.AreEqual(2, proxyCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        private void ValidateContractSelfServePolicy(SelfServePolicy expectedObject, SelfServePolicy newObject)
        {
            Assert.AreEqual(expectedObject.Id, newObject.Id);
            Assert.AreEqual(expectedObject.Grantor.GrantorType, newObject.Grantor.GrantorType);
            Assert.AreEqual(expectedObject.Grantor.TenantID, newObject.Grantor.TenantID);
            Assert.AreEqual(expectedObject.SelfServeEntity.SelfServeEntityType, newObject.SelfServeEntity.SelfServeEntityType);
            Assert.AreEqual(expectedObject.SelfServeEntity.TenantID, newObject.SelfServeEntity.TenantID);
            Assert.AreEqual(expectedObject.Permissions.Length, newObject.Permissions.Length);

            foreach (var permission in expectedObject.Permissions)
            {
                Assert.IsTrue(newObject.Permissions.Where(p => p.Action == permission.Action && p.Resource == permission.Resource).Any());
            }
        }

        private SelfServePolicy GetSelfServePolicy(string partnerId, string customerId)
        {
            return new SelfServePolicy
            {
                SelfServeEntity = new SelfServeEntity
                {
                    SelfServeEntityType = "customer",
                    TenantID = customerId,
                },
                Grantor = new Grantor
                {
                    GrantorType = "billToPartner",
                    TenantID = partnerId,
                },
                Permissions = new Permission[]
                {
                    new Permission
                    {
                    Action = "Purchase",
                    Resource = "AzureReservedInstances",
                    },
                },
            };
        }
    }
}
