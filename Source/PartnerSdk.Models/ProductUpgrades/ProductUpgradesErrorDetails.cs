// -----------------------------------------------------------------------
// <copyright file="ProductUpgradesErrorDetails.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ProductUpgrades
{
    /// <summary>
    /// Represents error details for a product upgrade.
    /// </summary>
    public class ProductUpgradesErrorDetails
    {
        /// <summary>
        /// Gets or sets the description of the product upgrade.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the error code of the product upgrade.
        /// </summary>
        public string Code { get; set; }
    }
}
