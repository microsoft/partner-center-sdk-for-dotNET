// -----------------------------------------------------------------------
// <copyright file="ProductCollectionByCountryByCollectionIdOperationsTests.cs" company="Microsoft">
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
    using Products.Fakes;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="ProductCollectionByCountryByCollectionIdOperations"/> class.
    /// </summary>
    [TestClass]
    public class ProductCollectionByCountryByCollectionIdOperationsTests
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
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The product collection by country by collection id operations instance under test.
        /// </summary>
        private static ProductCollectionByCountryByCollectionIdOperations productOperations;

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

            productOperations = new ProductCollectionByCountryByCollectionIdOperations(partnerOperations.Object, ExpectedCollectionId, ExpectedCountry);
        }

        /// <summary>
        /// Get Product success path tests.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryByCollectionIdOperationsTests_GetProductsTestVerifySuccessPath()
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
                        Assert.AreEqual(2, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("collectionId", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedCollectionId, jsonProxy.UriParameters.First().Value);
                        Assert.AreEqual("country", jsonProxy.UriParameters.ElementAt(1).Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.ElementAt(1).Value);

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
        public void ProductCollectionByCountryByCollectionIdOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productOperations = new ProductCollectionByCountryByCollectionIdOperations(Mock.Of<IPartner>(), ExpectedCollectionId, invalidCountry);
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
        public void ProductCollectionByCountryByCollectionIdOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productOperations = new ProductCollectionByCountryByCollectionIdOperations(Mock.Of<IPartner>(), ExpectedCollectionId, invalidCountry);
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
        public void ProductCollectionByCountryByCollectionIdOperationsTests_VerifyConstructorValidatesCollectionIdParameter()
        {
            var invalidCollectionIds = new List<string>() { string.Empty, null };

            foreach (var invalidCollectionId in invalidCollectionIds)
            {
                try
                {
                    productOperations = new ProductCollectionByCountryByCollectionIdOperations(Mock.Of<IPartner>(), invalidCollectionId, ExpectedCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: collectionId must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the by target segment operations get the correct segment value.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryByCollectionIdOperationsTests_VerifyByTargetSegmentPassesCorrectValue()
        {
            var expectedSegmentId = "3";

            using (ShimsContext.Create())
            {
                ShimProductCollectionByCountryByCollectionIdByTargetSegmentOperations.ConstructorIPartnerStringStringString =
                    (ProductCollectionByCountryByCollectionIdByTargetSegmentOperations operations, IPartner partnerOperations, string collectionId, string country, string targetSegment) =>
                    {
                        Assert.AreEqual(ExpectedCountry, country);
                        Assert.AreEqual(ExpectedCollectionId, collectionId);
                        Assert.AreEqual(expectedSegmentId, targetSegment);
                    };

                Assert.IsNotNull(productOperations.ByTargetSegment(expectedSegmentId));
            }
        }

        /// <summary>
        /// Ensures that this class doesn't duplicate the validation that is already done by the operations constructor.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryByCollectionIdOperationsTests_VerifyByTargetSegmentForwardsInvalidSegments()
        {
            IProductCollectionByCountryByCollectionIdByTargetSegment emptyOperations = null;
            IProductCollectionByCountryByCollectionIdByTargetSegment nullOperations = null;

            using (ShimsContext.Create())
            {
                ShimProductCollectionByCountryByCollectionIdByTargetSegmentOperations.ConstructorIPartnerStringStringString =
                    (ProductCollectionByCountryByCollectionIdByTargetSegmentOperations operations, IPartner partnerOperations, string collectionId, string country, string targetSegment) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperations = productOperations.ByTargetSegment(string.Empty);
                    nullOperations = productOperations.ByTargetSegment(null);
                }
                catch (Exception ex)
                {
                    Assert.Fail(string.Format("An exception shouldn't be explicitly thrown by this method. Message: {0}", ex.Message));
                }

                // Ensure that it doesn't fail by returning a null object.
                Assert.IsNotNull(emptyOperations);
                Assert.IsNotNull(nullOperations);
            }
        }

        /// <summary>
        /// Ensures that validation is done by an operations class at another level.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryByCollectionIdOperationsTests_VerifyByTargetSegmentThrowsExceptionForInvalidSegments()
        {
            var invalidSegments = new List<string>() { string.Empty, null };

            foreach (var invalidSegment in invalidSegments)
            { 
                try
                {
                    Assert.IsNotNull(productOperations.ByTargetSegment(invalidSegment));
                    Assert.Fail("ByTargetSegment didn't throw exception for invalid target segment.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: targetSegment must be set", ex.Message);
                }
            }
        }
    }
}
