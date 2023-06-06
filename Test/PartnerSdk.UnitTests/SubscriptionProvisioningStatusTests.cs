// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Models.Offers;
    using Models.Subscriptions;
    using Moq;
    using Network.Fakes;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using Subscriptions;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for Subscription Provisioning Status
    /// </summary>
    [TestClass]
    public class SubscriptionProvisioningStatusTests
    {
        /// <summary>
        /// Test customer id
        /// </summary>
        private static readonly string CustomerId = Guid.NewGuid().ToString();

        /// <summary>
        /// Test subscription id
        /// </summary>
        private static readonly string SubscriptionId = Guid.NewGuid().ToString();

        /// <summary>
        /// Test subscription provisioning status success
        /// </summary>
        private static readonly SubscriptionProvisioningStatus SubscriptionProvisioningStatusSuccess = new SubscriptionProvisioningStatus
        {
            SkuId = new Guid(),
            Quantity = 1,
            Status = ProvisioningStatus.Success,
            EndDate = new DateTime()
        };

        /// <summary>
        /// Test subscription provisioning status pending
        /// </summary>
        private static readonly SubscriptionProvisioningStatus SubscriptionProvisioningStatusPending = new SubscriptionProvisioningStatus
        {
            SkuId = new Guid(),
            Quantity = 1,
            Status = ProvisioningStatus.Pending,
            EndDate = new DateTime()
        };

        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The provisioning status operations.
        /// </summary>
        private static ISubscriptionProvisioningStatus subscriptionProvisioningStatusOperations;
       
        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            mockCredentials = new Mock<IPartnerCredentials>();
            mockCredentials.Setup(credentials => credentials.PartnerServiceToken).Returns("Fake Token");
            mockCredentials.Setup(credentials => credentials.ExpiresAt).Returns(DateTimeOffset.MaxValue);

            mockRequestContext = new Mock<IRequestContext>();
            mockRequestContext.Setup(context => context.CorrelationId).Returns(Guid.NewGuid());
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> partnerOperations = new Mock<IPartner>();
            partnerOperations.Setup(partner => partner.Credentials).Returns(mockCredentials.Object);
            partnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            subscriptionProvisioningStatusOperations = new SubscriptionProvisioningStatusOperations(partnerOperations.Object, CustomerId, SubscriptionId);
        }

        /// <summary>
        /// Ensures that customer user passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        [TestMethod]
        public void SubscriptionProvisioningTests_VerifyCustomerIdNullFails()
        {
            try
            {
                subscriptionProvisioningStatusOperations = new SubscriptionProvisioningStatusOperations(Mock.Of<IPartner>(), null, SubscriptionId);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("customerId must be set.", ex.Message);
            }
        }

        /// <summary>
        /// Ensures that customer user passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        [TestMethod]
        public void SubscriptionProvisioningTests_VerifySubscriptionIdNullFails()
        {
            try
            {
                subscriptionProvisioningStatusOperations = new SubscriptionProvisioningStatusOperations(Mock.Of<IPartner>(), CustomerId, null);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("subscriptionId must be set.", ex.Message);
            }
        }

        /// <summary>
        /// Test Get Subscription provisioning status success
        /// </summary>
        [TestMethod]
        public void SubscriptionProvisioningTests_GetSubscriptionProvisioningStatusSuccess()
        {
            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<SubscriptionProvisioningStatus, SubscriptionProvisioningStatus>.AllInstances.GetAsync = jsonProxy =>
                {
                    // verify request properties
                    Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionProvisioningStatus.Path, CustomerId, SubscriptionId), jsonProxy.ResourcePath);

                    return Task.FromResult(SubscriptionProvisioningStatusSuccess);
                };

                SubscriptionProvisioningStatus actualSubscriptionProvisioningStatus = subscriptionProvisioningStatusOperations.Get();

                Assert.IsNotNull(actualSubscriptionProvisioningStatus, "Subscription provisioning status details should not be empty or null");
                Assert.IsNotNull(actualSubscriptionProvisioningStatus.SkuId, "SkuId should not be empty or null");
                Assert.IsNotNull(actualSubscriptionProvisioningStatus.EndDate, "EndDate should not be empty or null");
                Assert.IsNotNull(actualSubscriptionProvisioningStatus.Status, "Status should not be empty or null");
                Assert.IsNotNull(actualSubscriptionProvisioningStatus.Quantity, "Quatity should not be empty or null");
                Assert.AreEqual(actualSubscriptionProvisioningStatus.Status, ProvisioningStatus.Success);
            }
        }

        /// <summary>
        /// Test Get Subscription provisioning status pending
        /// </summary>
        [TestMethod]
        public void SubscriptionProvisioningTests_GetSubscriptionProvisioningStatusPending()
        {
            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<SubscriptionProvisioningStatus, SubscriptionProvisioningStatus>.AllInstances.GetAsync = jsonProxy =>
                {
                    // verify request properties
                    Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionProvisioningStatus.Path, CustomerId, SubscriptionId), jsonProxy.ResourcePath);

                    return Task.FromResult(SubscriptionProvisioningStatusPending);
                };

                SubscriptionProvisioningStatus actualSubscriptionProvisioningStatus = subscriptionProvisioningStatusOperations.Get();

                Assert.IsNotNull(actualSubscriptionProvisioningStatus, "Subscription provisioning status details should not be empty or null");
                Assert.IsNotNull(actualSubscriptionProvisioningStatus.SkuId, "SkuId should not be empty or null");
                Assert.IsNotNull(actualSubscriptionProvisioningStatus.EndDate, "EndDate should not be empty or null");
                Assert.IsNotNull(actualSubscriptionProvisioningStatus.Status, "Status should not be empty or null");
                Assert.IsNotNull(actualSubscriptionProvisioningStatus.Quantity, "Quatity should not be empty or null");
                Assert.AreEqual(actualSubscriptionProvisioningStatus.Status, ProvisioningStatus.Pending);
            }
        }
    }
}