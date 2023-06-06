// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models.Products;
    using Moq;
    using Network;
    using Network.Fakes;
    using Products;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="ProductPromotionsOperations"/> class.
    /// </summary>
    [TestClass]
    public class ProductPromotionOperationsTests
    {
        /// <summary>
        /// The country used to build the operations object.
        /// </summary>
        private const string ExpectedCountry = "US";

        /// <summary>
        /// The productPromotionId used to build the operations object.
        /// </summary>
        private const string ExpectedProductPromotionId = "1";

        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The product promotion operations instance under test.
        /// </summary>
        private static ProductPromotionOperations productPromotionOperations;

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

            productPromotionOperations = new ProductPromotionOperations(partnerOperations.Object, ExpectedProductPromotionId, ExpectedCountry);
        }

        /// <summary>
        /// Get ProductPromotion success path tests.
        /// </summary>
        [TestMethod]
        public void ProductPromotionOperationsTests_GetProductPromotionTestVerifySuccessPath()
        {
            ProductPromotion expectedProductPromotion = new ProductPromotion()
            {
                Id = ExpectedProductPromotionId,
                Name = "Name"
            };

            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<ProductPromotion, ProductPromotion>.AllInstances.GetAsync
                    = (PartnerServiceProxy<ProductPromotion, ProductPromotion> jsonProxy) =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format("productpromotions/{0}", ExpectedProductPromotionId), jsonProxy.ResourcePath);                        
                        Assert.AreEqual(1, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("country", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.First().Value);

                        return Task.FromResult(expectedProductPromotion);
                    };

                var productPromotion = productPromotionOperations.Get();

                Assert.IsNotNull(productPromotion);
                Assert.AreEqual(expectedProductPromotion.Id, productPromotion.Id);
                Assert.AreEqual(expectedProductPromotion.Name, productPromotion.Name);
            }
        }

        /// <summary>
        /// Ensures that empty country validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductPromotionOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productPromotionOperations = new ProductPromotionOperations(Mock.Of<IPartner>(), ExpectedProductPromotionId, invalidCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Country can't be empty", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that country validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductPromotionOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productPromotionOperations = new ProductPromotionOperations(Mock.Of<IPartner>(), ExpectedProductPromotionId, invalidCountry);
                    Assert.Fail("Product promotion operations constructor didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Country has to be a 2 letter string", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that productPromotionId validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductPromotionOperationsTests_VerifyConstructorValidatesProductPromotionIdParameter()
        {
            var invalidProductPromotionIds = new List<string>() { string.Empty, null };

            foreach (var invalidProductPromotionId in invalidProductPromotionIds)
            {
                try
                {
                    productPromotionOperations = new ProductPromotionOperations(Mock.Of<IPartner>(), invalidProductPromotionId, ExpectedCountry);
                    Assert.Fail("Product promotion operations constructor didn't throw exception for invalid productPromotionId.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: productPromotionId has to be set.", ex.Message);
                }
            }
        }
    }
}
