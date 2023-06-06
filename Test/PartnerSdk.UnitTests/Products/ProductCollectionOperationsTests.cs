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
    /// Tests the <see cref="ProductsCollectionOperations"/> class.
    /// </summary>
    [TestClass]
    public class ProductCollectionOperationsTests
    {
        /// <summary>
        /// The product collection operations instance under test.
        /// </summary>
        private static ProductCollectionOperations productCollectionOperations;

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
            productCollectionOperations = new ProductCollectionOperations(Mock.Of<IPartner>());
        }

        /// <summary>
        /// Ensures that the by country operations get the correct country value.
        /// </summary>
        [TestMethod]
        public void ProductCollectionOperationsTests_VerifyByCountryPassesCorrectValue()
        {
            var expectedCountry = "US";

            using (ShimsContext.Create())
            {
                ShimProductCollectionByCountryOperations.ConstructorIPartnerString =
                    (ProductCollectionByCountryOperations operations, IPartner partnerOperations, string country) =>
                    {
                        Assert.AreEqual(expectedCountry, country);
                    };
                
                Assert.IsNotNull(productCollectionOperations.ByCountry(expectedCountry));
            }
        }

        /// <summary>
        /// Ensures that this class doesn't duplicate the validation that is already done by the operations constructor.
        /// </summary>
        [TestMethod]
        public void ProductCollectionOperationsTests_VerifyByCountryForwardsInvalidCountries()
        {
            IProductCollectionByCountry emptyOperations = null;
            IProductCollectionByCountry nullOperations = null;

            using (ShimsContext.Create())
            {
                ShimProductCollectionByCountryOperations.ConstructorIPartnerString =
                    (ProductCollectionByCountryOperations operations, IPartner partnerOperations, string country) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperations = productCollectionOperations.ByCountry(string.Empty);
                    nullOperations = productCollectionOperations.ByCountry(null);
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
        public void ProductCollectionOperationsTests_VerifyByCountryThrowsExceptionForEmptyCountries()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    Assert.IsNotNull(productCollectionOperations.ByCountry(invalidCountry));
                    Assert.Fail("ByCountry didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Country can't be empty", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that validation is done by an operations class at another level.
        /// </summary>
        [TestMethod]
        public void ProductCollectionOperationsTests_VerifyByCountryThrowsExceptionForInvalidCountries()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    Assert.IsNotNull(productCollectionOperations.ByCountry(invalidCountry));
                    Assert.Fail("ByCountry didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Country has to be a 2 letter string", ex.Message);
                }
            }
        }
    }
}
