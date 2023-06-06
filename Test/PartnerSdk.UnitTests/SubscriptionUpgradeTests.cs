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
    /// Unit tests for SubscriptionUpgradeOperations
    /// </summary>
    [TestClass]
    public class SubscriptionUpgradeTests
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
        /// Test subscription upgrade (disabled)
        /// </summary>
        private static readonly Upgrade SubscriptionUpgrade = new Upgrade
        {
            IsEligible = false,
            UpgradeType = UpgradeType.UpgradeOnly,
            TargetOffer = new Offer
            {
                Id = Guid.NewGuid().ToString(),
                MinimumQuantity = 0,
                MaximumQuantity = 30,
                Name = "Test Offer"
            },
            UpgradeErrors = new List<UpgradeError>
            {
                new UpgradeError
                {
                    Code = UpgradeErrorCode.DelegatedAdminPermissionsDisabled,
                    Description = "Delegated Administrator Permissions are disabled",
                    AdditionalDetails = "Some more details"
                }
            }
        };

        /// <summary>
        /// Test subscription upgrade with license transfer
        /// </summary>
        private static readonly Upgrade SubscriptionUpgradeWithLicense = new Upgrade
        {
            IsEligible = true,
            UpgradeType = UpgradeType.UpgradeWithLicenseTransfer,
            TargetOffer = new Offer
            {
                Id = Guid.NewGuid().ToString(),
                MinimumQuantity = 1,
                MaximumQuantity = 3,
                Name = "Another Test Offer"
            }
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
        private static ISubscriptionUpgradeCollection subscriptionUpgradeOperations;

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

            subscriptionUpgradeOperations = new SubscriptionUpgradeCollectionOperations(partnerOperations.Object, CustomerId, SubscriptionId);
        }

        /// <summary>
        /// Test Get Subscription Upgrades success path
        /// </summary>
        [TestMethod]
        public void SubscriptionUpgradeTests_GetSubscriptionUpgrades()
        {
            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Upgrade, ResourceCollection<Upgrade>>.AllInstances.GetAsync = jsonProxy =>
                    {
                        // verify request properties
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionUpgrades.Path, CustomerId, SubscriptionId), jsonProxy.ResourcePath);

                        return Task.FromResult(new ResourceCollection<Upgrade>(new List<Upgrade> { SubscriptionUpgrade, SubscriptionUpgradeWithLicense }));
                    };

                ResourceCollection<Upgrade> actualSubscriptionUpgrades = subscriptionUpgradeOperations.Get();
                Assert.IsNotNull(actualSubscriptionUpgrades, "SubscriptionUpgrades should not be empty or null");
                foreach (Upgrade actualSubscriptionUpgrade in actualSubscriptionUpgrades.Items)
                {
                    var expectedSubscriptionUpgrade = actualSubscriptionUpgrade.UpgradeType == UpgradeType.UpgradeWithLicenseTransfer ? SubscriptionUpgradeWithLicense : SubscriptionUpgrade;

                    Assert.AreEqual(expectedSubscriptionUpgrade.IsEligible, actualSubscriptionUpgrade.IsEligible);
                    Assert.AreEqual(expectedSubscriptionUpgrade.UpgradeType, actualSubscriptionUpgrade.UpgradeType);
                    Assert.AreEqual(expectedSubscriptionUpgrade.TargetOffer.Id, actualSubscriptionUpgrade.TargetOffer.Id);
                    Assert.AreEqual(expectedSubscriptionUpgrade.TargetOffer.Name, actualSubscriptionUpgrade.TargetOffer.Name);
                    Assert.AreEqual(expectedSubscriptionUpgrade.TargetOffer.MinimumQuantity, actualSubscriptionUpgrade.TargetOffer.MinimumQuantity);
                    Assert.AreEqual(expectedSubscriptionUpgrade.TargetOffer.MaximumQuantity, actualSubscriptionUpgrade.TargetOffer.MaximumQuantity);
                    Assert.AreEqual(expectedSubscriptionUpgrade.UpgradeErrors, actualSubscriptionUpgrade.UpgradeErrors);

                    if (expectedSubscriptionUpgrade.UpgradeErrors == null || !expectedSubscriptionUpgrade.UpgradeErrors.Any())
                    {
                        continue;
                    }

                    Assert.AreEqual(expectedSubscriptionUpgrade.UpgradeErrors.First().Code, actualSubscriptionUpgrade.UpgradeErrors.First().Code);
                    Assert.AreEqual(expectedSubscriptionUpgrade.UpgradeErrors.First().Description, actualSubscriptionUpgrade.UpgradeErrors.First().Description);
                    Assert.AreEqual(expectedSubscriptionUpgrade.UpgradeErrors.First().AdditionalDetails, actualSubscriptionUpgrade.UpgradeErrors.First().AdditionalDetails);
                }
            }
        }

        /// <summary>
        /// Test upgrade subscription success path
        /// </summary>
        /// <returns>Task for the test</returns>
        [TestMethod]
        public async Task SubscriptionUpgradeTests_UpgradeSubscription()
        {
            using (ShimsContext.Create())
            {
                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Upgrade, UpgradeResult>.AllInstances.PostAsyncT0 =
                    (jsonProxy, upgrade) =>
                        Task.FromResult(new UpgradeResult
                        {
                            UpgradeType = upgrade.UpgradeType,
                            SourceSubscriptionId = "source",
                            TargetSubscriptionId = "target",
                            UpgradeErrors = new List<UpgradeError>(),
                            LicenseErrors = new List<UserLicenseError>()
                        });

                UpgradeResult upgradeResult = await subscriptionUpgradeOperations.CreateAsync(SubscriptionUpgradeWithLicense);
                Assert.IsNotNull(upgradeResult, "UpgradeResult should not be empty or null");
                Assert.IsNotNull(upgradeResult.SourceSubscriptionId, "UpgradeResult SourceSubscriptionId should not be empty or null");
                Assert.IsNotNull(upgradeResult.TargetSubscriptionId, "UpgradeResult TargetSubscriptionId should not be empty or null");
                Assert.IsTrue(upgradeResult.UpgradeType == SubscriptionUpgradeWithLicense.UpgradeType, "UpgradeResult UpgradeType should be with License Transfer");
                Assert.IsFalse(upgradeResult.UpgradeErrors.Any(), "UpgradeResult UpgradeErrors should be empty or null");
                Assert.IsFalse(upgradeResult.LicenseErrors.Any(), "UpgradeResult LicenseErrors should be empty or null");
            }
        }
    }
}
