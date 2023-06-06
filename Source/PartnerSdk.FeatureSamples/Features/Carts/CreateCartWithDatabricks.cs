// -----------------------------------------------------------------------
// <copyright file="CreateCartWithDatabricks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Cart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Store.PartnerCenter.Models.Carts.Enums;
    using Models.Carts;
    using Models.Products;

    /// <summary>
    /// Creating a sample cart line item that having IncludedQuantityOptions property and saving it to test the create cart. 
    /// </summary>
    internal class CreateCartWithDatabricks : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title
        /// </summary>
        public string Title
        {
            get
            {
                return "Create Cart with Databricks";
            }
        }

        /// <summary>
        /// Testing the create cart operation
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations</param>
        /// <param name="state">cartID string</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerForOrderSvc] as string;

            var databrickProductId = "DZH318Z0BWR1";
            var databricksSkuId = partnerOperations.Products.ByCountry("US").ById(databrickProductId).Skus.Get().Items.FirstOrDefault().Id;
            var databricksAvailability = partnerOperations.Products.ByCountry("US").ById(databrickProductId).Skus.ById(databricksSkuId).Availabilities.Get().Items.FirstOrDefault();

            var subsciptionValidId = state[FeatureSamplesApplication.LegacyAzureSubscriptionKey] as string;
            var cart = new Cart()
            {
                LineItems = new List<CartLineItem>()
                {
                    new CartLineItem()
                    {
                        Id = 0,
                        CatalogItemId = databricksAvailability.CatalogItemId,
                        Quantity = databricksAvailability.Sku.MinimumQuantity,
                        BillingCycle = databricksAvailability.Sku.SupportedBillingCycles.FirstOrDefault(),
                        CurrencyCode = "USD",
                        ProvisioningContext = new Dictionary<string, string>
                        {
                            { "subscriptionId", subsciptionValidId },
                            { "scope", "shared" }
                        },
                        TermDuration = databricksAvailability.Terms?.FirstOrDefault()?.Duration
                    }
                }
            };

            cart = partnerOperations.Customers.ById(selectedCustomerId).Carts.Create(cart);

            Console.Out.WriteLine("Id: {0} ", cart.Id);
            Console.Out.WriteLine("Total Line Items: {0}", cart.LineItems.Count());
            Console.Out.WriteLine();
            state.Add(FeatureSamplesApplication.CartsKey, cart);
        }
    }
}
