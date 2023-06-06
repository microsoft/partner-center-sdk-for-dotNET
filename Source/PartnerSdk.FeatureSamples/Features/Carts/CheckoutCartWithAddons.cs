// -----------------------------------------------------------------------
// <copyright file="CheckoutCartWithAddons.cs" company="Microsoft Corporation">
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

    /// <summary>
    /// Checking a given cart including add on items to see if the order is being placed 
    /// </summary>
    internal class CheckoutCartWithAddons : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title
        /// </summary>
        public string Title
        {
            get
            {
                return "Checkout Cart with add on items";
            }
        }

        /// <summary>
        /// Testing the update cart  add on items operation
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerForOrderSvc] as string;
            var selectedCart = state[FeatureSamplesApplication.CartsKey] as Cart;

            Console.Out.WriteLine("Id of Cart: {0} ", selectedCart.Id);
            Console.Out.WriteLine(
                "Catalog Id of Addon: {0}",
                selectedCart.LineItems.FirstOrDefault().AddonItems == null ? selectedCart.LineItems.FirstOrDefault().CatalogItemId : selectedCart.LineItems.FirstOrDefault().AddonItems.ToArray()[0].CatalogItemId);
            Console.Out.WriteLine("Quantity: {0}", selectedCart.LineItems.ToArray()[0].Quantity);
            Console.Out.WriteLine("Currency: {0}", selectedCart.LineItems.ToArray()[0].CurrencyCode);
            Console.Out.WriteLine("CatalogItemId: {0}", selectedCart.LineItems.ToArray()[0].CatalogItemId);
            Console.Out.WriteLine("BillingCycle: {0}", selectedCart.LineItems.ToArray()[0].BillingCycle);
            Console.Out.WriteLine("FriendlyName: {0}", selectedCart.LineItems.ToArray()[0].FriendlyName);
            Console.Out.WriteLine("Checkout...");

            var ans = partnerOperations.Customers.ById(selectedCustomerId).Carts.ById(selectedCart.Id).Checkout();
            state[FeatureSamplesApplication.OrdersKey] = ans.Orders;
            Console.Out.WriteLine("Id: {0}", ans.Orders.First().Id);
            Console.Out.WriteLine("Creation Date: {0}", ans.Orders.First().CreationDate);
            Console.Out.WriteLine("Status of Order: {0}", ans.Orders.First().Status);
            Console.Out.WriteLine("Count of LineItems: {0}", ans.Orders.First().LineItems.Count());
            Console.Out.WriteLine();
        }
    }
}
