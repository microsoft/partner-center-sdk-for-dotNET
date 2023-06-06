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
    using PartnerCenter.Models;
    using PartnerCenter.Models.Query;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class is for testing user member collection operations.
    /// </summary>
    [TestClass]
    public class UserMemberCollectionOperationsTest
    {
        /// <summary>
        /// The customer directory role operations instance under test.
        /// </summary>
        private static UserMemberCollectionOperations userMemberCollectionOperations;

        /// <summary>
        /// Ensures that get directory role user members passes in the right values to the proxy and actually calls the proxy.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task UserMemberCollectionOperationsTest_VerifyGetCustomerDirectoryRoleUserMembers()
        {
            string roleId = Guid.NewGuid().ToString();
            string customerId = Guid.NewGuid().ToString();

            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            userMemberCollectionOperations = new UserMemberCollectionOperations(mockPartnerOperations.Object, customerId, roleId);

            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // Route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<DirectoryRole, SeekBasedResourceCollection<Models.Roles.UserMember>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<DirectoryRole, SeekBasedResourceCollection<Models.Roles.UserMember>> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerDirectoryRoleUserMembers.Path, customerId, roleId), resourcePath);
                };

                // Divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<DirectoryRole, SeekBasedResourceCollection<Models.Roles.UserMember>>.AllInstances.GetAsync = (PartnerServiceProxy<DirectoryRole, SeekBasedResourceCollection<Models.Roles.UserMember>> jsonProxy) =>
                {
                    // Increment the number of the calls.
                    proxyGetAsyncCalls++;
                    return Task.FromResult<SeekBasedResourceCollection<Models.Roles.UserMember>>(null);
                };

                // Call both sync and async versions.
                await userMemberCollectionOperations.GetAsync();
                userMemberCollectionOperations.Get();

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that get directory role user members with null paging value combinations fails.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task UserMemberCollectionOperationsTest_VerifyGetCustomerDirectoryRoleUserMembersWithNullPagingValuesFails()
        {
            string roleId = Guid.NewGuid().ToString();
            string customerId = Guid.NewGuid().ToString();

            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            userMemberCollectionOperations = new UserMemberCollectionOperations(mockPartnerOperations.Object, customerId, roleId);

            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<DirectoryRole, SeekBasedResourceCollection<Models.Roles.UserMember>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<DirectoryRole, SeekBasedResourceCollection<Models.Roles.UserMember>> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerDirectoryRoleUserMembers.Path, customerId, roleId), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<DirectoryRole, SeekBasedResourceCollection<Models.Roles.UserMember>>.AllInstances.GetAsync = (PartnerServiceProxy<DirectoryRole, SeekBasedResourceCollection<Models.Roles.UserMember>> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;
                    return Task.FromResult<SeekBasedResourceCollection<Models.Roles.UserMember>>(null);
                };

                try
                {
                    await userMemberCollectionOperations.QueryAsync(QueryFactory.Instance.BuildCountQuery());
                    Assert.Fail();
                }
                catch (ArgumentException)
                {
                }

                try
                {
                    userMemberCollectionOperations.Query(QueryFactory.Instance.BuildSeekQuery(SeekOperation.Next, token: null));
                    Assert.Fail();
                }
                catch (ArgumentNullException)
                {
                }

                Assert.AreEqual(0, proxyGetAsyncCalls);
            }
        }

        /// <summary>
        /// Ensures that add users member to directory role passes in the right values to the proxy and actually calls the proxy.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task UserMemberCollectionOperationsTest_VerifyAddUserMemberToDirectoryRole()
        {
            string customerId = Guid.NewGuid().ToString();
            var roleId = Guid.NewGuid();
            var newUserMember = new Models.Roles.UserMember();
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            userMemberCollectionOperations = new UserMemberCollectionOperations(mockPartnerOperations.Object, customerId, roleId.ToString());

            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<UserMember, Models.Roles.UserMember>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<UserMember, Models.Roles.UserMember> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.AddUserToCustomerDirectoryRole.Path, customerId, roleId), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<Models.Roles.UserMember, Models.Roles.UserMember>.AllInstances.PostAsyncT0 = (PartnerServiceProxy<Models.Roles.UserMember, Models.Roles.UserMember> jsonProxy, Models.Roles.UserMember userMemberToAdd) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;
                    return Task.FromResult<Models.Roles.UserMember>(newUserMember);
                };

                // call both sync and async versions of the role user members API.
                await userMemberCollectionOperations.CreateAsync(newUserMember);
                userMemberCollectionOperations.Create(newUserMember);

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that add user member to directory role fails with null user member.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task UserMemberCollectionOperationsTestt_VerifyAddUserMemberToDirectoryRoleFailsNullUser()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            userMemberCollectionOperations = new UserMemberCollectionOperations(mockPartnerOperations.Object, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            try
            {
                await userMemberCollectionOperations.CreateAsync(null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
            }

            try
            {
                userMemberCollectionOperations.Create(null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
            }
        }

        /// <summary>
        /// Ensures that remove user member from directory role passes in the right values to the proxy and actually calls the proxy.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerDirectoryRoleOperationsTest_VerifyRemoveUserMemberFromDirectoryRole()
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