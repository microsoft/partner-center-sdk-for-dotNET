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
    /// Tests the <see cref="ProductPromotionsCollectionByCountryOperations"/> class.
    /// </summary>
    [TestClass]
    public class ProductPromotionCollectionByCountryOperationsTests
    {
        /// <summary>
        /// The country used to build the operations object.
        /// </summary>
        private const string ExpectedCountry = "US";

        /// <summary>
        /// The product promotion collection operations instance under test.
        /// </summary>
        private static ProductPromotionCollectionByCountryOperations productPromotionCollectionByCountryOperations;

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
            productPromotionCollectionByCountryOperations = new ProductPromotionCollectionByCountryOperations(Mock.Of<IPartner>(), ExpectedCountry);
        }

        /// <summary>
        /// Ensures that validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void ProductPromotionCollectionByCountryOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productPromotionCollectionByCountryOperations = new ProductPromotionCollectionByCountryOperations(Mock.Of<IPartner>(), invalidCountry);
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
        public void ProductPromotionCollectionByCountryOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    productPromotionCollectionByCountryOperations = new ProductPromotionCollectionByCountryOperations(Mock.Of<IPartner>(), invalidCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid country.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Country has to be a 2 letter string", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the by segment operations get the correct segment value.
        /// </summary>
        [TestMethod]
        public void ProductPromotionCollectionByCountryOperationsTests_VerifyBySegmentPassesCorrectValue()
        {
            var expectedSegment = "1";

            using (ShimsContext.Create())
            {
                ShimProductPromotionCollectionByCountryBySegmentOperations.ConstructorIPartnerStringString =
                    (ProductPromotionCollectionByCountryBySegmentOperations operations, IPartner partnerOperations, string segment, string country) =>
                    {
                        Assert.AreEqual(ExpectedCountry, country);
                        Assert.AreEqual(expectedSegment, segment);
                    };
                
                Assert.IsNotNull(productPromotionCollectionByCountryOperations.BySegment(expectedSegment));
            }
        }

        /// <summary>
        /// Ensures that this class doesn't duplicate the validation that is already done by the operations constructor.
        /// </summary>
        [TestMethod]
        public void ProductPromotionCollectionByCountryOperationsTests_VerifyBySegmentForwardsInvalidSegments()
        {
            IProductPromotionCollectionByCountryBySegment emptyOperations = null;
            IProductPromotionCollectionByCountryBySegment nullOperations = null;

            using (ShimsContext.Create())
            {
                ShimProductPromotionCollectionByCountryBySegmentOperations.ConstructorIPartnerStringString =
                    (ProductPromotionCollectionByCountryBySegmentOperations operations, IPartner partnerOperations, string segment, string country) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperations = productPromotionCollectionByCountryOperations.BySegment(string.Empty);
                    nullOperations = productPromotionCollectionByCountryOperations.BySegment(null);
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
        public void ProductPromotionCollectionByCountryOperationsTests_VerifyBySegmentThrowsExceptionForInvalidSegments()
        {
            var invalidSegments = new List<string>() { string.Empty, null };

            foreach (var invalidSegment in invalidSegments)
            {
                try
                {
                    Assert.IsNotNull(productPromotionCollectionByCountryOperations.BySegment(invalidSegment));
                    Assert.Fail("BySegment didn't throw exception for invalid segment.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: segment must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the product promotion operations get the correct productPromotionId value.
        /// </summary>
        [TestMethod]
        public void ProductPromotionCollectionByCountryOperationsTests_VerifyByIdPassesCorrectValue()
        {
            var expectedProductPromotionId = "1";

            using (ShimsContext.Create())
            {
                ShimProductPromotionOperations.ConstructorIPartnerStringString =
                    (ProductPromotionOperations operations, IPartner partnerOperations, string productPromotionId, string country) =>
                    {
                        Assert.AreEqual(ExpectedCountry, country);
                        Assert.AreEqual(expectedProductPromotionId, productPromotionId);
                    };

                Assert.IsNotNull(productPromotionCollectionByCountryOperations.ById(expectedProductPromotionId));
                Assert.IsNotNull(productPromotionCollectionByCountryOperations[expectedProductPromotionId]);
            }
        }

        /// <summary>
        /// Ensures that no validation is done on the productPromotionId parameter at this level.
        /// </summary>
        [TestMethod]
        public void ProductPromotionCollectionByCountryOperationsTests_VerifyByIdForwardsInvalidProductPromotionIds()
        {
            IProductPromotion emptyOperationsFromMethod = null;
            IProductPromotion emptyOperationsFromIndex = null;
            IProductPromotion nullOperationsFromMethod = null;
            IProductPromotion nullOperationsFromIndex = null;

            using (ShimsContext.Create())
            {
                ShimProductPromotionOperations.ConstructorIPartnerStringString =
                    (ProductPromotionOperations operations, IPartner partnerOperations, string productPromotionId, string country) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperationsFromMethod = productPromotionCollectionByCountryOperations.ById(string.Empty);
                    nullOperationsFromMethod = productPromotionCollectionByCountryOperations.ById(null);
                    emptyOperationsFromIndex = productPromotionCollectionByCountryOperations[string.Empty];
                    nullOperationsFromIndex = productPromotionCollectionByCountryOperations[null];
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
        public void ProductPromotionCollectionByCountryOperationsTests_VerifyByIdThrowsExceptionForInvalidProductPromotionIds()
        {
            var invalidIds = new List<string>() { string.Empty, null };

            foreach (var invalidId in invalidIds)
            {
                try
                {
                    Assert.IsNotNull(productPromotionCollectionByCountryOperations.ById(invalidId));
                    Assert.Fail("ById didn't throw exception for invalid productPromotionId.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: productPromotionId has to be set.", ex.Message);
                }

                try
                {
                    Assert.IsNotNull(productPromotionCollectionByCountryOperations[invalidId]);
                    Assert.Fail("ById didn't throw exception for invalid productPromotionId.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: productPromotionId has to be set.", ex.Message);
                }
            }
        }
    }
}
