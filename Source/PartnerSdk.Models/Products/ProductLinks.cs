// -----------------------------------------------------------------------
// -----------------------------------------------------------------------
// <copyright file="ProductLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    /// <summary>
    /// Navigation links for Product.
    /// </summary>
    public class ProductLinks : StandardResourceLinks
    {
        /// <summary>
        /// Gets or sets the skus link.
        /// </summary>
        public Link Skus { get; set; }
    }
}
