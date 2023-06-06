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
    using Products.Fakes;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="SkuCollectionOperations"/> class.
    /// </summary>
    [TestClass]
    public class SkuCollectionOperationsTests
    {
        /// <summary>
        /// The productId used to build the operations object.
        /// </summary>
        private const string ExpectedProductId = "1";

        /// <summary>
        /// The country used to build the operations object.
        /// </summary>
        private const string ExpectedCountry = "US";

        /// <summary>
        /// The sku collection operations instance under test.
        /// </summary>
        private static SkuCollectionOperations skuOperations;

        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

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

            skuOperations = new SkuCollectionOperations(partnerOperations.Object, ExpectedProductId, ExpectedCountry);
        }

        /// <summary>
        /// Get Skus success path tests.
        /// </summary>
        [TestMethod]
        public void SkuCollectionOperationsTests_GetSkusTestVerifySuccessPath()
        {
            var skuList = new List<Sku>()
            {
                new Sku()
                {
                    Id = "1",
                    Title = "Title1"
                },
                new Sku()
                {
                    Id = "2",
                    Title = "Title2"
                }
            };
            var expectedSkus = new ResourceCollection<Sku>(skuList);

            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<Sku, ResourceCollection<Sku>>.AllInstances.GetAsync
                    = (PartnerServiceProxy<Sku, ResourceCollection<Sku>> jsonProxy) =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format("products/{0}/skus", ExpectedProductId), jsonProxy.ResourcePath);
                        Assert.AreEqual(1, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("country", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.First().Value);

                        return Task.FromResult<ResourceCollection<Sku>>(expectedSkus);
                    };

                var skus = skuOperations.Get();

                Assert.IsNotNull(skus);
                Assert.AreEqual(expectedSkus.TotalCount, skus.TotalCount);
                for (int i = 0; i < expectedSkus.TotalCount; i++)
                {
                    Assert.AreEqual(expectedSkus.Items.ElementAt(i).Id, skus.Items.ElementAt(i).Id);
                    Assert.AreEqual(expectedSkus.Items.ElementAt(i).Title, skus.Items.ElementAt(i).Title);
                }
            }
        }

        /// <summary>
        /// Ensures that country validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void SkuCollectionOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    skuOperations = new SkuCollectionOperations(Mock.Of<IPartner>(), ExpectedProductId, invalidCountry);
                    Assert.Fail();
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
        public void SkuCollectionOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    skuOperations = new SkuCollectionOperations(Mock.Of<IPartner>(), ExpectedProductId, invalidCountry);
                    Assert.Fail();
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
        public void SkuCollectionOperationsTests_VerifyConstructorValidatesProductIdParameter()
        {
            var invalidProductIds = new List<string>() { string.Empty, null };

            foreach (var invalidProductId in invalidProductIds)
            {
                try
                {
                    skuOperations = new SkuCollectionOperations(Mock.Of<IPartner>(), invalidProductId, ExpectedCountry);
                    Assert.Fail();
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: productId must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the sku operations get the correct skuId value.
        /// </summary>
        [TestMethod]
        public void SkuCollectionOperationsTests_VerifyByIdPassesCorrectValue()
        {
            var expectedSkuId = "1";

            using (ShimsContext.Create())
            {
                ShimSkuOperations.ConstructorIPartnerStringStringString =
                    (SkuOperations operations, IPartner partnerOperations, string productId, string skuId, string country) =>
                    {
                        Assert.AreEqual(ExpectedProductId, productId);
                        Assert.AreEqual(expectedSkuId, skuId);
                        Assert.AreEqual(ExpectedCountry, country);
                    };

                Assert.IsNotNull(skuOperations.ById(expectedSkuId));
                Assert.IsNotNull(skuOperations[expectedSkuId]);
            }
        }

        /// <summary>
        /// Ensures that this class doesn't duplicate the validation that is already done by the operations constructor.
        /// </summary>
        [TestMethod]
        public void SkuCollectionOperationsTests_VerifyByIdForwardsInvalidSkuIds()
        {
            ISku emptyOperationsFromMethod = null;
            ISku emptyOperationsFromIndex = null;
            ISku nullOperationsFromMethod = null;
            ISku nullOperationsFromIndex = null;

            using (ShimsContext.Create())
            {
                ShimSkuOperations.ConstructorIPartnerStringStringString =
                    (SkuOperations operations, IPartner partnerOperations, string productId, string skuId, string country) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperationsFromMethod = skuOperations.ById(string.Empty);
                    nullOperationsFromMethod = skuOperations.ById(null);
                    emptyOperationsFromIndex = skuOperations[string.Empty];
                    nullOperationsFromIndex = skuOperations[null];
                }
                catch (Exception ex)
                {
                    Assert.Fail(string.Format("An exception shouldn't be explicitly thrown by this method. Message: {0}", ex.Message));
                }

                // Ensure that it doesn't fail by returning a null object.
                Assert.IsNotNull(emptyOperationsFromMethod);
                Assert.IsNotNull(nullOperationsFromMethod);
                Assert.IsNotNull(emptyOperationsFromIndex);
                Assert.IsNotNull(nullOperationsFromIndex);
            }
        }

        /// <summary>
        /// Ensures that validation is done by an operations class at another level.
        /// </summary>
        [TestMethod]
        public void SkuCollectionOperationsTests_VerifyByIdThrowsExceptionForInvalidSkuIds()
        {
            var invalidIds = new List<string>() { string.Empty, null };

            foreach (var invalidId in invalidIds)
            {
                try
                {
                    Assert.IsNotNull(skuOperations.ById(invalidId));
                    Assert.Fail("ById didn't throw exception for invalid skuId.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: skuId must be set", ex.Message);
                }

                try
                {
                    Assert.IsNotNull(skuOperations[invalidId]);
                    Assert.Fail("ById didn't throw exception for invalid skuId.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: skuId must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the by target segment operations get the correct target segment value.
        /// </summary>
        [TestMethod]
        public void SkuCollectionOperationsTests_VerifyByTargetSegmentPassesCorrectValue()
        {
            var expectedTargetSegment = "3";

            using (ShimsContext.Create())
            {
                ShimSkuCollectionByTargetSegmentOperations.ConstructorIPartnerStringStringString =
                    (SkuCollectionByTargetSegmentOperations operations, IPartner partnerOperations, string productId, string country, string targetSegment) =>
                    {
                        Assert.AreEqual(ExpectedProductId, productId);
                        Assert.AreEqual(ExpectedCountry, country);
                        Assert.AreEqual(expectedTargetSegment, targetSegment);
                    };

                Assert.IsNotNull(skuOperations.ByTargetSegment(expectedTargetSegment));
            }
        }

        /// <summary>
        /// Ensures that this class doesn't duplicate the validation that is already done by the operations constructor.
        /// </summary>
        [TestMethod]
        public void SkuCollectionOperationsTests_VerifyByTargetSegmentForwardsInvalidTargetSegments()
        {
            ISkuCollectionByTargetSegment emptyOperations = null;
            ISkuCollectionByTargetSegment nullOperations = null;

            using (ShimsContext.Create())
            {
                ShimSkuCollectionByTargetSegmentOperations.ConstructorIPartnerStringStringString =
                    (SkuCollectionByTargetSegmentOperations operations, IPartner partnerOperations, string productId, string country, string targetSegment) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperations = skuOperations.ByTargetSegment(string.Empty);
                    nullOperations = skuOperations.ByTargetSegment(null);
                }
                catch (Exception ex)
                {
                    Assert.Fail(string.Format("An exception shouldn't be explicitly thrown by this method. Message: {0}", ex.Message));
                }
            }

            // Ensure that it doesn't fail by returning a null object.
            Assert.IsNotNull(emptyOperations);
            Assert.IsNotNull(nullOperations);
        }

        /// <summary>
        /// Ensures that validation is done by an operations class at another level.
        /// </summary>
        [TestMethod]
        public void SkuCollectionOperationsTests_VerifyByTargetSegmentThrowsExceptionForInvalidTargetSegments()
        {
            var invalidTargetSegments = new List<string>() { string.Empty, null };

            foreach (var invalidTargetSegment in invalidTargetSegments)
            {
                try
                {
                    Assert.IsNotNull(skuOperations.ByTargetSegment(invalidTargetSegment));
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
