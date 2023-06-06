// -----------------------------------------------------------------------
// <copyright file="IAzureRateCard.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.RateCards
{
    using System.Threading.Tasks;
    using Models.RateCards;

    /// <summary>
    /// Holds operations that apply to Azure rate card.
    /// </summary>
    public interface IAzureRateCard : IPartnerComponent
    {
        /// <summary>
        /// Gets the Azure rate card which provides real-time prices for Azure offers.
        /// </summary>
        /// <param name="currency">An optional three letter ISO code for the currency in which the resource rates will be provided.
        /// The default is the currency associated with the market in the partner's profile.</param>
        /// <param name="region">An optional two-letter ISO country/region code that indicates the market where the offer is purchased.
        /// The default is the country/region code set in the partner profile.</param>
        /// <returns>The Azure rate card for the partner.</returns>
        AzureRateCard Get(string currency = default(string), string region = default(string));

        /// <summary>
        /// Asynchronously gets the Azure rate card which provides real-time prices for Azure offers.
        /// </summary>
        /// <param name="currency">An optional three letter ISO code for the currency in which the resource rates will be provided.
        /// The default is the currency associated with the market in the partner's profile.</param>
        /// <param name="region">An optional two-letter ISO country/region code that indicates the market where the offer is purchased.
        /// The default is the country/region code set in the partner profile.</param>
        /// <returns>The Azure rate card for the partner.</returns>
        Task<AzureRateCard> GetAsync(string currency = default(string), string region = default(string));

        /// <summary>
        /// Asynchronously gets the Azure CSL rate card which provides real-time prices for Azure offers.
        /// </summary>
        /// <param name="currency">An optional three letter ISO code for the currency in which the resource rates will be provided.
        /// The default is the currency associated with the market in the partner's profile.</param>
        /// <param name="region">An optional two-letter ISO country/region code that indicates the market where the offer is purchased.
        /// The default is the country/region code set in the partner profile.</param>
        /// <returns>The Azure rate card for the partner.</returns>
        AzureRateCard GetShared(string currency = default(string), string region = default(string));

        /// <summary>
        /// Asynchronously gets the Azure CSL rate card which provides real-time prices for Azure offers.
        /// </summary>
        /// <param name="currency">An optional three letter ISO code for the currency in which the resource rates will be provided.
        /// The default is the currency associated with the market in the partner's profile.</param>
        /// <param name="region">An optional two-letter ISO country/region code that indicates the market where the offer is purchased.
        /// The default is the country/region code set in the partner profile.</param>
        /// <returns>The Azure rate card for the partner.</returns>
        Task<AzureRateCard> GetSharedAsync(string currency = default(string), string region = default(string));
    }
}
