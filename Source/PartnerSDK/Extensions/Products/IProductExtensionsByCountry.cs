// -----------------------------------------------------------------------
// <copyright file="IProductExtensionsByCountry.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Extensions.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Products;

    /// <summary>
    /// Holds extension operations for products by country.
    /// </summary>
    public interface IProductExtensionsByCountry : IPartnerComponent
    {
        /// <summary>
        /// Retrieves inventory validation results for the provided country.
        /// </summary>
        /// <param name="checkRequest">The request for the inventory check.</param>
        /// <returns>The inventory check results.</returns>
        IEnumerable<InventoryItem> CheckInventory(InventoryCheckRequest checkRequest);

        /// <summary>
        /// Asynchronously retrieves inventory validation results for the provided country.
        /// </summary>
        /// <param name="checkRequest">The request for the inventory check.</param>
        /// <returns>The inventory check results.</returns>
        Task<IEnumerable<InventoryItem>> CheckInventoryAsync(InventoryCheckRequest checkRequest);
    }
}
