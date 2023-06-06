// -----------------------------------------------------------------------
// <copyright file="PromotionEligibilities.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.PromotionEligibilities
{
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.Models.PromotionEligibilities.Enums;

    /// <summary>
    ///  This class represents a model of a promotion eligibilities object.
    /// </summary>
    public class PromotionEligibilities : ResourceBase
    {
        /// <summary>
        /// Gets or sets the id of the promotions eligibilities item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the catalog item identifier.
        /// </summary>
        /// <value>
        /// The catalog item identifier.
        /// </value>
        public string CatalogItemId { get; set; }

        /// <summary>
        /// Gets or sets the product quantity.
        /// </summary>
        /// <value>
        /// The product quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the type of billing cycle for the selected offers.
        /// </summary>
        /// <value>
        /// The type of billing cycle set for the offers.
        /// </value>
        public BillingCycleType BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets the term duration if applicable.
        /// </summary>
        /// <value>
        /// The term duration if applicable.
        /// </value>
        public string TermDuration { get; set; }

        /// <summary>
        /// Gets or sets the promotion eligibility collection.
        /// </summary>
        /// <value>
        /// The eligibile promotions if applicable.
        /// </value>
        public List<PromotionEligibility> Eligibilities { get; set; }
    }
}
