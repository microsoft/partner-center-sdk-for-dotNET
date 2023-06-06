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
    /// Tests the <see cref="SkuCollectionByTargetSegmentOperations"/> class.
    /// </summary>
    [TestClass]
    public class SkuCollectionByTargetSegmentOperationsTests
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
        /// The segment used to build the operations object.
        /// </summary>
        private const string ExpectedTargetSegment = "software";

        /// <summary>
        /// The sku collection operations instance under test.
        /// </summary>
        private static SkuCollectionByTargetSegmentOperations skuOperations;

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

            skuOperations = new SkuCollectionByTargetSegmentOperations(partnerOperations.Object, ExpectedProductId, ExpectedCountry, ExpectedTargetSegment);
        }

        /// <summary>
        /// Get Skus success path tests.
        /// </summary>
        [TestMethod]
        public void SkuCollectionByTargetSegmentOperationsTests_GetSkusTestVerifySuccessPath()
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
                        Assert.AreEqual(2, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("country", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.First().Value);
                        Assert.AreEqual("targetSegment", jsonProxy.UriParameters.ElementAt(1).Key);
                        Assert.AreEqual(ExpectedTargetSegment, jsonProxy.UriParameters.ElementAt(1).Value);

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
        public void SkuCollectionByTargetSegmentOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    skuOperations = new SkuCollectionByTargetSegmentOperations(Mock.Of<IPartner>(), ExpectedProductId, invalidCountry, ExpectedTargetSegment);
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
        public void SkuCollectionByTargetSegmentOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    skuOperations = new SkuCollectionByTargetSegmentOperations(Mock.Of<IPartner>(), ExpectedProductId, invalidCountry, ExpectedTargetSegment);
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
        public void SkuCollectionByTargetSegmentOperationsTests_VerifyConstructorValidatesProductIdParameter()
        {
            var invalidProductIds = new List<string>() { string.Empty, null };

            foreach (var invalidProductId in invalidProductIds)
            {
                try
                {
                    skuOperations = new SkuCollectionByTargetSegmentOperations(Mock.Of<IPartner>(), invalidProductId, ExpectedCountry, ExpectedTargetSegment);
                    Assert.Fail();
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: productId must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that target segment validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void SkuCollectionByTargetSegmentOperationsTests_VerifyConstructorValidatesTargetSegmentParameter()
        {
            var invalidSegments = new List<string>() { string.Empty, null };

            foreach (var invalidSegment in invalidSegments)
            {
                try
                {
                    skuOperations = new SkuCollectionByTargetSegmentOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedCountry, invalidSegment);
                    Assert.Fail();
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: targetSegment must be set", ex.Message);
                }
            }
        }
    }
}
