// -----------------------------------------------------------------------
// <copyright file="AzureRateCardOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.RateCards
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.RateCards;
    using Network;

    /// <summary>
    /// Implements operations that apply to Azure rate card.
    /// </summary>
    internal class AzureRateCardOperations : BasePartnerComponent, IAzureRateCard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureRateCardOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public AzureRateCardOperations(IPartner rootPartnerOperations) : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Gets the Azure rate card which provides real-time prices for Azure offers.
        /// </summary>
        /// <param name="currency">An optional three letter ISO code for the currency in which the resource rates will be provided.
        /// The default is the currency associated with the market in the partner's profile.</param>
        /// <param name="region">An optional two-letter ISO country/region code that indicates the market where the offer is purchased.
        /// The default is the country/region code set in the partner profile.</param>
        /// <returns>The Azure rate card for the partner.</returns>
        public AzureRateCard Get(string currency = default(string), string region = default(string))
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync(currency, region));
        }

        /// <summary>
        /// Asynchronously gets the Azure rate card which provides real-time prices for Azure offers.
        /// </summary>
        /// <param name="currency">An optional three letter ISO code for the currency in which the resource rates will be provided.
        /// The default is the currency associated with the market in the partner's profile.</param>
        /// <param name="region">An optional two-letter ISO country/region code that indicates the market where the offer is purchased.
        /// The default is the country/region code set in the partner profile.</param>        
        /// <returns>The Azure rate card for the partner.</returns>
        public async Task<AzureRateCard> GetAsync(string currency = default(string), string region = default(string))
        {
            return await this.GetPartnerServiceProxy(currency, region, false).GetAsync();
        }

        /// <summary>
        /// Asynchronously gets the Azure CSL rate card which provides real-time prices for Azure offers.
        /// </summary>
        /// <param name="currency">An optional three letter ISO code for the currency in which the resource rates will be provided.
        /// The default is the currency associated with the market in the partner's profile.</param>
        /// <param name="region">An optional two-letter ISO country/region code that indicates the market where the offer is purchased.
        /// The default is the country/region code set in the partner profile.</param>        
        /// <returns>The Azure rate card for the partner.</returns>
        public AzureRateCard GetShared(string currency = default(string), string region = default(string))
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetSharedAsync(currency, region));
        }

        /// <summary>
        /// Asynchronously gets the Azure CSL rate card which provides real-time prices for Azure offers.
        /// </summary>
        /// <param name="currency">An optional three letter ISO code for the currency in which the resource rates will be provided.
        /// The default is the currency associated with the market in the partner's profile.</param>
        /// <param name="region">An optional two-letter ISO country/region code that indicates the market where the offer is purchased.
        /// The default is the country/region code set in the partner profile.</param>        
        /// <returns>The Azure rate card for the partner.</returns>
        public async Task<AzureRateCard> GetSharedAsync(string currency = default(string), string region = default(string))
        {
            return await this.GetPartnerServiceProxy(currency, region, true).GetAsync();
        }

        /// <summary>
        /// Creates PartnerServiceProxy used to fetch Azure CSP/CSL rate card data
        /// </summary>
        /// <param name="currency">An optional three letter ISO code for the currency in which the resource rates will be provided.
        /// The default is the currency associated with the market in the partner's profile.</param>
        /// <param name="region">An optional two-letter ISO country/region code that indicates the market where the offer is purchased.
        /// The default is the country/region code set in the partner profile.</param>
        /// <param name="isAzureShared">Is true for Azure CSL offer, false for other offer types.</param>
        /// <returns>The Azure rate card for the partner.</returns>
        private PartnerServiceProxy<AzureRateCard, AzureRateCard> GetPartnerServiceProxy(string currency, string region, bool isAzureShared)
        {
            var partnerServiceProxy = new PartnerServiceProxy<AzureRateCard, AzureRateCard>(
               this.Partner,
               isAzureShared ? PartnerService.Instance.Configuration.Apis.GetAzureSharedRateCard.Path : PartnerService.Instance.Configuration.Apis.GetAzureRateCard.Path);

            if (!string.IsNullOrWhiteSpace(currency))
            {
                partnerServiceProxy.UriParameters.Add(
                    new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetAzureRateCard.Parameters.Currency,
                        currency));
            }

            if (!string.IsNullOrWhiteSpace(region))
            {
                partnerServiceProxy.UriParameters.Add(
                    new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetAzureRateCard.Parameters.Region,
                        region));
            }

            return partnerServiceProxy;
        }
    }
}
