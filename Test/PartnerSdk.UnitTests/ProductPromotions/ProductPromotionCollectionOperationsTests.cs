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
    /// Tests the <see cref="ProductPromotionsCollectionOperations"/> class.
    /// </summary>
    [TestClass]
    public class ProductPromotionCollectionOperationsTests
    {
        /// <summary>
        /// The product promotion collection operations instance under test.
        /// </summary>
        private static ProductPromotionCollectionOperations productPromotionCollectionOperations;

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
            productPromotionCollectionOperations = new ProductPromotionCollectionOperations(Mock.Of<IPartner>());
        }

        /// <summary>
        /// Ensures that the by country operations get the correct country value.
        /// </summary>
        [TestMethod]
        public void ProductPromotionCollectionOperationsTests_VerifyByCountryPassesCorrectValue()
        {
            var expectedCountry = "US";

            using (ShimsContext.Create())
            {
                ShimProductPromotionCollectionByCountryOperations.ConstructorIPartnerString =
                    (ProductPromotionCollectionByCountryOperations operations, IPartner partnerOperations, string country) =>
                    {
                        Assert.AreEqual(expectedCountry, country);
                    };
                
                Assert.IsNotNull(productPromotionCollectionOperations.ByCountry(expectedCountry));
            }
        }

        /// <summary>
        /// Ensures that this class doesn't duplicate the validation that is already done by the operations constructor.
        /// </summary>
        [TestMethod]
        public void ProductPromotionCollectionOperationsTests_VerifyByCountryForwardsInvalidCountries()
        {
            IProductPromotionCollectionByCountry emptyOperations = null;
            IProductPromotionCollectionByCountry nullOperations = null;

            using (ShimsContext.Create())
            {
                ShimProductPromotionCollectionByCountryOperations.ConstructorIPartnerString =
                    (ProductPromotionCollectionByCountryOperations operations, IPartner partnerOperations, string country) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperations = productPromotionCollectionOperations.ByCountry(string.Empty);
                    nullOperations = productPromotionCollectionOperations.ByCountry(null);
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
        public void ProductPromotionCollectionOperationsTests_VerifyByCountryThrowsExceptionForEmptyCountries()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    Assert.IsNotNull(productPromotionCollectionOperations.ByCountry(invalidCountry));
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
        public void ProductPromotionCollectionOperationsTests_VerifyByCountryThrowsExceptionForInvalidCountries()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    Assert.IsNotNull(productPromotionCollectionOperations.ByCountry(invalidCountry));
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
