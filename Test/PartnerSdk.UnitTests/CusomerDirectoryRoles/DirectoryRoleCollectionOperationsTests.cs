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
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class is for testing directory role collection operations.
    /// </summary>
    [TestClass]
    public class DirectoryRoleCollectionOperationsTests
    {
        /// <summary>
        /// Method to test get all customer directory roles operation.
        /// </summary>
        /// <returns>A task which is completed when the test is finished.</returns>
        [TestMethod]
        public async Task DirectoryRoleCollectionOperationsTests_TestGetCustomerDirectoryRoles()
        {
            string expectedCustomerId = Guid.NewGuid().ToString();

            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            DirectoryRoleCollectionOperations directoryRoleCollectionOperations = new DirectoryRoleCollectionOperations(mockPartnerOperations.Object, expectedCustomerId);

            int proxyGetCustomerDirectoryRolesAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<DirectoryRole, ResourceCollection<DirectoryRole>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<DirectoryRole, ResourceCollection<DirectoryRole>> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerDirectoryRoles.Path, expectedCustomerId), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<DirectoryRole, ResourceCollection<DirectoryRole>>.AllInstances.GetAsync = (PartnerServiceProxy<DirectoryRole, ResourceCollection<DirectoryRole>> jsonProxy) =>
                {
                // increment the number of the calls.
                proxyGetCustomerDirectoryRolesAsyncCalls++;
                    return Task.FromResult<ResourceCollection<DirectoryRole>>(null);
                };

                // call both sync and async versions of the Get customer users API.
                var asyncResponse = await directoryRoleCollectionOperations.GetAsync();
                var response = directoryRoleCollectionOperations.Get();

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetCustomerDirectoryRolesAsyncCalls);
                Assert.AreEqual(null, asyncResponse);
                Assert.AreEqual(null, response);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }
    }
}