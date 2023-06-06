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
    /// Unit tests for SubscriptionConversionOperations
    /// </summary>
    [TestClass]
    public class SubscriptionConversionTests
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
        /// Test subscription conversion (disabled)
        /// </summary>
        private static readonly Conversion SubscriptionConversion = new Conversion
        {
            OfferId = Guid.NewGuid().ToString(),
            TargetOfferId = Guid.NewGuid().ToString(),
            OrderId = Guid.NewGuid().ToString(),
            BillingCycle = BillingCycleType.None,
            Quantity = 25,
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
        /// The invoice collection operations.
        /// </summary>
        private static ISubscriptionConversionCollection subscriptionConversionOperations;

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

            subscriptionConversionOperations = new SubscriptionConversionCollectionOperations(partnerOperations.Object, CustomerId, SubscriptionId);
        }

        /// <summary>
        /// Test Get Subscription Upgrades success path
        /// </summary>
        [TestMethod]
        public void SubscriptionConversionTests_GetSubscriptionConversions()
        {
            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Conversion, ResourceCollection<Conversion>>.AllInstances.GetAsync = jsonProxy =>
                {
                    // verify request properties
                    Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionConversions.Path, CustomerId, SubscriptionId), jsonProxy.ResourcePath);

                    return Task.FromResult(new ResourceCollection<Conversion>(new List<Conversion> { SubscriptionConversion }));
                };

                ResourceCollection<Conversion> actualSubscriptionConversions = subscriptionConversionOperations.Get();
                Assert.IsNotNull(actualSubscriptionConversions, "actualSubscriptionConversion should not be empty or null");
                Assert.AreEqual(1, actualSubscriptionConversions.TotalCount, "The total number of conversions should be 1.");

                Conversion actualSubscriptionConversion = actualSubscriptionConversions.Items.ToList()[0];
                Assert.AreEqual(SubscriptionConversion.TargetOfferId, actualSubscriptionConversion.TargetOfferId);
                Assert.AreEqual(SubscriptionConversion.OrderId, actualSubscriptionConversion.OrderId);
                Assert.AreEqual(SubscriptionConversion.OfferId, actualSubscriptionConversion.OfferId);
                Assert.AreEqual(SubscriptionConversion.Quantity, actualSubscriptionConversion.Quantity);
                Assert.AreEqual(SubscriptionConversion.BillingCycle, actualSubscriptionConversion.BillingCycle);
            }
        }

        /// <summary>
        /// Test upgrade subscription success path
        /// </summary>
        /// <returns>Task for the test</returns>
        [TestMethod]
        public async Task SubscriptionConversionTests_ConvertSubscription()
        {
            using (ShimsContext.Create())
            {
                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Conversion, ConversionResult>.AllInstances.PostAsyncT0 =
                    (jsonProxy, conversion) =>
                    {
                        Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.PostSubscriptionConversion.Path, CustomerId, SubscriptionId), jsonProxy.ResourcePath);

                        return Task.FromResult(new ConversionResult
                        {
                            OfferId = conversion.OfferId,
                            SubscriptionId = Guid.NewGuid().ToString(),
                            TargetOfferId = conversion.TargetOfferId,
                            Error = null,
                        });
                    };

                ConversionResult conversionResult = await subscriptionConversionOperations.CreateAsync(SubscriptionConversion);
                Assert.IsNotNull(conversionResult, "conversionResult should not be empty or null");       
                Assert.AreEqual(conversionResult.OfferId, SubscriptionConversion.OfferId);
                Assert.AreEqual(conversionResult.TargetOfferId, SubscriptionConversion.TargetOfferId);
                Assert.AreEqual(conversionResult.Error, null);
            }
        }
    }
}
