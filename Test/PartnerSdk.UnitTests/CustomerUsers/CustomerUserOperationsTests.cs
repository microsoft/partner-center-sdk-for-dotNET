// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.CustomerUsers
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
    using PartnerCenter.CustomerUsers;
    using PartnerCenter.Models;
    using PartnerCenter.Models.Users;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class is for testing user operations.
    /// </summary>
    [TestClass]
    public class CustomerUserOperationsTests
    {
        /// <summary>
        /// The expected customer id.
        /// </summary>
        private static readonly Guid ExpectedCustomerId = Guid.NewGuid();

        /// <summary>
        /// The expected user id.
        /// </summary>
        private static readonly Guid ExpectedUserId = Guid.NewGuid();

        /// <summary>
        /// The user operations instance under test.
        /// </summary>
        private static CustomerUserOperations customerUserOperations;

        /// <summary>
        /// Ensures that update customer user passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerUserCollectionOperationsTests_VerifyUpdateCustomerUser()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerUserOperations = new CustomerUserOperations(mockPartnerOperations.Object, ExpectedCustomerId.ToString(), ExpectedUserId.ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<CustomerUser, CustomerUser>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<CustomerUser, CustomerUser> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.UpdateCustomerUser.Path, ExpectedCustomerId.ToString(), ExpectedUserId.ToString()), resourcePath);
                };

                // Specifying update attributes.
                var userToUpdate = new CustomerUser()
                {
                    PasswordProfile = new PasswordProfile() { ForceChangePassword = true, Password = "testPassword" },
                    DisplayName = "Roger Federer",
                    FirstName = "Roger",
                    LastName = "Federer",
                    UserPrincipalName = "roger@master.com"
                };

                // divert calls to UpdateAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<CustomerUser, CustomerUser>.AllInstances.PatchAsyncT0 = (PartnerServiceProxy<CustomerUser, CustomerUser> jsonProxy, CustomerUser user) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;

                    // return the user object.
                    return Task.FromResult<CustomerUser>(user);
                };

                // call both sync and async versions of the update users API.
                var updatedUser = await customerUserOperations.PatchAsync(userToUpdate);
                AssertAreEqual(userToUpdate, updatedUser);
                updatedUser = customerUserOperations.Patch(userToUpdate);
                AssertAreEqual(userToUpdate, updatedUser);

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that get customer user passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerUserOperationsTests_VerifyGetCustomerUser()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerUserOperations = new CustomerUserOperations(mockPartnerOperations.Object, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<CustomerUser, SeekBasedResourceCollection<CustomerUser>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<CustomerUser, SeekBasedResourceCollection<CustomerUser>> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetPartnerUser.Path), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<CustomerUser, CustomerUser>.AllInstances.GetAsync = (PartnerServiceProxy<CustomerUser, CustomerUser> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;
                    return Task.FromResult<CustomerUser>(null);
                };

                // call both sync and async versions of the get customer user API.
                await customerUserOperations.GetAsync();
                customerUserOperations.Get();

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that update customer user passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        [TestMethod]
        public void CustomerUserOperationsTests_VerifyCustomerUserUpdateCustomerIdNullFails()
        {
            try
            {
                customerUserOperations = new CustomerUserOperations(Mock.Of<IPartner>(), null, ExpectedUserId.ToString());
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("customerId must be set.", ex.Message);
            }
        }

        /// <summary>
        /// Ensures that update customer user passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        [TestMethod]
        public void CustomerUserOperationsTests_VerifyCustomerUserUpdateUserIdNullFails()
        {
            try
            {
                customerUserOperations = new CustomerUserOperations(Mock.Of<IPartner>(), ExpectedCustomerId.ToString(), null);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("userId must be set.", ex.Message);
            }
        }

        /// <summary>
        /// Ensures that get customer user directory roles passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerUserOperationsTests_VerifyGetCustomerUserDirectoryRoles()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerUserOperations = new CustomerUserOperations(mockPartnerOperations.Object, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<CustomerUser, SeekBasedResourceCollection<CustomerUser>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<CustomerUser, SeekBasedResourceCollection<CustomerUser>> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.CustomerUserDirectoryRoles.Path), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<CustomerUser, ResourceCollection<DirectoryRole>>.AllInstances.GetAsync = (PartnerServiceProxy<CustomerUser, ResourceCollection<DirectoryRole>> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;
                    return Task.FromResult<ResourceCollection<DirectoryRole>>(null);
                };

                // call both sync and async versions of the get partner user API.
                await customerUserOperations.DirectoryRoles.GetAsync();
                customerUserOperations.DirectoryRoles.Get();

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that delete customer user passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerUserOperationsTests_VerifyDeleteCustomerUser()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerUserOperations = new CustomerUserOperations(mockPartnerOperations.Object, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<CustomerUser, SeekBasedResourceCollection<CustomerUser>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<CustomerUser, SeekBasedResourceCollection<CustomerUser>> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.DeletePartnerUser.Path), resourcePath);
                };

                // divert calls to DeleteAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<CustomerUser, CustomerUser>.AllInstances.DeleteAsync = (PartnerServiceProxy<CustomerUser, CustomerUser> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;
                    return Task.FromResult<CustomerUser>(null);
                };

                // call both sync and async versions of the delete customer user API.
                await customerUserOperations.DeleteAsync();
                customerUserOperations.Delete();

                // ensure the proxy DeleteAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures an user instance matches expectations.
        /// </summary>
        /// <param name="expected">The expected user state.</param>
        /// <param name="actual">The actual user information.</param>
        private static void AssertAreEqual(CustomerUser expected, CustomerUser actual)
        {
            if (expected == null || actual == null)
            {
                // both should be null
                Assert.AreEqual(expected, actual);
                return;
            }

            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.UserPrincipalName, actual.UserPrincipalName);
        }
    }
}