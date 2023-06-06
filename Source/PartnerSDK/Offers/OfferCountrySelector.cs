// -----------------------------------------------------------------------
// <copyright file="OfferCountrySelector.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Offers
{
    using System;
    using GenericOperations;
    using Utilities;

    /// <summary>
    /// An implementation that scopes offers and categories into a specific country.
    /// </summary>
    internal class OfferCountrySelector : BasePartnerComponent, ICountrySelector<IOfferCollection>, ICountrySelector<IOfferCategoryCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferCountrySelector"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public OfferCountrySelector(IPartner rootPartnerOperations) : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Scopes offers behavior to a specific country.
        /// </summary>
        /// <param name="country">The country for which to return offer operations.</param>
        /// <returns>The offer collection operations customized for the given country.</returns>
        IOfferCollection ICountrySelector<IOfferCollection>.ByCountry(string country)
        {
            ParameterValidator.ValidateCountryCode(country);

            return new OfferCollectionOperations(this.Partner, country);
        }

        /// <summary>
        /// Scopes offer categories behavior to a specific country.
        /// </summary>
        /// <param name="country">The country for which to return offer category operations.</param>
        /// <returns>The offer category collection operations customized for the given country.</returns>
        IOfferCategoryCollection ICountrySelector<IOfferCategoryCollection>.ByCountry(string country)
        {
            ParameterValidator.ValidateCountryCode(country);

            return new OfferCategoryCollectionOperations(this.Partner, country);
        }
    }
}
