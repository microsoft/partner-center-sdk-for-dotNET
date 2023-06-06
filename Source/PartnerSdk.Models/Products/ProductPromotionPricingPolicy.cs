// <copyright file="ProductPromotionPricingPolicy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    /// <summary>
    /// Class that represents a product promotion pricing policy
    /// </summary>
    public sealed class ProductPromotionPricingPolicy
    {
        /// <summary>
        /// Gets or sets the policy type.
        /// </summary>
        public string PolicyType { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
    }
}
