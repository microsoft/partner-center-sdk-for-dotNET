// -----------------------------------------------------------------------
// <copyright file="PromotionEligibilitiesCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.PromotionEligibilities
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.PromotionEligibilities;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// Promotion eligibilities collection operations implementation class.
    /// </summary>
    internal class PromotionEligibilitiesCollectionOperations : BasePartnerComponent, IPromotionEligibilitiesCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionEligibilitiesCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id to whom the subscriptions belong.</param>
        public PromotionEligibilitiesCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set");
            }
        }

        /// <summary>
        /// Posts a promotion eligibility request.
        /// </summary>
        /// <param name="promotionEligibilitiesRequest">A promotion eligibilities request.</param>
        /// <returns>The resulting promotion eligibilities.</returns>
        public ResourceCollection<Models.PromotionEligibilities.PromotionEligibilities> Post(PromotionEligibilitiesRequest promotionEligibilitiesRequest)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Models.PromotionEligibilities.PromotionEligibilities>>(() => this.PostAsync(promotionEligibilitiesRequest));
        }

        /// <summary>
        /// Asynchronously posts a promotion eligibility request.
        /// </summary>
        /// <param name="promotionEligibilitiesRequest">A promotion eligibilities request.</param>
        /// <returns>The resulting promotion eligibilities.</returns>
        public async Task<ResourceCollection<Models.PromotionEligibilities.PromotionEligibilities>> PostAsync(PromotionEligibilitiesRequest promotionEligibilitiesRequest)
        {
            ParameterValidator.Required(promotionEligibilitiesRequest, "promotionEligibilitiesRequest can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<PromotionEligibilitiesRequest, ResourceCollection<Models.PromotionEligibilities.PromotionEligibilities>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.PostPromotionEligibilities.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(promotionEligibilitiesRequest);
        }
    }
}
