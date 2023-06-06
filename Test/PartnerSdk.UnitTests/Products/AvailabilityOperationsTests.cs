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
    /// Tests the <see cref="AvailabilityOperations"/> class.
    /// </summary>
    [TestClass]
    public class AvailabilityOperationsTests
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
        /// The availabilityId used to build the operations object.
        /// </summary>
        private const string ExpectedAvailabilityId = "3";

        /// <summary>
        /// The availability operations instance under test.
        /// </summary>
        private static AvailabilityOperations availabilityOperations;

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

            availabilityOperations = new AvailabilityOperations(partnerOperations.Object, ExpectedProductId, ExpectedSkuId, ExpectedAvailabilityId, ExpectedCountry);
        }

        /// <summary>
        /// Get Availability success path tests.
        /// </summary>
        [TestMethod]
        public void AvailabilityOperationsTests_GetAvailabilityTestVerifySuccessPath()
        {
            var expectedAvailability = new Availability()
            {
                Id = "4"
            };

            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<Availability, Availability>.AllInstances.GetAsync
                    = (PartnerServiceProxy<Availability, Availability> jsonProxy) =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format("products/{0}/skus/{1}/availabilities/{2}", ExpectedProductId, ExpectedSkuId, ExpectedAvailabilityId), jsonProxy.ResourcePath);
                        Assert.AreEqual(1, jsonProxy.UriParameters.Count);
                        Assert.AreEqual("country", jsonProxy.UriParameters.First().Key);
                        Assert.AreEqual(ExpectedCountry, jsonProxy.UriParameters.First().Value);

                        return Task.FromResult<Availability>(expectedAvailability);
                    };

                var availability = availabilityOperations.Get();

                Assert.IsNotNull(availability);
                Assert.AreEqual(expectedAvailability.Id, availability.Id);
            }
        }

        /// <summary>
        /// Ensures that country validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void AvailabilityOperationsTests_VerifyConstructorValidatesEmptyCountryParameter()
        {
            var invalidCountries = new List<string>() { string.Empty, null };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    availabilityOperations = new AvailabilityOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedSkuId, ExpectedAvailabilityId, invalidCountry);
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
        public void AvailabilityOperationsTests_VerifyConstructorValidatesCountryParameter()
        {
            var invalidCountries = new List<string>() { "ABC", "A" };

            foreach (var invalidCountry in invalidCountries)
            {
                try
                {
                    availabilityOperations = new AvailabilityOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedSkuId, ExpectedAvailabilityId, invalidCountry);
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
        public void AvailabilityOperationsTests_VerifyConstructorValidatesProductIdParameter()
        {
            var invalidProductIds = new List<string>() { string.Empty, null };

            foreach (var invalidProductId in invalidProductIds)
            {
                try
                {
                    availabilityOperations = new AvailabilityOperations(Mock.Of<IPartner>(), invalidProductId, ExpectedSkuId, ExpectedAvailabilityId, ExpectedCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid productId.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: productId has to be set.", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that skuId validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void AvailabilityOperationsTests_VerifyConstructorValidatesSkuIdParameter()
        {
            var invalidSkuIds = new List<string>() { string.Empty, null };

            foreach (var invalidSkuId in invalidSkuIds)
            {
                try
                {
                    availabilityOperations = new AvailabilityOperations(Mock.Of<IPartner>(), ExpectedProductId, invalidSkuId, ExpectedAvailabilityId, ExpectedCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid skuId.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: skuId has to be set.", ex.Message);
                }
            }
        }

        /// <summary>
        /// Ensures that availabilityId validation is done at the constructor.
        /// </summary>
        [TestMethod]
        public void AvailabilityOperationsTests_VerifyConstructorValidatesAvailabilityIdParameter()
        {
            var invalidAvailabilityIds = new List<string>() { string.Empty, null };

            foreach (var invalidAvailabilityId in invalidAvailabilityIds)
            {
                try
                {
                    availabilityOperations = new AvailabilityOperations(Mock.Of<IPartner>(), ExpectedProductId, ExpectedSkuId, invalidAvailabilityId, ExpectedCountry);
                    Assert.Fail("Constructor didn't throw exception for invalid availabilityId.");
                }
                catch (ArgumentException ex)
                {
                    Assert.AreEqual("Value cannot be null.\r\nParameter name: availabilityId has to be set.", ex.Message);
                }
            }
        }
    }
}
