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
    /// Tests the <see cref="AvailabilityCollectionByTargetSegmentOperations"/> class.
    /// </summary>
    [TestClass]
    public class AvailabilityCollectionByTargetSegmentOperationsTests
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
        /// The target segment used to build the operations object.
        /// </summary>
        private const string ExpectedTargetSegment = "commercial";

        /// <summary>
        /// The availability operations instance under test.
        /// </summary>
        private static AvailabilityCollectionByTargetSegmentOperations availabilityOperations;

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

            availabilityOperations = new AvailabilityCollectionByTargetSegmentOperations(partnerOperations.Object, ExpectedProductId, ExpectedSkuId, ExpectedCountry, ExpectedTargetSegment);
        }

        /// <summary>
        /// Get Availability success path tests.
        /// </summary>
        [TestMethod]
        public void AvailabilityCollectionByTargetSegmentOperationsTests_GetAvailabilityTestVerifySuccessPath()
        {
            var availabilityList = new List<Availability>()
            {
                new Availability()
                {
                    Id = "1"
                },
                new Availability()
                {
                    Id = "2"
                }
            };
            var expectedAvailabilities = new ResourceCollection<Availability>(availabilityList);

            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<Availability, ResourceCollection<Availability>>.AllInstances.GetAsync
                    = (PartnerServiceProxy<Availability, ResourceCollection<Availability>> jsonProxy) =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format("products/{0}/skus/{1}/availabilities", ExpectedProductId, ExpectedSkuId), jsonProxy.ResourcePath);
                        Assert.AreEqual(2, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("country", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.First().Value);
                        Assert.AreEqual("targetSegment", jsonProxy.UriParameters.ElementAt(1).Key);
                        Assert.AreEqual(ExpectedTargetSegment, jsonProxy.UriParameters.ElementAt(1).Value);

                        return Task.FromResult<ResourceCollection<Availability>>(expectedAvailabilities);
                    };

                var availabilities = availabilityOperations.Get();

                Assert.IsNotNull(availabilities);
                Assert.AreEqual(expectedAvailabilities.TotalCount, availabilities.TotalCount);
                for (int i = 0; i < expectedAvailabilities.TotalCount; i++)
                {
                    Assert.AreEqual(expectedAvailabilities.Items.ElementAt(i).Id, availabilities.Items.ElementAt(i).Id);
                }
            }
        }

        /// <summary>
        /// Ensures that country validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void AvailabilityCollectionByTargetSegmentOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    availabilityOperations = new AvailabilityCollectionByTargetSegmentOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedSkuId, invalidCountry, ExpectedTargetSegment);
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
        public void AvailabilityCollectionByTargetSegmentOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    availabilityOperations = new AvailabilityCollectionByTargetSegmentOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedSkuId, invalidCountry, ExpectedTargetSegment);
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
        public void AvailabilityCollectionByTargetSegmentOperationsTests_VerifyConstructorValidatesProductIdParameter()
        {
            var invalidProductIds = new List<string>() { string.Empty, null };

            foreach (var invalidProductId in invalidProductIds)
            {
                try
                {
                    availabilityOperations = new AvailabilityCollectionByTargetSegmentOperations(Mock.Of<IPartner>(), invalidProductId, ExpectedSkuId, ExpectedCountry, ExpectedTargetSegment);
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
        public void AvailabilityCollectionByTargetSegmentOperationsTests_VerifyConstructorValidatesSkuIdParameter()
        {
            var invalidSkuIds = new List<string>() { string.Empty, null };

            foreach (var invalidSkuId in invalidSkuIds)
            {
                try
                {
                    availabilityOperations = new AvailabilityCollectionByTargetSegmentOperations(Mock.Of<IPartner>(), ExpectedProductId, invalidSkuId, ExpectedCountry, ExpectedTargetSegment);
                    Assert.Fail("Constructor didn't throw exception for invalid skuId.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: skuId must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that target segment validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void AvailabilityCollectionByTargetSegmentOperationsTests_VerifyConstructorValidatesTargetSegmentParameter()
        {
            var invalidTargetSegments = new List<string>() { string.Empty, null };

            foreach (var invalidTargetSegment in invalidTargetSegments)
            {
                try
                {
                    availabilityOperations = new AvailabilityCollectionByTargetSegmentOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedSkuId, ExpectedCountry, invalidTargetSegment);
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
