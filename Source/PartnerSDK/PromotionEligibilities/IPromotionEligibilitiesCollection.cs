// -----------------------------------------------------------------------
// <copyright file="IPromotionEligibilitiesCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.PromotionEligibilities
{
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.PromotionEligibilities;

    /// <summary>
    /// Encapsulates the promotion eligibilities behavior.
    /// </summary>
    public interface IPromotionEligibilitiesCollection : IPartnerComponent, IEntityPostOperations<PromotionEligibilitiesRequest, ResourceCollection<PromotionEligibilities>>
    {
        /// <summary>
        /// Posts a promotion eligibility request.
        /// </summary>
        /// <param name="promotionEligibilitiesRequest">A promotion eligibilities request.</param>
        /// <returns>The resulting promotion eligibilities.</returns>
        new ResourceCollection<PromotionEligibilities> Post(PromotionEligibilitiesRequest promotionEligibilitiesRequest);

        /// <summary>
        /// Asynchronously posts a promotion eligibility request.
        /// </summary>
        /// <param name="promotionEligibilitiesRequest">A promotion eligibilities request.</param>
        /// <returns>The resulting promotion eligibilities.</returns>
        new Task<ResourceCollection<PromotionEligibilities>> PostAsync(PromotionEligibilitiesRequest promotionEligibilitiesRequest);
    }
}
