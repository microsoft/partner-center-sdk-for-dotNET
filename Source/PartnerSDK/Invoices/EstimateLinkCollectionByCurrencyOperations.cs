// -----------------------------------------------------------------------
// <copyright file="EstimateLinkCollectionByCurrencyOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Invoices;
    using Network;
    using Utilities;

    /// <summary>
    /// Represents the operations available on an estimate link collection by currency.
    /// </summary>
    internal class EstimateLinkCollectionByCurrencyOperations : BasePartnerComponent, IEstimateLinkCollectionByCurrency
    {
        /// <summary>
        /// The currency code.
        /// </summary>
        private readonly string currencyCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="EstimateLinkCollectionByCurrencyOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="currencyCode">The currency code.</param>
        public EstimateLinkCollectionByCurrencyOperations(IPartner rootPartnerOperations, string currencyCode)
            : base(rootPartnerOperations, currencyCode)
        {
            ParameterValidator.Required(currencyCode, "CurrencyCode has to be set.");
            this.currencyCode = currencyCode;
        }

        /// <summary>
        /// Retrieves the estimate link resource collection.
        /// </summary>
        /// <returns>The estimate link resource collection.</returns>
        public ResourceCollection<EstimateLink> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<EstimateLink>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the estimate link resource collection.
        /// </summary>
        /// <returns>The estimate link resource collection.</returns>
        public async Task<ResourceCollection<EstimateLink>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<EstimateLink, ResourceCollection<EstimateLink>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetEstimatesLinks.Path, this.currencyCode));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetEstimatesLinks.Parameters.CurrencyCode, this.currencyCode));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
