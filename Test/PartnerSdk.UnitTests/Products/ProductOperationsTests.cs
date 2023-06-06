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
    /// Tests the <see cref="ProductsOperations"/> class.
    /// </summary>
    [TestClass]
    public class ProductOperationsTests
    {
        /// <summary>
        /// The country used to build the operations object.
        /// </summary>
        private const string ExpectedCountry = "US";

        /// <summary>
        /// The productId used to build the operations object.
        /// </summary>
        private const string ExpectedProductId = "1";

        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The product operations instance under test.
        /// </summary>
        private static ProductOperations productOperations;

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

            productOperations = new ProductOperations(partnerOperations.Object, ExpectedProductId, ExpectedCountry);
        }

        /// <summary>
        /// Get Product success path tests.
        /// </summary>
        [TestMethod]
        public void ProductOperationsTests_GetProductTestVerifySuccessPath()
        {
            Product expectedProduct = new Product()
            {
                Id = ExpectedProductId,
                Title = "Title"
            };

            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<Product, Product>.AllInstances.GetAsync
                    = (PartnerServiceProxy<Product, Product> jsonProxy) =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format("products/{0}", ExpectedProductId), jsonProxy.ResourcePath);                        
                        Assert.AreEqual(1, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("country", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.First().Value);

                        return Task.FromResult<Product>(expectedProduct);
                    };

                var product = productOperations.Get();

                Assert.IsNotNull(product);
                Assert.AreEqual(expectedProduct.Id, product.Id);
                Assert.AreEqual(expectedProduct.Title, product.Title);
            }
        }

        /// <summary>
        /// Ensures that empty country validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productOperations = new ProductOperations(Mock.Of<IPartner>(), ExpectedProductId, invalidCountry);
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
        public void ProductOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productOperations = new ProductOperations(Mock.Of<IPartner>(), ExpectedProductId, invalidCountry);
                    Assert.Fail("Product operations constructor didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Country has to be a 2 letter string", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that productId validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductOperationsTests_VerifyConstructorValidatesProductIdParameter()
        {
            var invalidProductIds = new List<string>() { string.Empty, null };

            foreach (var invalidProductId in invalidProductIds)
            {
                try
                {
                    productOperations = new ProductOperations(Mock.Of<IPartner>(), invalidProductId, ExpectedCountry);
                    Assert.Fail("Product operations constructor didn't throw exception for invalid productId.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: productId has to be set.", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the sku operations are not null.
        /// </summary>
        [TestMethod]
        public void ProductOperationsTests_VerifySkuOperationsNotNull()
        {
            Assert.IsNotNull(productOperations.Skus);
        }
    }
}
