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
    /// Tests the <see cref="AvailabilityCollectionOperations"/> class.
    /// </summary>
    [TestClass]
    public class AvailabilityCollectionOperationsTests
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
        /// The availability collection operations instance under test.
        /// </summary>
        private static AvailabilityCollectionOperations availabilityOperations;

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

            availabilityOperations = new AvailabilityCollectionOperations(partnerOperations.Object, ExpectedProductId, ExpectedSkuId, ExpectedCountry);
        }

        /// <summary>
        /// Get Availabilities success path tests.
        /// </summary>
        [TestMethod]
        public void AvailabilityCollectionOperationsTests_GetAvailabilitiesTestVerifySuccessPath()
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
                        Assert.AreEqual(1, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("country", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.First().Value);

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
        public void AvailabilityCollectionOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    availabilityOperations = new AvailabilityCollectionOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedSkuId, invalidCountry);
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
        public void AvailabilityCollectionOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    availabilityOperations = new AvailabilityCollectionOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedSkuId, invalidCountry);
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
        public void AvailabilityCollectionOperationsTests_VerifyConstructorValidatesProductIdParameter()
        {
            var invalidProductIds = new List<string>() { string.Empty, null };

            foreach (var invalidProductId in invalidProductIds)
            {
                try
                {
                    availabilityOperations = new AvailabilityCollectionOperations(Mock.Of<IPartner>(), invalidProductId, ExpectedSkuId, ExpectedCountry);
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
        public void AvailabilityCollectionOperationsTests_VerifyConstructorValidatesSkuIdParameter()
        {
            var invalidSkuIds = new List<string>() { string.Empty, null };

            foreach (var invalidSkuId in invalidSkuIds)
            {
                try
                {
                    availabilityOperations = new AvailabilityCollectionOperations(Mock.Of<IPartner>(), ExpectedProductId, invalidSkuId, ExpectedCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid skuId.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: skuId must be set", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the availability operations get the correct availabilityId value.
        /// </summary>
        [TestMethod]
        public void AvailabilityCollectionOperationsTests_VerifyByIdPassesCorrectValue()
        {
            var expectedAvailabilityId = "5";

            using (ShimsContext.Create())
            {
                ShimAvailabilityOperations.ConstructorIPartnerStringStringStringString =
                    (AvailabilityOperations operations, IPartner partnerOperations, string productId, string skuId, string availabilityId, string country) =>
                    {
                        Assert.AreEqual(ExpectedProductId, productId);
                        Assert.AreEqual(ExpectedSkuId, skuId);
                        Assert.AreEqual(expectedAvailabilityId, availabilityId);
                        Assert.AreEqual(ExpectedCountry, country);
                    };

                Assert.IsNotNull(availabilityOperations.ById(expectedAvailabilityId));
                Assert.IsNotNull(availabilityOperations[expectedAvailabilityId]);
            }
        }

        /// <summary>
        /// Ensures that this class doesn't duplicate the validation that is already done by the operations constructor.
        /// </summary>
        [TestMethod]
        public void AvailabilityCollectionOperationsTests_VerifyByIdForwardsInvalidAvailabilityIds()
        {
            IAvailability emptyOperationsFromMethod = null;
            IAvailability emptyOperationsFromIndex = null;
            IAvailability nullOperationsFromMethod = null;
            IAvailability nullOperationsFromIndex = null;

            using (ShimsContext.Create())
            {
                ShimAvailabilityOperations.ConstructorIPartnerStringStringStringString =
                    (AvailabilityOperations operations, IPartner partnerOperations, string productId, string skuId, string availabilityId, string country) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperationsFromMethod = availabilityOperations.ById(string.Empty);
                    nullOperationsFromMethod = availabilityOperations.ById(null);
                    emptyOperationsFromIndex = availabilityOperations[string.Empty];
                    nullOperationsFromIndex = availabilityOperations[null];
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
        public void AvailabilityCollectionOperationsTests_VerifyByIdThrowsExceptionForInvalidAvailabilityIds()
        {
            var invalidIds = new List<string>() { string.Empty, null };

            foreach (var invalidId in invalidIds)
            {
                try
                {
                    Assert.IsNotNull(availabilityOperations.ById(invalidId));
                    Assert.Fail("ById didn't throw exception for invalid availabilityId.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: availabilityId has to be set.", ex.Message);
                }

                try
                {
                    Assert.IsNotNull(availabilityOperations[invalidId]);
                    Assert.Fail("ById didn't throw exception for invalid availabilityId.");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: availabilityId has to be set.", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that the by segment operations get the correct segment value.
        /// </summary>
        [TestMethod]
        public void AvailabilityCollectionOperationsTests_VerifyByTargetSegmentPassesCorrectValue()
        {
            var expectedSegmentId = "3";

            using (ShimsContext.Create())
            {
                ShimAvailabilityCollectionByTargetSegmentOperations.ConstructorIPartnerStringStringStringString =
                    (AvailabilityCollectionByTargetSegmentOperations operations, IPartner partnerOperations, string productId, string skuId, string country, string segment) =>
                    {
                        Assert.AreEqual(ExpectedProductId, productId);
                        Assert.AreEqual(ExpectedSkuId, skuId);
                        Assert.AreEqual(ExpectedCountry, country);
                        Assert.AreEqual(expectedSegmentId, segment);
                    };

                Assert.IsNotNull(availabilityOperations.ByTargetSegment(expectedSegmentId));
            }
        }

        /// <summary>
        /// Ensures that this class doesn't duplicate the validation that is already done by the operations constructor.
        /// </summary>
        [TestMethod]
        public void AvailabilityCollectionOperationsTests_VerifyByTargetSegmentForwardsInvalidSegments()
        {
            IAvailabilityCollectionByTargetSegment emptyOperations = null;
            IAvailabilityCollectionByTargetSegment nullOperations = null;

            using (ShimsContext.Create())
            {
                ShimAvailabilityCollectionByTargetSegmentOperations.ConstructorIPartnerStringStringStringString =
                    (AvailabilityCollectionByTargetSegmentOperations operations, IPartner partnerOperations, string productId, string skuId, string country, string segment) =>
                    {
                        // Ignore validation from other classes.
                    };

                try
                {
                    emptyOperations = availabilityOperations.ByTargetSegment(string.Empty);
                    nullOperations = availabilityOperations.ByTargetSegment(null);
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
        public void AvailabilityCollectionOperationsTests_VerifyByTargetSegmentThrowsExceptionForInvalidSegments()
        {
            var invalidSegments = new List<string>() { string.Empty, null };

            foreach (var invalidSegment in invalidSegments)
            {
                try
                {
                    Assert.IsNotNull(availabilityOperations.ByTargetSegment(invalidSegment));
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
