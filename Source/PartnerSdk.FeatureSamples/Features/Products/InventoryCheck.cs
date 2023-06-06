// -----------------------------------------------------------------------
// <copyright file="InventoryCheck.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Products;

    /// <summary>
    /// Showcases checking inventory.
    /// </summary>
    internal class InventoryCheck : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Check Inventory"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // get the products available to this partner by software
            var products = partnerOperations.Products.ByCountry("US").ByTargetView("azure").Get();
            var customerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;
            var subscriptionId = state[FeatureSamplesApplication.SelectedSubscriptionKey] as string;
            var checkRequest = new InventoryCheckRequest()
            {
                TargetItems = products.Items.Select(product => new InventoryItem { ProductId = product.Id }),

                InventoryContext = new Dictionary<string, string>()
                {
                    { "customerId", customerId },
                    { "azureSubscriptionId", subscriptionId }
                }
            };

            var inventoryItems = partnerOperations.Extensions.Product.ByCountry("us").CheckInventory(checkRequest);

            // display the inventory items
            Console.Out.WriteLine("inventory items count: " + inventoryItems.Count());

            foreach (var inventoryItem in inventoryItems)
            {
                Console.Out.WriteLine("Is restricted: {0}", inventoryItem.IsRestricted);
                if (!inventoryItem.IsRestricted)
                {
                    state[FeatureSamplesApplication.InventoryKey] = inventoryItem;
                }

                foreach (var restriction in inventoryItem.Restrictions)
                {
                    Console.Out.WriteLine("Restriction: {0}", restriction.Description);
                }
            }
        }
    }
}
