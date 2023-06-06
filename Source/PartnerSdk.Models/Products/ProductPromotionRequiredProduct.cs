// -----------------------------------------------------------------------
// <copyright file="ProductPromotionRequiredProduct.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents a product promotion required product
    /// </summary>
    public sealed class ProductPromotionRequiredProduct
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the sku id.
        /// </summary>
        public string SkuId { get; set; }

        /// <summary>
        /// Gets or sets the term.
        /// </summary>
        public ProductPromotionTerm Term { get; set; }

        /// <summary>
        /// Gets or sets the pricing policies.
        /// </summary>
        public IEnumerable<ProductPromotionPricingPolicy> PricingPolicies { get; set; }
    }
}
