// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.SelfServePolicies
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
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
    /// This class is for testing self serve policy operations.
    /// </summary>
    [TestClass]
    public class SelfServePolicyOperationsTests
    {
        /// <summary>
        /// Ensures that delete self service policy passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task SelfServePolicyOperationsTests_VerifyDeleteServePolicy()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            var id = Guid.NewGuid().ToString();

            var selfServePolicyOperations = new SelfServePolicyOperations(mockPartnerOperations.Object, id);
            int proxyCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to DeleteAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<SelfServePolicy, SelfServePolicy>.AllInstances.DeleteAsync = (PartnerServiceProxy<SelfServePolicy, SelfServePolicy> jsonProxy) =>
                {
                    Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.DeleteSelfServePolicy.Path, id), jsonProxy.ResourcePath);

                    // increment the number of the calls.
                    proxyCalls++;
                    return Task.FromResult<SelfServePolicy>(null);
                };

                // call both sync and async versions of the delete customer user API.
                await selfServePolicyOperations.DeleteAsync();
                selfServePolicyOperations.Delete();

                // ensure the proxy DeleteAsync() was called twice.
                Assert.AreEqual(2, proxyCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that put self service policy passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task SelfServePolicyOperationsTests_VerifyPutServePolicy()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            var id = Guid.NewGuid().ToString();
            var partnerId = Guid.NewGuid().ToString();
            var customerId = Guid.NewGuid().ToString();
            var selfServePolicy = GetSelfServePolicy(partnerId, customerId);

            var selfServePolicyOperations = new SelfServePolicyOperations(mockPartnerOperations.Object, id);
            int proxyCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to PutAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<SelfServePolicy, SelfServePolicy>.AllInstances.PutAsyncT0 = (jsonProxy, updatedSelfServePolicy) =>
                {
                    Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpdateSelfServePolicy.Path, id), jsonProxy.ResourcePath);
                    this.ValidateContractSelfServePolicy(selfServePolicy, updatedSelfServePolicy);
                    
                    // increment the number of the calls
                    proxyCalls++;
                    return Task.FromResult(selfServePolicy);
                };

                // call both sync and async versions of the delete customer user API.
                await selfServePolicyOperations.PutAsync(selfServePolicy);
                selfServePolicyOperations.Put(selfServePolicy);

                // ensure the proxy PutAsync() was called twice.
                Assert.AreEqual(2, proxyCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that self serve policy operations fails when null id is passed.
        /// network methods.
        /// </summary>
        [TestMethod]
        public void SelfServePolicyOperationsTests_VerifySelfServePolicyOperationsIdNullFails()
        {
            try
            {
                var selfServePolicyOperations = new SelfServePolicyOperations(Mock.Of<IPartner>(), null);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("id must be set.", ex.Message);
            }
        }

        /// <summary>
        /// Ensures that self serve policy operations fails when null entity is passed.
        /// network methods.
        /// </summary>
        [TestMethod]
        public async Task SelfServePolicyOperationsTests_VerifySelfServePolicyOperationsNewEntityNullFails()
        {
            var customerId = Guid.NewGuid().ToString();

            try
            {
                var selfServePolicyOperations = new SelfServePolicyOperations(Mock.Of<IPartner>(), customerId);
                await selfServePolicyOperations.PutAsync(null);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: newEntity can't be null", ex.Message);
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
