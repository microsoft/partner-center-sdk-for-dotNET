// -----------------------------------------------------------------------
// <copyright file="GetMarketSpecificValidationDataByCountry.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CountryValidationRules
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Showcases Get Market specific validation data.
    /// </summary>
    internal class GetMarketSpecificValidationDataByCountry : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Get Country Valdiation Rules By Country";
            }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var countryValidationRules = partnerOperations.CountryValidationRules.ByCountry("US").Get();

            Console.WriteLine("Country validation rules from the xml:");
            Console.WriteLine(countryValidationRules.Iso2Code);
            Console.WriteLine(countryValidationRules.PhoneNumberRegex);
            Console.WriteLine(countryValidationRules.PostalCodeRegex);
            Console.WriteLine(countryValidationRules.TaxIdFormat);
            Console.WriteLine(countryValidationRules.TaxIdSample);
        }
    }
}