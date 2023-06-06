// -----------------------------------------------------------------------
// <copyright file="ProductPromotion.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents a product promotion
    /// </summary>
    public sealed class ProductPromotion : ResourceBase
    {
        /// <summary>
        /// Gets or sets the promotion id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the promotion name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the promotion description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public ProductPromotionProperties Properties { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public IEnumerable<ProductPromotionRequiredProduct> RequiredProducts { get; set; }

        /// <summary>
        /// Gets or sets the promotion constraints.
        /// </summary>
        public PromotionConstraints PromotionConstraints { get; set; }
    }
}
