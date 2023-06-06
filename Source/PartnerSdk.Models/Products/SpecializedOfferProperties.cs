// <copyright file="SpecializedOfferProperties.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents a specialized offer properties
    /// </summary>
    public class SpecializedOfferProperties
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// Gets or sets the pricing policies.
        /// </summary>
        public IEnumerable<SpecializedOfferPricingPolicy> PricingPolicies { get; set; }
    }
}
