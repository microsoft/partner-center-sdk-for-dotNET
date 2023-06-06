//----------------------------------------------------------------
// <copyright file="InventoryCheckRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents an inventory check request.
    /// </summary>
    public class InventoryCheckRequest
    {
        /// <summary>
        /// Any context values that apply towards inventory check of the provided items.
        /// </summary>
        private Dictionary<string, string> inventoryContext;

        /// <summary>
        /// Gets or sets the target items for the inventory check.
        /// </summary>
        public IEnumerable<InventoryItem> TargetItems { get; set; }

        /// <summary>
        /// Gets or sets any context values that apply towards inventory check of the provided items.
        /// </summary>
        public Dictionary<string, string> InventoryContext
        {
            get
            {
                if (this.inventoryContext == null)
                {
                    this.inventoryContext = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }

                return this.inventoryContext;
            }

            set
            {
                this.inventoryContext = new Dictionary<string, string>(value, StringComparer.OrdinalIgnoreCase);
            }
        }
    }
}
