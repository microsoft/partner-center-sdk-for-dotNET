// -----------------------------------------------------------------------
// <copyright file="CreateCartModernOffice.cs" company="Microsoft Corporation">
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
    /// Creating a sample Modern Office cart line item and saves it to test the create cart item.
    /// </summary>
    internal class CreateCartModernOffice : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Create a Modern Office Cart";
            }
        }

        /// <summary>
        /// Testing the create cart operation.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">cartID string.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerForOrderSvc] as string;

            var productId = "CFQ7TTC0LH16"; // Modern Office Product.

            var modernOfficeAvailability = this.GetAvailability(partnerOperations, productId);

            var cart = new Cart()
            {
                LineItems = new List<CartLineItem>()
                {
                    new CartLineItem()
                    {
                        Id = 0,
                        CatalogItemId = modernOfficeAvailability.CatalogItemId,
                        Quantity = modernOfficeAvailability.Sku?.MinimumQuantity ?? 20,
                        BillingCycle = (BillingCycleType)modernOfficeAvailability.Sku?.SupportedBillingCycles?.FirstOrDefault(),
                        TermDuration = modernOfficeAvailability.Terms?.FirstOrDefault()?.Duration,
                        CurrencyCode = "USD"
                    },
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
