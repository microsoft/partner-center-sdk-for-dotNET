//----------------------------------------------------------------
// <copyright file="InventoryItem.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that represents an inventory item.
    /// </summary>
    public class InventoryItem
    {
        /// <summary>
        /// Gets or sets the item product id.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the item sku id.
        /// </summary>
        public string SkuId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this item currently has any inventory restrictions.
        /// </summary>
        public bool IsRestricted { get; set; }

        /// <summary>
        /// Gets or sets the restrictions for this item if any.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<InventoryRestriction> Restrictions { get; set; }
    }
}
