// -----------------------------------------------------------------------
// <copyright file="ProductUpgradeStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.ProductUpgrades
{    
    using System;
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.Models.ProductUpgrades;

    /// <summary>
    /// Checking the upgrade status of a product
    /// </summary>
    internal class ProductUpgradeStatus : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title
        /// </summary>
        public string Title
        {
            get
            {
                return "Product Upgrade Status";
            }
        }

        /// <summary>
        /// Testing the get upgrade status operation
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations</param>
        /// <param name="state">the application state</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedProductUpgradeCustomerKey] as string;
            var selectedProductFamily = state[FeatureSamplesApplication.ProductFamily] as string;
            var selectedUpgradeId = state[FeatureSamplesApplication.UpgradeId] as string;

            var productUpgradeRequest = new ProductUpgradesRequest
            {
                CustomerId = selectedCustomerId,
                ProductFamily = selectedProductFamily
            };

            ProductUpgradesStatus productUpgradeStatus = partnerOperations.ProductUpgrades.ById(selectedUpgradeId).CheckStatus(productUpgradeRequest);

            Console.Out.WriteLine("id: {0}", productUpgradeStatus.Id);
            Console.Out.WriteLine("productFamily: {0}", productUpgradeStatus.ProductFamily);
            Console.Out.WriteLine("status: {0}", productUpgradeStatus.Status);
            Console.Out.WriteLine();
        }
    }
}
