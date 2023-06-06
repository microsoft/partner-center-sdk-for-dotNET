// -----------------------------------------------------------------------
// <copyright file="ProductCollectionByCountryByCollectionIdByTargetSegmentOperationsTests.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

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
    /// Tests the <see cref="ProductCollectionByCountryByCollectionIdByTargetSegmentOperations"/> class.
    /// </summary>
    [TestClass]
    public class ProductCollectionByCountryByCollectionIdByTargetSegmentOperationsTests
    {
        /// <summary>
        /// The country used to build the operations object.
        /// </summary>
        private const string ExpectedCountry = "US";

        /// <summary>
        /// The collectionId used to build the operations object.
        /// </summary>
        private const string ExpectedCollectionId = "software";

        /// <summary>
        /// The segment used to build the operations object.
        /// </summary>
        private const string ExpectedTargetSegment = "commercial";

        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The product collection by country by collection id by target segment operations instance under test.
        /// </summary>
        private static ProductCollectionByCountryByCollectionIdByTargetSegmentOperations productOperations;

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

            productOperations = new ProductCollectionByCountryByCollectionIdByTargetSegmentOperations(partnerOperations.Object, ExpectedCollectionId, ExpectedCountry, ExpectedTargetSegment);
        }

        /// <summary>
        /// Get Product success path tests.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryByCollectionIdByTargetSegmentOperationsTests_GetProductsTestVerifySuccessPath()
        {
            var productList = new List<Product>()
            {
                new Product()
                {
                    Id = "1",
                    Title = "Title1"
                },
                new Product()
                {
                    Id = "2",
                    Title = "Title2"
                }
            };
            var expectedProducts = new ResourceCollection<Product>(productList);

            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<Product, ResourceCollection<Product>>.AllInstances.GetAsync
                    = (PartnerServiceProxy<Product, ResourceCollection<Product>> jsonProxy) =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual("products", jsonProxy.ResourcePath);                        
                        Assert.AreEqual(3, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("collectionId", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedCollectionId, jsonProxy.UriParameters.First().Value);
                        Assert.AreEqual("country", jsonProxy.UriParameters.ElementAt(1).Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.ElementAt(1).Value);
                        Assert.AreEqual("targetSegment", jsonProxy.UriParameters.ElementAt(2).Key);
                        Assert.AreEqual(ExpectedTargetSegment, jsonProxy.UriParameters.ElementAt(2).Value);

                        return Task.FromResult<ResourceCollection<Product>>(expectedProducts);
                    };

                var products = productOperations.Get();

                Assert.IsNotNull(products);
                Assert.AreEqual(expectedProducts.TotalCount, products.TotalCount);
                for (int i = 0; i < expectedProducts.TotalCount; i++)
                {
                    Assert.AreEqual(expectedProducts.Items.ElementAt(i).Id, products.Items.ElementAt(i).Id);
                    Assert.AreEqual(expectedProducts.Items.ElementAt(i).Title, products.Items.ElementAt(i).Title);
                }
            }
        }

        /// <summary>
        /// Ensures that country validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryByCollectionIdByTargetSegmentOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productOperations = new ProductCollectionByCountryByCollectionIdByTargetSegmentOperations(Mock.Of<IPartner>(), ExpectedCollectionId, invalidCountry, ExpectedTargetSegment);
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
        public void ProductCollectionByCountryByCollectionIdByTargetSegmentOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productOperations = new ProductCollectionByCountryByCollectionIdByTargetSegmentOperations(Mock.Of<IPartner>(), ExpectedCollectionId, invalidCountry, ExpectedTargetSegment);
                    Assert.Fail("Constructor didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Country has to be a 2 letter string", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that collectionId validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryByCollectionIdByTargetSegmentOperationsTests_VerifyConstructorValidatesCollectionIdParameter()
        {
            var invalidCollectionIds = new List<string>() { string.Empty, null };

            foreach (var invalidCollectionId in invalidCollectionIds)
            {
                try
                {
                    productOperations = new ProductCollectionByCountryByCollectionIdByTargetSegmentOperations(Mock.Of<IPartner>(), invalidCollectionId, ExpectedCountry, ExpectedTargetSegment);
                    Assert.Fail("Constructor didn't throw exception for invalid collectionId.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: collectionId must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that target segment validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryByCollectionIdByTargetSegmentOperationsTests_VerifyConstructorValidatesTargetSegmentParameter()
        {
            var invalidSegments = new List<string>() { string.Empty, null };

            foreach (var invalidSegment in invalidSegments)
            {
                try
                {
                    productOperations = new ProductCollectionByCountryByCollectionIdByTargetSegmentOperations(Mock.Of<IPartner>(), ExpectedCollectionId, ExpectedCountry, invalidSegment);
                    Assert.Fail("Constructor didn't throw exception for invalid target segment.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: targetSegment must be set", ex.Message);
                }
            }
        }
    }
}
