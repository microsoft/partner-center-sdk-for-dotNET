// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations;
    using Microsoft.Store.PartnerCenter.NewCommerceMigrations;
    using Moq;
    using Network;
    using Network.Fakes;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="NewCommerceMigrationOperationsTests"/> class.
    /// </summary>
    [TestClass]
    public class NewCommerceMigrationOperationsTests
    {
        /// <summary>
        /// The Customer ID used to build the operations object.
        /// </summary>
        private const string ExpectedCustomerId = "4b0f2753-8b45-430e-8817-aec69478098d";

        /// <summary>
        /// The New-Commerce migration ID used to build the operations object.
        /// </summary>
        private const string ExpectedNewCommerceMigrationId = "41280f2a-85d4-4d76-bae4-df4fd18c720b";

        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The New-Commerce migration operations instance under test.
        /// </summary>
        private static NewCommerceMigrationOperations newCommerceMigrationOperations;

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
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

            newCommerceMigrationOperations = new NewCommerceMigrationOperations(partnerOperations.Object, ExpectedCustomerId, ExpectedNewCommerceMigrationId);
        }

        /// <summary>
        /// Get New-Commerce migration success path test.
        /// </summary>
        [TestMethod]
        public void GetNewCommerceMigrationTestVerifySuccessPath()
        {
            NewCommerceMigration expectedNewCommerceMigration = new NewCommerceMigration()
            {
                Id = ExpectedNewCommerceMigrationId,
            };

            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<NewCommerceMigration, NewCommerceMigration>.AllInstances.GetAsync
                    = (PartnerServiceProxy<NewCommerceMigration, NewCommerceMigration> jsonProxy) =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format("customers/{0}/migrations/newcommerce/{1}", ExpectedCustomerId, ExpectedNewCommerceMigrationId), jsonProxy.ResourcePath);

                        return Task.FromResult(expectedNewCommerceMigration);
                    };

                var newCommerceMigration = newCommerceMigrationOperations.Get();

                Assert.IsNotNull(newCommerceMigration);
                Assert.AreEqual(expectedNewCommerceMigration.Id, newCommerceMigration.Id);
            }
        }
    }
}
