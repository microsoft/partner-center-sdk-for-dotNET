// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.CustomerDirectoryRoles
{
    using System;
    using System.Threading.Tasks;
    using ErrorHandling;
    using Models.Roles;
    using Moq;
    using Network;
    using Network.Fakes;
    using Newtonsoft.Json;
    using PartnerCenter;
    using PartnerCenter.CustomerDirectoryRoles;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class is for testing user member operations.
    /// </summary>
    [TestClass]
    public class UserMemberOperationsTests
    {
        /// <summary>
        /// Ensures that remove user member from directory role passes in the right values to the proxy and actually calls the proxy.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task UserMemberOperationsTest_VerifyRemoveUserMemberFromDirectoryRole()
        {
            var customerId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            UserMemberOperations userMemberOperations = new UserMemberOperations(mockPartnerOperations.Object, customerId.ToString(), roleId.ToString(), memberId.ToString());

            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<DirectoryRole, DirectoryRole>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<DirectoryRole, DirectoryRole> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.RemoveCustomerUserMemberFromDirectoryRole.Path, customerId, roleId, memberId), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<DirectoryRole, DirectoryRole>.AllInstances.DeleteAsync = (PartnerServiceProxy<DirectoryRole, DirectoryRole> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;
                    return Task.FromResult<DirectoryRole>(null);
                };

                // call both sync and async versions of the role user members API.
                await userMemberOperations.DeleteAsync();
                userMemberOperations.Delete();

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }
    }
}