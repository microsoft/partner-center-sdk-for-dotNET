﻿// -----------------------------------------------------------------------
// <copyright file="Availability.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents an availability.
    /// </summary>
    public class Availability : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets the availability id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the sku id.
        /// </summary>
        public string SkuId { get; set; }

        /// <summary>
        /// Gets or sets the id that uniquely identifies this item in the catalog.
        /// </summary>
        public string CatalogItemId { get; set; }

        /// <summary>
        /// Gets or sets the default currency supported for this availability.
        /// </summary>
        public CurrencyInfo DefaultCurrency { get; set; }

        /// <summary>
        /// Gets or sets the segment.
        /// </summary>
        public string Segment { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the availability is purchasable or not.
        /// </summary>
        public bool IsPurchasable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the availability is renewable or not.
        /// </summary>
        public bool IsRenewable { get; set; }

        /// <summary>
        /// Gets or sets the list of renewal instructions.
        /// </summary>
        public IEnumerable<RenewalInstruction> RenewalInstructions { get; set; }

        /// <summary>
        /// Gets or sets the terms if applicable to this availability.
        /// </summary>
        public IEnumerable<AvailabilityTerm> Terms { get; set; }

        /// <summary>
        /// Gets or sets the options for included quantity.
        /// </summary>
        public IEnumerable<AvailabilityIncludedQuantity> IncludedQuantityOptions { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        public Sku Sku { get; set; }
    }
}
