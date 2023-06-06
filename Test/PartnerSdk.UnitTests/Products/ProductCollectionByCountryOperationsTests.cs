// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using Products;
    using Products.Fakes;
    using QualityTools.Testing.Fakes;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="ProductsCollectionByCountryOperations"/> class.
    /// </summary>
    [TestClass]
    public class ProductCollectionByCountryOperationsTests
    {
        /// <summary>
        /// The country used to build the operations object.
        /// </summary>
        private const string ExpectedCountry = "US";

        /// <summary>
        /// The product collection operations instance under test.
        /// </summary>
        private static ProductCollectionByCountryOperations productCollectionByCountryOperations;

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
            productCollectionByCountryOperations = new ProductCollectionByCountryOperations(Mock.Of<IPartner>(), ExpectedCountry);
        }

        /// <summary>
        /// Ensures that validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productCollectionByCountryOperations = new ProductCollectionByCountryOperations(Mock.Of<IPartner>(), invalidCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Country can't be empty", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productCollectionByCountryOperations = new ProductCollectionByCountryOperations(Mock.Of<IPartner>(), invalidCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Country has to be a 2 letter string", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the by targetView operations get the correct targetView value.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryOperationsTests_VerifyByTargetViewPassesCorrectValue()
        {
            var expectedTargetView = "1";

            using (ShimsContext.Create())
            {
                ShimProductCollectionByCountryByTargetViewOperations.ConstructorIPartnerStringString =
                    (ProductCollectionByCountryByTargetViewOperations operations, IPartner partnerOperations, string targetView, string country) =>
                    {
                        Assert.AreEqual(ExpectedCountry, country);
                        Assert.AreEqual(expectedTargetView, targetView);
                    };
                
                Assert.IsNotNull(productCollectionByCountryOperations.ByTargetView(expectedTargetView));
            }
        }

        /// <summary>
        /// Ensures that this class doesn't duplicate the validation that is already done by the operations constructor.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryOperationsTests_VerifyByTargetViewForwardsInvalidTargetViews()
        {
            IProductCollectionByCountryByTargetView emptyOperations = null;
            IProductCollectionByCountryByTargetView nullOperations = null;

            using (ShimsContext.Create())
            {
                ShimProductCollectionByCountryByTargetViewOperations.ConstructorIPartnerStringString =
                    (ProductCollectionByCountryByTargetViewOperations operations, IPartner partnerOperations, string targetView, string country) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperations = productCollectionByCountryOperations.ByTargetView(string.Empty);
                    nullOperations = productCollectionByCountryOperations.ByTargetView(null);
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
        public void ProductCollectionByCountryOperationsTests_VerifyByTargetViewThrowsExceptionForInvalidTargetViews()
        {
            var invalidTargetViews = new List<string>() { string.Empty, null };

            foreach (var invalidTargetView in invalidTargetViews)
            {
                try
                {
                    Assert.IsNotNull(productCollectionByCountryOperations.ByTargetView(invalidTargetView));
                    Assert.Fail("ByTargetView didn't throw exception for invalid targetView.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: targetView must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the product operations get the correct productId value.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryOperationsTests_VerifyByIdPassesCorrectValue()
        {
            var expectedProductId = "1";

            using (ShimsContext.Create())
            {
                ShimProductOperations.ConstructorIPartnerStringString =
                    (ProductOperations operations, IPartner partnerOperations, string productId, string country) =>
                    {
                        Assert.AreEqual(ExpectedCountry, country);
                        Assert.AreEqual(expectedProductId, productId);
                    };

                Assert.IsNotNull(productCollectionByCountryOperations.ById(expectedProductId));
                Assert.IsNotNull(productCollectionByCountryOperations[expectedProductId]);
            }
        }
        
        /// <summary>
        /// Ensures that no validation is done on the productId parameter at this level.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryOperationsTests_VerifyByIdForwardsInvalidProductIds()
        {
            IProduct emptyOperationsFromMethod = null;
            IProduct emptyOperationsFromIndex = null;
            IProduct nullOperationsFromMethod = null;
            IProduct nullOperationsFromIndex = null;

            using (ShimsContext.Create())
            {
                ShimProductOperations.ConstructorIPartnerStringString =
                    (ProductOperations operations, IPartner partnerOperations, string productId, string country) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperationsFromMethod = productCollectionByCountryOperations.ById(string.Empty);
                    nullOperationsFromMethod = productCollectionByCountryOperations.ById(null);
                    emptyOperationsFromIndex = productCollectionByCountryOperations[string.Empty];
                    nullOperationsFromIndex = productCollectionByCountryOperations[null];
                }
                catch (Exception)
                {
                    Assert.Fail("An exception shouldn't be explicitly thrown by this method.");
                }
            }
        }

        /// <summary>
        /// Ensures that validation is done by an operations class at another level.
        /// </summary>
        [TestMethod]
        public void ProductCollectionByCountryOperationsTests_VerifyByIdThrowsExceptionForInvalidProductIds()
        {
            var invalidIds = new List<string>() { string.Empty, null };

            foreach (var invalidId in invalidIds)
            {
                try
                {
                    Assert.IsNotNull(productCollectionByCountryOperations.ById(invalidId));
                    Assert.Fail("ById didn't throw exception for invalid productId.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: productId has to be set.", ex.Message);
                }

                try
                {
                    Assert.IsNotNull(productCollectionByCountryOperations[invalidId]);
                    Assert.Fail("ById didn't throw exception for invalid productId.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: productId has to be set.", ex.Message);
                }
            }
        }
    }
}
