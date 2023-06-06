// -----------------------------------------------------------------------
// <copyright file="ICountryValidationRulesCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CountryValidationRules
{
    using Microsoft.Store.PartnerCenter.GenericOperations;

    /// <summary>
    /// Encapsulates country validation rules behavior.
    /// </summary>
    public interface ICountryValidationRulesCollection : IPartnerComponent, ICountrySelector<ICountryValidationRules>
    {
        /// <summary>
        /// Obtains behavior for a specific country's validation rules.
        /// </summary>
        /// <param name="country">The country's ISO2 code.</param>
        /// <returns>The country validation rules operations.</returns>
        new ICountryValidationRules ByCountry(string country);
    }
}
