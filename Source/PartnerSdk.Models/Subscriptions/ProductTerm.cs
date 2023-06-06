// -----------------------------------------------------------------------
// <copyright file="ProductTerm.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using Microsoft.Store.PartnerCenter.Models.Offers;
    using Newtonsoft.Json;

    /// <summary>
    /// The product and term information for the next term.
    /// </summary>
    public class ProductTerm
    {
        /// <summary>
        /// Gets or sets the future product id.
        /// *Required.
        /// </summary>
        /// <value>The future product id.</value>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the future SKU id.
        /// *Required.
        /// </summary>
        /// <value>The future SKU id.</value>
        public string SkuId { get; set; }

        /// <summary>
        /// Gets or sets the future availability id.
        /// *Required.
        /// </summary>
        /// <value>The future availability id.</value>
        public string AvailabilityId { get; set; }

        /// <summary>
        /// Gets or sets the type of billing cycle for the selected offers.
        /// *Required.
        /// </summary>
        /// <value>
        /// The type of billing cycle set for the offers.</value>
        public BillingCycleType BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets the term duration if applicable.
        /// *Required.
        /// </summary>
        /// <value>
        /// The term duration if applicable.
        /// </value>
        public string TermDuration { get; set; }

        /// <summary>
        /// Gets or sets the promotion id.
        /// This property is used when the target subscription is part of a promotion.
        /// </summary>
        /// <value>The value of the promotion id.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PromotionId { get; set; }
    }
}