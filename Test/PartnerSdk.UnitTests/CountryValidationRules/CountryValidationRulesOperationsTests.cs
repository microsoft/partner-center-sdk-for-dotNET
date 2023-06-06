// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.CountryValidationRules
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using ErrorHandling;
    using Models.CountryValidationRules;
    using Moq;
    using Network;
    using Network.Fakes;
    using Newtonsoft.Json;
    using PartnerCenter.CountryValidationRules;
    using QualityTools.Testing.Fakes;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for CountryValidationRulesOperations
    /// </summary>
    [TestClass]
    public class CountryValidationRulesOperationsTests
    {
        /// <summary>
        /// Ensures that check domain availability pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns> A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CountryValidationRulesOperationsTests_VerifyGetCountryValidationRules()
        {
            var countryValidationRules = new CountryValidationRules
            {
                Iso2Code = "US",
                DefaultCulture = "en-US",
                IsStateRequired = true,
                SupportedStatesList = new List<string> { "Washington" },
                SupportedLanguagesList = new List<string> { "English" },
                SupportedCulturesList = new List<string> { "en-US" },
                IsPostalCodeRequired = true,
                PostalCodeRegex = "postalCodeRegex",
                IsCityRequired = true,
                IsVatIdSupported = false,
                TaxIdFormat = "taxIdFormat",
                TaxIdSample = "taxIdSample",
                VatIdRegex = null,
                PhoneNumberRegex = "phoneNumberRegex",
                IsTaxIdSupported = true,
                CountryCallingCodesList = new List<string> { "91" }
            };

            int proxyGetAsyncCalls = 0;

            var countryValidationRulesOperations = new CountryValidationRulesOperations(Mock.Of<IPartner>(), countryValidationRules.Iso2Code);

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<CountryValidationRules, CountryValidationRules>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<CountryValidationRules, CountryValidationRules> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    var expectedResourcePath = string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCountryValidationRulesByCountry.Path, countryValidationRules.Iso2Code);
                    Assert.AreEqual(expectedResourcePath, resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<CountryValidationRules, CountryValidationRules>.AllInstances.GetAsync = (PartnerServiceProxy<CountryValidationRules, CountryValidationRules> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<CountryValidationRules>(countryValidationRules);
                };
                
                // call both sync and async versions of the Get country validation rules API
                var result = await countryValidationRulesOperations.GetAsync();
                AssertAreEqual(countryValidationRules, result);
                result = countryValidationRulesOperations.Get();
                AssertAreEqual(countryValidationRules, result);

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(2, proxyGetAsyncCalls);
            }
        }

        /// <summary>
        /// Ensures two country validation rules instances match.
        /// </summary>
        /// <param name="expected">The expected country validation rules.</param>
        /// <param name="actual">The actual country validation rules.</param>
        private static void AssertAreEqual(CountryValidationRules expected, CountryValidationRules actual)
        {
            Assert.AreEqual(expected.Iso2Code, actual.Iso2Code);
            Assert.AreEqual(expected.DefaultCulture, actual.DefaultCulture);
            Assert.AreEqual(expected.IsStateRequired, actual.IsStateRequired);
            Assert.AreEqual(expected.IsPostalCodeRequired, actual.IsPostalCodeRequired);
            Assert.AreEqual(expected.PostalCodeRegex, actual.PostalCodeRegex);
            Assert.AreEqual(expected.IsCityRequired, actual.IsCityRequired);
            Assert.AreEqual(expected.IsVatIdSupported, actual.IsVatIdSupported);
            Assert.AreEqual(expected.TaxIdFormat, actual.TaxIdFormat);
            Assert.AreEqual(expected.TaxIdSample, actual.TaxIdSample);
            Assert.AreEqual(expected.VatIdRegex, actual.VatIdRegex);
            Assert.AreEqual(expected.PhoneNumberRegex, actual.PhoneNumberRegex);
            Assert.AreEqual(expected.IsTaxIdSupported, actual.IsTaxIdSupported);
            AssertIfListEqual(expected.SupportedStatesList, actual.SupportedStatesList);
            AssertIfListEqual(expected.SupportedLanguagesList, actual.SupportedLanguagesList);
            AssertIfListEqual(expected.SupportedCulturesList, actual.SupportedCulturesList);
            AssertIfListEqual(expected.CountryCallingCodesList, actual.CountryCallingCodesList);            
        }

        /// <summary>
        /// Checks if two lists are equal
        /// </summary>
        /// <param name="expected">Expected list of string.</param>
        /// <param name="actual">Actual list of string.</param>
        private static void AssertIfListEqual(IEnumerable<string> expected, IEnumerable<string> actual)
        {
            if (expected == null || actual == null)
            {
                Assert.AreEqual(expected, actual);
            }

            Assert.AreEqual(expected.Count(), actual.Count());
            foreach (var element in expected)
            {
                Assert.IsTrue(actual.Contains(element));
            }
        }
    }
}