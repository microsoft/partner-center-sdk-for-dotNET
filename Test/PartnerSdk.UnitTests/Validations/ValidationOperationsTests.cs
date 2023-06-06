// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.Domains
{
    using System.Threading.Tasks;
    using ErrorHandling;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Customers;
    using Microsoft.Store.PartnerCenter.Validations;
    using Moq;
    using Network;
    using Network.Fakes;
    using Newtonsoft.Json;
    using QualityTools.Testing.Fakes;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for ValidationOperations
    /// </summary>
    [TestClass]
    public class ValidationOperationsTests
    {
        /// <summary>
        /// Ensures that address validation pass in the right values to the proxy and actually calls the proxy for a valid address.
        /// </summary>
        /// <returns> A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task ValidationOperationsTests_IsAddressValid_ValidAddress()
        {
            var verifiedShippableAddress = new Address()
            {
                AddressLine1 = "1 Microsoft Way",
                City = "Redmond",
                State = "WA",
                Country = "US",
                PostalCode = "98052"
            };

            int proxyPostAsyncCalls = 0;
            var validationOperations = new ValidationOperations(Mock.Of<IPartner>());

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<Address, AddressValidationResponse>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<Address, AddressValidationResponse> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.AddressValidation.Path, resourcePath);
                };

                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Address, AddressValidationResponse>.AllInstances.PostAsyncT0 = (PartnerServiceProxy<Address, AddressValidationResponse> jsonProxy, Address address) =>
                {
                    // increment the number of the calls
                    proxyPostAsyncCalls++;
                    return Task.FromResult<AddressValidationResponse>(new AddressValidationResponse { Status = "VerifiedShippable" });
                };

                // call both sync and async versions of the address validation checks API
                var result = await validationOperations.IsAddressValidAsync(verifiedShippableAddress);
                Assert.AreEqual("VerifiedShippable", result.Status);

                result = validationOperations.IsAddressValid(verifiedShippableAddress);
                Assert.AreEqual("VerifiedShippable", result.Status);

                // ensure the proxy PostAsync() was called twice
                Assert.AreEqual(2, proxyPostAsyncCalls);
            }
        }

        /// <summary>
        /// Ensures that address validation pass in the right values to the proxy and actually calls the proxy for an invalid address.
        /// </summary>
        /// <returns> A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task ValidationOperationsTests_IsAddressValid_InvalidAddress()
        {
            var invalidAddress = new Address()
            {
                AddressLine1 = "1 Microsoft Way",
                City = "Seattle",
                State = "WA",
                Country = "US",
                PostalCode = "98055"
            };

            int proxyPostAsyncCalls = 0;
            var validationOperations = new ValidationOperations(Mock.Of<IPartner>());

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<Address, AddressValidationResponse>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<Address, AddressValidationResponse> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.AddressValidation.Path, resourcePath);
                };

                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Address, AddressValidationResponse>.AllInstances.PostAsyncT0 = (PartnerServiceProxy<Address, AddressValidationResponse> jsonProxy, Address address) =>
                {
                    // increment the number of the calls
                    proxyPostAsyncCalls++;
                    return Task.FromResult<AddressValidationResponse>(new AddressValidationResponse { Status = "None" });
                };

                // call async versions of the address validation checks API
                var result = await validationOperations.IsAddressValidAsync(invalidAddress);
                Assert.AreEqual("None", result.Status);

                // call sync versions of the address validation checks API
                result = validationOperations.IsAddressValid(invalidAddress);
                Assert.AreEqual("None", result.Status);

                // ensure the proxy PostAsync() was called twice
                Assert.AreEqual(2, proxyPostAsyncCalls);
            }
        }
    }
}