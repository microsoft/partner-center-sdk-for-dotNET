// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Models.Products;
    using Moq;
    using Network;
    using Network.Fakes;
    using Products;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="ProductPromotionCollectionByCountryBySegmentOperations"/> class.
    /// </summary>
    [TestClass]
    public class ProductPromotionCollectionByCountryBySegmentOperationsTests
    {
        /// <summary>
        /// The country used to build the operations object.
        /// </summary>
        private const string ExpectedCountry = "US";

        /// <summary>
        /// The segment used to build the operations object.
        /// </summary>
        private const string ExpectedSegment = "commercial";

        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The product promotion collection by country by segment operations instance under test.
        /// </summary>
        private static ProductPromotionCollectionByCountryBySegmentOperations productPromotionOperations;

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

            productPromotionOperations = new ProductPromotionCollectionByCountryBySegmentOperations(partnerOperations.Object, ExpectedSegment, ExpectedCountry);
        }

        /// <summary>
        /// Get ProductPromotion success path tests.
        /// </summary>
        [TestMethod]
        public void ProductPromotionCollectionByCountryBySegmentOperationsTests_GetProductPromotionsTestVerifySuccessPath()
        {
            var productPromotionList = new List<ProductPromotion>()
            {
                new ProductPromotion()
                {
                    Id = "1",
                    Name = "Name1"
                },
                new ProductPromotion()
                {
                    Id = "2",
                    Name = "Name2"
                }
            };
            var expectedProductPromotions = new ResourceCollection<ProductPromotion>(productPromotionList);

            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<ProductPromotion, ResourceCollection<ProductPromotion>>.AllInstances.GetAsync
                    = (PartnerServiceProxy<ProductPromotion, ResourceCollection<ProductPromotion>> jsonProxy) =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual("productpromotions", jsonProxy.ResourcePath);                        
                        Assert.AreEqual(2, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("segment", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedSegment, jsonProxy.UriParameters.First().Value);
                        Assert.AreEqual("country", jsonProxy.UriParameters.ElementAt(1).Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.ElementAt(1).Value);

                        return Task.FromResult(expectedProductPromotions);
                    };

                var productPromotions = productPromotionOperations.Get();

                Assert.IsNotNull(productPromotions);
                Assert.AreEqual(expectedProductPromotions.TotalCount, productPromotions.TotalCount);
                for (int i = 0; i < expectedProductPromotions.TotalCount; i++)
                {
                    Assert.AreEqual(expectedProductPromotions.Items.ElementAt(i).Id, productPromotions.Items.ElementAt(i).Id);
                    Assert.AreEqual(expectedProductPromotions.Items.ElementAt(i).Name, productPromotions.Items.ElementAt(i).Name);
                }
            }
        }

        /// <summary>
        /// Ensures that country validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductPromotionCollectionByCountryBySegmentOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productPromotionOperations = new ProductPromotionCollectionByCountryBySegmentOperations(Mock.Of<IPartner>(), ExpectedSegment, invalidCountry);
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
        public void ProductCollectionByCountryByTargetViewOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productPromotionOperations = new ProductPromotionCollectionByCountryBySegmentOperations(Mock.Of<IPartner>(), ExpectedSegment, invalidCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Country has to be a 2 letter string", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that segment validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductPromotionCollectionByCountryBySegmentOperationsTests_VerifyConstructorValidatesSegmentParameter()
        {
            var invalidSegments = new List<string>() { string.Empty, null };

            foreach (var invalidSegment in invalidSegments)
            {
                try
                {
                    productPromotionOperations = new ProductPromotionCollectionByCountryBySegmentOperations(Mock.Of<IPartner>(), invalidSegment, ExpectedCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid segment.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: segment must be set", ex.Message);
                }
            }
        }
    }
}
