// -----------------------------------------------------------------------
// <copyright file="CreateCart.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Cart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Carts;
    using Models.Products;

    /// <summary>
    /// Creating a sample cart line item and saving it to test the create cart item s 
    /// </summary>
    internal class CreateCart : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title
        /// </summary>
        public string Title
        {
            get
            {
                return "Create Cart";
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
            var subsciptionValidId = state[FeatureSamplesApplication.LegacyAzureSubscriptionKey] as string;

            var productId1 = "MS-AZR-0145P"; // legacy Azure subscription
            var productId2 = "DG7GMGF0DVT9"; // Software
            var productId3 = "DZH318Z0C1F2"; // SeatBasedSaaS
            var productId4 = "DZH318Z0BZ17"; // SQLdw

            var softwareAvailability = this.GetAvailability(partnerOperations, productId2);
            var seatBasedSaaSAvailability = this.GetAvailability(partnerOperations, productId3);
            var sqlDWAvailability = this.GetAvailability(partnerOperations, productId4);

            var cart = new Cart()
            {
                LineItems = new List<CartLineItem>()
                {
                    new CartLineItem()
                    {
                        Id = 0,
                        CatalogItemId = productId1,
                        Quantity = 1,
                        BillingCycle = BillingCycleType.Monthly,
                        CurrencyCode = "USD"
                    },
                    new CartLineItem()
                    {
                        Id = 1,
                        CatalogItemId = softwareAvailability.CatalogItemId,
                        Quantity = softwareAvailability.Sku.MinimumQuantity,
                        BillingCycle = (BillingCycleType)softwareAvailability.Sku?.SupportedBillingCycles?.FirstOrDefault(),
                    },
                    new CartLineItem()
                    {
                        Id = 2,
                        CatalogItemId = seatBasedSaaSAvailability.CatalogItemId,
                        Quantity = seatBasedSaaSAvailability.Sku.MinimumQuantity,
                        BillingCycle = (BillingCycleType)seatBasedSaaSAvailability.Sku?.SupportedBillingCycles?.FirstOrDefault(),
                        CurrencyCode = "USD",
                        TermDuration = seatBasedSaaSAvailability.Terms?.FirstOrDefault()?.Duration
                    },
                    new CartLineItem()
                    {
                        Id = 3,
                        CatalogItemId = sqlDWAvailability.CatalogItemId,
                        Quantity = sqlDWAvailability.Sku.MinimumQuantity,
                        BillingCycle = (BillingCycleType)sqlDWAvailability.Sku?.SupportedBillingCycles?.FirstOrDefault(),
                        CurrencyCode = "USD",
                        ProvisioningContext = new Dictionary<string, string>
                        {
                            { "subscriptionId", subsciptionValidId },
                            { "scope", "shared" }
                        },
                        TermDuration = sqlDWAvailability.Terms?.FirstOrDefault()?.Duration
                    }
                }
            };
            cart = partnerOperations.Customers.ById(selectedCustomerId).Carts.Create(cart);

            Console.Out.WriteLine("Id: {0} ", cart.Id);
            Console.Out.WriteLine("Total Line Items: {0}", cart.LineItems.Count());
            Console.Out.WriteLine();
            state.Add(FeatureSamplesApplication.CartsKey, cart);
        }

        /// <summary>
        /// Retrieve an availability from the given productId.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="productId">A productId.</param>
        /// <returns>Availability item.</returns>
        private Availability GetAvailability(IAggregatePartner partnerOperations, string productId)
        {
            var skuId = partnerOperations.Products.ByCountry("US").ById(productId).Skus.ByTargetSegment("commercial").Get().Items?.FirstOrDefault()?.Id;
            var availabilities = partnerOperations.Products.ByCountry("US").ById(productId).Skus.ById(skuId).Availabilities.ByTargetSegment("commercial").Get();
            return availabilities?.Items?.FirstOrDefault();
        }
    }
}
