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
    using PartnerCenter.CustomerUsers;
    using PartnerCenter.Models;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class is for testing customer user license operations.
    /// </summary>
    [TestClass]
    public class CustomerUserLicenseOperationsTests
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
        /// The customer user license operation instance under test.
        /// </summary>
        private static CustomerUserLicenseCollectionOperations customerUserLicenseOperations;

        /// <summary>
        /// The customer user license update operation instance under test.
        /// </summary>
        private static CustomerUserLicenseUpdateOperations customerUserLicenseUpdateOperations;

        /// <summary>
        /// Ensures that customer user assigned licenses passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        [TestMethod]
        public void CustomerUserLicenseOperationsTests_VerifyCustomerUserAssignedLicensesCustomerIdNullFails()
        {
            try
            {
                customerUserLicenseOperations = new CustomerUserLicenseCollectionOperations(Mock.Of<IPartner>(), null, ExpectedUserId.ToString());
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("customerId must be set.", ex.Message);
            }
        }

        /// <summary>
        /// Ensures that customer user assigned licenses passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        [TestMethod]
        public void CustomerUserLicenseOperationsTests_VerifyCustomerUserAssignedLicensesUserIdNullFails()
        {
            try
            {
                customerUserLicenseOperations = new CustomerUserLicenseCollectionOperations(Mock.Of<IPartner>(), ExpectedCustomerId.ToString(), null);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("userId must be set.", ex.Message);
            }
        }

        /// <summary>
        /// Ensures that customer user assigned licenses passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerUserLicenseOperationsTests_VerifyCustomerUserAssignedLicenses()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerUserLicenseOperations = new CustomerUserLicenseCollectionOperations(mockPartnerOperations.Object, ExpectedCustomerId.ToString(), ExpectedUserId.ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<License, ResourceCollection<License>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<License, ResourceCollection<License>> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerUserAssignedLicenses.Path, ExpectedCustomerId.ToString(), ExpectedUserId.ToString()), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<License, ResourceCollection<License>>.AllInstances.GetAsync = (PartnerServiceProxy<License, ResourceCollection<License>> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;

                    // return the user object.
                    return Task.FromResult<ResourceCollection<License>>(null);
                };

                // call both sync and async versions of the get licenses API.
                await customerUserLicenseOperations.GetAsync();
                customerUserLicenseOperations.Get();

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that customer user assigned licenses passes in the right values to the proxy and actually calls the proxy for Group1 license group.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerUserLicenseOperationsTests_VerifyCustomerUserLicenseGroup1AssignedLicenses()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerUserLicenseOperations = new CustomerUserLicenseCollectionOperations(mockPartnerOperations.Object, ExpectedCustomerId.ToString(), ExpectedUserId.ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<License, ResourceCollection<License>>.AllInstances.GetAsync = (PartnerServiceProxy<License, ResourceCollection<License>> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;

                    // Verify that two uri parameter was added for Group1.
                    Assert.AreEqual(1, jsonProxy.UriParameters.Count);
                    Assert.IsTrue(jsonProxy.UriParameters.Contains(new KeyValuePair<string, string>("licenseGroupIds", LicenseGroupId.Group1.ToString())));

                    // return the user object.
                    return Task.FromResult<ResourceCollection<License>>(null);
                };

                // call both sync and async versions of the get licenses API.
                List<LicenseGroupId> groups = new List<LicenseGroupId>() { LicenseGroupId.Group1 };
                await customerUserLicenseOperations.GetAsync(groups);
                customerUserLicenseOperations.Get(groups);

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that customer user assigned licenses passes in the right values to the proxy and actually calls the proxy for Group2 license group.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerUserLicenseOperationsTests_VerifyCustomerUserLicenseGroup2AssignedLicenses()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerUserLicenseOperations = new CustomerUserLicenseCollectionOperations(mockPartnerOperations.Object, ExpectedCustomerId.ToString(), ExpectedUserId.ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<License, ResourceCollection<License>>.AllInstances.GetAsync = (PartnerServiceProxy<License, ResourceCollection<License>> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;

                    // Verify that two uri parameter was added for Group2.
                    Assert.AreEqual(1, jsonProxy.UriParameters.Count);
                    Assert.IsTrue(jsonProxy.UriParameters.Contains(new KeyValuePair<string, string>("licenseGroupIds", LicenseGroupId.Group2.ToString())));

                    // return the user object.
                    return Task.FromResult<ResourceCollection<License>>(null);
                };

                // call both sync and async versions of the get licenses API.
                List<LicenseGroupId> groups = new List<LicenseGroupId>() { LicenseGroupId.Group2 };
                await customerUserLicenseOperations.GetAsync(groups);
                customerUserLicenseOperations.Get(groups);

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that customer user assigned licenses passes in the right values to the proxy and actually calls the proxy for Group1 and Group2 license groups.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerUserLicenseOperationsTests_VerifyCustomerUserLicenseGroup1AndGroup2AssignedLicenses()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerUserLicenseOperations = new CustomerUserLicenseCollectionOperations(mockPartnerOperations.Object, ExpectedCustomerId.ToString(), ExpectedUserId.ToString());
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<License, ResourceCollection<License>>.AllInstances.GetAsync = (PartnerServiceProxy<License, ResourceCollection<License>> jsonProxy) =>
                {
                    // increment the number of the calls.
                    proxyGetAsyncCalls++;

                    // Verify that two uri parameters were added. One for Group1 and one for Group2.
                    Assert.AreEqual(2, jsonProxy.UriParameters.Count);
                    Assert.IsTrue(jsonProxy.UriParameters.Contains(new KeyValuePair<string, string>("licenseGroupIds", LicenseGroupId.Group1.ToString())));
                    Assert.IsTrue(jsonProxy.UriParameters.Contains(new KeyValuePair<string, string>("licenseGroupIds", LicenseGroupId.Group2.ToString())));

                    // return the user object.
                    return Task.FromResult<ResourceCollection<License>>(null);
                };

                // call both sync and async versions of the get licenses API.
                List<LicenseGroupId> groups = new List<LicenseGroupId>() { LicenseGroupId.Group1, LicenseGroupId.Group2 };
                await customerUserLicenseOperations.GetAsync(groups);
                customerUserLicenseOperations.Get(groups);

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyGetAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that assign licenses to customer user passes with the right values to the proxy and actually calls the proxy.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerUserLicenseOperationsTests_VerifyCustomerUserAssignLicenses()
        {
            string skuId = Guid.NewGuid().ToString();
            LicenseUpdate request = new LicenseUpdate();
            LicenseAssignment license = new LicenseAssignment();
            license.SkuId = skuId;
            license.ExcludedPlans = null;

            List<LicenseAssignment> licenseList = new List<LicenseAssignment>();
            licenseList.Add(license);
            request.LicensesToAssign = licenseList;

            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerUserLicenseUpdateOperations = new CustomerUserLicenseUpdateOperations(mockPartnerOperations.Object, ExpectedCustomerId.ToString(), ExpectedUserId.ToString());
            int proxyPostAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<LicenseUpdate, LicenseUpdate>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<LicenseUpdate, LicenseUpdate> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.SetCustomerUserLicenseUpdates.Path, ExpectedCustomerId.ToString(), ExpectedUserId.ToString()), resourcePath);
                };

                ShimPartnerServiceProxy<LicenseUpdate, LicenseUpdate>.AllInstances.PostAsyncT0 = (PartnerServiceProxy<LicenseUpdate, LicenseUpdate> jsonProxy, LicenseUpdate licenseRequest) =>
                {
                    // increment the number of the calls
                    proxyPostAsyncCalls++;

                    return Task.FromResult<LicenseUpdate>(null);
                };

                // call both sync and async versions of the assign licenses API.
                await customerUserLicenseUpdateOperations.CreateAsync(request);
                customerUserLicenseUpdateOperations.Create(request);

                // ensure the proxy GetAsync() was called twice.
                Assert.AreEqual(2, proxyPostAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }

        /// <summary>
        /// Ensures that assign customer user licenses fails when customer id or user id is null.
        /// network methods.
        /// </summary>
        [TestMethod]
        public void CustomerUserLicenseOperationsTests_VerifyCustomerUserAssignLicensesParametersNullFails()
        {
            try
            {
                customerUserLicenseOperations = new CustomerUserLicenseCollectionOperations(Mock.Of<IPartner>(), ExpectedCustomerId.ToString(), null);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("userId must be set.", ex.Message);
            }

            try
            {
                customerUserLicenseOperations = new CustomerUserLicenseCollectionOperations(Mock.Of<IPartner>(), null, ExpectedUserId.ToString());
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("customerId must be set.", ex.Message);
            }
        }
    }
}