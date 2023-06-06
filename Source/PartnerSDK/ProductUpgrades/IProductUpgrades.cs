// -----------------------------------------------------------------------
// <copyright file="IProductUpgrades.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ProductUpgrades
{
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.ProductUpgrades;

    /// <summary>
    /// Holds operations that apply to product upgrades.
    /// </summary>
    public interface IProductUpgrades : IPartnerComponent
    {
        /// <summary>
        /// Checks the product upgrade status
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade status request body.</param>
        /// <returns>The eligibility object for the customer.</returns>
        ProductUpgradesStatus CheckStatus(ProductUpgradesRequest productUpgradesRequest);

        /// <summary>
        /// Asynchronously checks the product upgrade status
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade status request body.</param>
        /// <returns>The eligibility object for the customer.</returns>        
        Task<ProductUpgradesStatus> CheckStatusAsync(ProductUpgradesRequest productUpgradesRequest);       
    }
}
