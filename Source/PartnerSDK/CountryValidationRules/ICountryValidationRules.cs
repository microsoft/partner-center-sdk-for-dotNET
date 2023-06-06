// -----------------------------------------------------------------------
// <copyright file="ICountryValidationRules.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CountryValidationRules
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.CountryValidationRules;

    /// <summary>
    /// Represents the behavior of a specific country validation rules.
    /// </summary>
    public interface ICountryValidationRules : IPartnerComponent, IEntityGetOperations<CountryValidationRules>
    {
        /// <summary>
        /// Retrieves the country validation rules.
        /// </summary>
        /// <returns>The validation rules for the country.</returns>
        new CountryValidationRules Get();

        /// <summary>
        /// Asynchronously retrieves the country validation rules.
        /// </summary>
        /// <returns>The validation rules for the country.</returns>
        new Task<CountryValidationRules> GetAsync();
    }
}