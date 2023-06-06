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
    /// Unit tests for SubscriptionSupportContactTests
    /// </summary>
    [TestClass]
    public class SubscriptionSupportContactTests
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
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The subscription support contact operations.
        /// </summary>
        private static ISubscriptionSupportContact subscriptionSupportContactOperations;

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

            subscriptionSupportContactOperations = new SubscriptionSupportContactOperations(partnerOperations.Object, CustomerId, SubscriptionId);
        }

        /// <summary>
        /// Test Get Subscription Support Contact success path
        /// </summary>
        [TestMethod]
        public void SubscriptionSupportContactTests_GetSubscriptionSupportContact()
        {
            using (ShimsContext.Create())
            {
                var supportContact = new SupportContact()
                {
                    Name = "abc",
                    SupportMpnId = "12345",
                    SupportTenantId = Guid.NewGuid().ToString()
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<SupportContact, SupportContact>.AllInstances.GetAsync = jsonProxy =>
                {
                    // verify request properties
                    Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionSupportContact.Path, CustomerId, SubscriptionId), jsonProxy.ResourcePath);
                    
                    return Task.FromResult(supportContact);
                };

                SupportContact actualSupportContact = subscriptionSupportContactOperations.Get();
                Assert.IsNotNull(actualSupportContact, "actualSupportContact should not be empty or null");                
                Assert.AreEqual(supportContact.Name, actualSupportContact.Name);
                Assert.AreEqual(supportContact.SupportTenantId, actualSupportContact.SupportTenantId);
                Assert.AreEqual(supportContact.SupportMpnId, actualSupportContact.SupportMpnId);
            }
        }

        /// <summary>
        /// Test Update Subscription Support Contact success path
        /// </summary>
        [TestMethod]
        public void SubscriptionSupportContactTests_UpdateSubscriptionSupportContact()
        {
            using (ShimsContext.Create())
            {
                var supportContact = new SupportContact()
                {
                    Name = "abc",
                    SupportMpnId = "12345",
                    SupportTenantId = Guid.NewGuid().ToString()
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<SupportContact, SupportContact>.AllInstances.GetAsync = jsonProxy =>
                {
                    // verify request properties
                    Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpdateSubscriptionSupportContact.Path, CustomerId, SubscriptionId), jsonProxy.ResourcePath);
                    
                    return Task.FromResult(supportContact);
                };

                SupportContact actualSupportContact = subscriptionSupportContactOperations.Get();
                Assert.IsNotNull(actualSupportContact, "actualSupportContact should not be empty or null");
                Assert.AreEqual(supportContact.Name, actualSupportContact.Name);
                Assert.AreEqual(supportContact.SupportTenantId, actualSupportContact.SupportTenantId);
                Assert.AreEqual(supportContact.SupportMpnId, actualSupportContact.SupportMpnId);
            }
        }
    }
}
