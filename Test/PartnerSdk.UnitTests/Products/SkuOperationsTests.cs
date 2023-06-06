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
    /// Tests the <see cref="SkuOperations"/> class.
    /// </summary>
    [TestClass]
    public class SkuOperationsTests
    {
        /// <summary>
        /// The productId used to build the operations object.
        /// </summary>
        private const string ExpectedProductId = "1";

        /// <summary>
        /// The skuId used to build the operations object.
        /// </summary>
        private const string ExpectedSkuId = "2";

        /// <summary>
        /// The country used to build the operations object.
        /// </summary>
        private const string ExpectedCountry = "US";

        /// <summary>
        /// The sku collection operations instance under test.
        /// </summary>
        private static SkuOperations skuOperations;

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

            skuOperations = new SkuOperations(partnerOperations.Object, ExpectedProductId, ExpectedSkuId, ExpectedCountry);
        }

        /// <summary>
        /// Get Sku success path tests.
        /// </summary>
        [TestMethod]
        public void SkuOperationsTests_GetSkuTestVerifySuccessPath()
        {
            var expectedSku = new Sku()
            {
                Id = ExpectedSkuId,
                Title = "Title1"
            };            

            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<Sku, Sku>.AllInstances.GetAsync
                    = (PartnerServiceProxy<Sku, Sku> jsonProxy) =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format("products/{0}/skus/{1}", ExpectedProductId, ExpectedSkuId), jsonProxy.ResourcePath);
                        Assert.AreEqual(1, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("country", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.First().Value);

                        return Task.FromResult<Sku>(expectedSku);
                    };

                var sku = skuOperations.Get();

                Assert.IsNotNull(sku);
                Assert.AreEqual(expectedSku.Id, sku.Id);
                Assert.AreEqual(expectedSku.Title, sku.Title);
            }
        }

        /// <summary>
        /// Ensures that country validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void SkuOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    skuOperations = new SkuOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedSkuId, invalidCountry);
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
        public void SkuOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    skuOperations = new SkuOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedSkuId, invalidCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid country.");
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
        public void SkuOperationsTests_VerifyConstructorValidatesProductIdParameter()
        {
            var invalidProductIds = new List<string>() { string.Empty, null };

            foreach (var invalidProductId in invalidProductIds)
            {
                try
                {
                    skuOperations = new SkuOperations(Mock.Of<IPartner>(), invalidProductId, ExpectedSkuId, ExpectedCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid productId.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: productId must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that skuId validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void SkuOperationsTests_VerifyConstructorValidatesSkuIdParameter()
        {
            var invalidSkuIds = new List<string>() { string.Empty, null };

            foreach (var invalidSkuId in invalidSkuIds)
            {
                try
                {
                    skuOperations = new SkuOperations(Mock.Of<IPartner>(), ExpectedProductId, invalidSkuId, ExpectedCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid skuId.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: skuId must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the availabilities operations are not null.
        /// </summary>
        [TestMethod]
        public void ProductOperationsTests_VerifyAvailabilitiesOperationsNotNull()
        {
            Assert.IsNotNull(skuOperations.Availabilities);
        }
    }
}
