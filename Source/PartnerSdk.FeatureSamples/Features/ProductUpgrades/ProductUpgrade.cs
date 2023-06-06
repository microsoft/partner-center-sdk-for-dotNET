// -----------------------------------------------------------------------
// <copyright file="ProductUpgrade.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.ProductUpgrades
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Microsoft.Store.PartnerCenter.Models.ProductUpgrades;

    /// <summary>
    /// Upgrade a product
    /// </summary>
    internal class ProductUpgrade : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title
        /// </summary>
        public string Title
        {
            get
            {
                return "Product Upgrade";
            }
        }

        /// <summary>
        /// Testing the product upgrade operation
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations</param>
        /// <param name="state">the application state</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedProductUpgradeCustomerKey] as string;
            var selectedProductFamily = state[FeatureSamplesApplication.ProductFamily] as string;

            var productUpgradeRequest = new ProductUpgradesRequest
            {
                CustomerId = selectedCustomerId,
                ProductFamily = selectedProductFamily
            };
            var productUpgradeLocationHeader = partnerOperations.ProductUpgrades.Create(productUpgradeRequest);
            if (!string.IsNullOrEmpty(productUpgradeLocationHeader))
            {
                string[] locationHeaderString = Regex.Split(productUpgradeLocationHeader, "/");
                state.Add(FeatureSamplesApplication.UpgradeId, locationHeaderString[1]);
                Console.Out.WriteLine("Upgrade Complete!");
            }
            else
            {
                Console.Out.WriteLine("Upgrade Failed!");
            }        
            
            Console.Out.WriteLine();            
        }
    }
}
