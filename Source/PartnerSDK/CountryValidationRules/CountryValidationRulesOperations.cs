// -----------------------------------------------------------------------
// <copyright file="CountryValidationRulesOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CountryValidationRules
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.CountryValidationRules;
    using Network;
    using Utilities;

    /// <summary>
    /// The country validation rules operations implementation.
    /// </summary>
    internal class CountryValidationRulesOperations : BasePartnerComponent, ICountryValidationRules
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryValidationRulesOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="country">The country.</param>
        public CountryValidationRulesOperations(IPartner rootPartnerOperations, string country)
            : base(rootPartnerOperations, country)
        {
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <summary>
        /// Gets the market specific validation data by country.
        /// </summary>
        /// <returns>The market specific validation data operations.</returns>
        public CountryValidationRules Get()
        {
            return PartnerService.Instance.SynchronousExecute<CountryValidationRules>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the country validation rules by country.
        /// </summary>
        /// <returns>The country validation rules.</returns>
        public async Task<CountryValidationRules> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<CountryValidationRules, CountryValidationRules>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCountryValidationRulesByCountry.Path, this.Context));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}