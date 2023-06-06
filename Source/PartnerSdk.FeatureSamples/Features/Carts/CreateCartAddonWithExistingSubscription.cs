// <copyright file="CreateCartAddonWithExistingSubscription.cs" company="Microsoft Corporation">
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
    /// Creating a sample cart having add on items for existing subscription. 
    /// </summary>
    internal class CreateCartAddonWithExistingSubscription : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title
        /// </summary>
        public string Title
        {
            get
            {
                return "Create Cart with Addons for existing subscription";
            }
        }

        /// <summary>
        /// Testing the create cart having add on item operation
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerForOrderSvc] as string;
            var baseOfferCart = new Cart()
            {
                LineItems = new List<CartLineItem>()
                {
                    new CartLineItem()
                    {
                        Id = 0,
                        CatalogItemId = state[FeatureSamplesApplication.OfferWithAddonId] as string,
                        FriendlyName = "Myofferpurchase",
                        Quantity = 3,
                        BillingCycle = BillingCycleType.Annual,
                    }
                }
            };

            baseOfferCart = partnerOperations.Customers.ById(selectedCustomerId).Carts.Create(baseOfferCart);
            var ans = partnerOperations.Customers.ById(selectedCustomerId).Carts.ById(baseOfferCart.Id).Checkout();
            var cart = new Cart()
            {
                LineItems = new List<CartLineItem>()
                {
                    new CartLineItem()
                    {
                        Id = 0,
                        CatalogItemId = state[FeatureSamplesApplication.OfferAddonOneId] as string,
                        ProvisioningContext = new Dictionary<string, string>
                        {
                            {
                                "ParentSubscriptionId",
                                ans.Orders.FirstOrDefault().LineItems.FirstOrDefault().SubscriptionId
                            }
                        },
                        Quantity = 1,
                        BillingCycle = BillingCycleType.Annual,
                    }
                }
            };

            cart = partnerOperations.Customers.ById(selectedCustomerId).Carts.Create(cart);
            Console.Out.WriteLine("Id: {0} ", cart.Id);
            Console.Out.WriteLine("Quantity: {0}", cart.LineItems.ToArray()[0].Quantity);
            Console.Out.WriteLine("Id of existing subscription: {0}", cart.LineItems.FirstOrDefault().ProvisioningContext["parentSubscriptionId"]);
            Console.Out.WriteLine("Id of addon item: {0}", cart.LineItems.FirstOrDefault().CatalogItemId);
            Console.Out.WriteLine();
            state[FeatureSamplesApplication.CartsKey] = cart;
        }
    }
}
