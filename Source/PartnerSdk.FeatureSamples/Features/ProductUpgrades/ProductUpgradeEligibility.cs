// -----------------------------------------------------------------------
// <copyright file="ProductUpgradeEligibility.cs" company="Microsoft Corporation">
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
    /// Checking the upgrade eligibility of a product
    /// </summary>
    internal class ProductUpgradeEligibility : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title
        /// </summary>
        public string Title
        {
            get
            {
                return "Product Upgrade Eligibility";
            }
        }

        /// <summary>
        /// Testing the get upgrade eligibility operation
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

            ProductUpgradesEligibility productUpgradeEligibility = partnerOperations.ProductUpgrades.CheckEligibility(productUpgradeRequest);

            Console.Out.WriteLine("Eligible: {0} ", productUpgradeEligibility.IsEligible);

            if (!string.IsNullOrEmpty(productUpgradeEligibility.UpgradeId))
            {
                Console.Out.WriteLine("UpgradeId: {0}", productUpgradeEligibility.UpgradeId);
                state.Add(FeatureSamplesApplication.UpgradeId, productUpgradeEligibility.UpgradeId);
            }

            Console.Out.WriteLine();
        }
    }
}
