// -----------------------------------------------------------------------
// <copyright file="IProductUpgradesCollection.cs" company="Microsoft Corporation">
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
    public interface IProductUpgradesCollection : IPartnerComponent
    {
        /// <summary>
        /// Gets the product upgrade by id operations for a specific customer.
        /// </summary>
        /// <param name="upgradeId">The upgrade id.</param>
        /// <returns>The customer operations.</returns>
        IProductUpgrades ById(string upgradeId);

        /// <summary>
        /// Checks the product upgrade eligibility
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade request body.</param>
        /// <returns>The eligibility object for the customer.</returns>
        ProductUpgradesEligibility CheckEligibility(ProductUpgradesRequest productUpgradesRequest);

        /// <summary>
        /// Asynchronously checks the product upgrade eligibility
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade request body.</param>
        /// <returns>The eligibility object for the customer.</returns>        
        Task<ProductUpgradesEligibility> CheckEligibilityAsync(ProductUpgradesRequest productUpgradesRequest);

        /// <summary>
        /// Upgrade product
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade request body.</param>
        /// <returns>The eligibility object for the customer.</returns>
        string Create(ProductUpgradesRequest productUpgradesRequest);

        /// <summary>
        /// Asynchronously upgrade product
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade request body.</param>
        /// <returns>The eligibility object for the customer.</returns>        
        Task<string> CreateAsync(ProductUpgradesRequest productUpgradesRequest);
    }
}
