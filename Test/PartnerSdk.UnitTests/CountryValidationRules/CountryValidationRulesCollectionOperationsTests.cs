// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.CountryValidationRules
{
    using Moq;
    using PartnerCenter.CountryValidationRules;
    using PartnerCenter.CountryValidationRules.Fakes;
    using QualityTools.Testing.Fakes;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="CountryValidationRulesCollectionOperations"/> class.
    /// </summary>
    [TestClass]
    public class CountryValidationRulesCollectionOperationsTests
    {
        /// <summary>
        /// The expected country.
        /// </summary>
        private const string ExpectedCountry = "US";
        
        /// <summary>
        /// Ensures that getting a country validation rules by country works as expected.
        /// </summary>
        [TestMethod]
        public void CountryValidationRulesCollectionOperationsTests_VerifyByCountryNavigation()
        {
            var countryValidationRulesCollectionOperations = new CountryValidationRulesCollectionOperations(Mock.Of<IPartner>());

            using (ShimsContext.Create())
            {
                // route all CountryValidationRulesOperations constructors to our handler below
                ShimCountryValidationRulesOperations.ConstructorIPartnerString =
                    (CountryValidationRulesOperations operations, IPartner partnerOperations, string country) =>
                    {
                        Assert.AreEqual(ExpectedCountry, country);
                    };

                // invoke by country
                Assert.IsNotNull(countryValidationRulesCollectionOperations.ByCountry(ExpectedCountry));
            }
        }
    }
}