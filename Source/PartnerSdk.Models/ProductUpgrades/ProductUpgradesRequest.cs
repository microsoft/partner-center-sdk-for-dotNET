﻿// -----------------------------------------------------------------------
// <copyright file="ProductUpgradesRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ProductUpgrades
{
    /// <summary>
    /// Represents a product upgrade request body.
    /// </summary>
    public class ProductUpgradesRequest
    {
        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the product family.
        /// </summary>
        public string ProductFamily { get; set; }
    }
}
