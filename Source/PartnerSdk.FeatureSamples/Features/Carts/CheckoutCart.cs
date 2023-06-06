// -----------------------------------------------------------------------
// <copyright file="CheckoutCart.cs" company="Microsoft Corporation">
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
    /// Checking a given cart to see if the order is being placed 
    /// </summary>
    internal class CheckoutCart : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title
        /// </summary>
        public string Title
        {
            get
            {
                return "Checkout Cart";
            }
        }

        /// <summary>
        /// Testing the update cart operation
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations</param>
        /// <param name="state">cartID string</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerForOrderSvc] as string;
            Cart cart = state[FeatureSamplesApplication.CartsKey] as Cart;
            Console.Out.WriteLine("Card Id: {0} ", cart.Id);
            Console.Out.WriteLine("CatalogItem IDs:");
            foreach (var item in cart.LineItems)
            {
                Console.Out.WriteLine(item.CatalogItemId);
            }

            Console.Out.WriteLine();

            var ans = partnerOperations.Customers.ById(selectedCustomerId).Carts.ById(cart.Id).Checkout();
            state[FeatureSamplesApplication.OrdersKey] = ans.Orders;
            Console.Out.WriteLine("Order IDs:");
            foreach (var order in ans.Orders)
            {
                Console.Out.WriteLine(order.Id);
            }

            if (ans.OrderErrors.Any())
            {
                Console.Out.WriteLine("Order Errors:");
                foreach (var error in ans.OrderErrors)
                {
                    Console.Out.WriteLine(error.Description);
                }
            }

            Console.Out.WriteLine();
        }
    }
}
