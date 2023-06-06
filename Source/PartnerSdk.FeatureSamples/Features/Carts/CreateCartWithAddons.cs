// -----------------------------------------------------------------------
// <copyright file="CreateCartWithAddons.cs" company="Microsoft Corporation">
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
    /// Creating a sample cart having add on items and saving it to test the create cart. 
    /// </summary>
    internal class CreateCartWithAddons : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title
        /// </summary>
        public string Title
        {
            get
            {
                return "Create Cart with Addons";
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
            var cart = new Cart()
            {
                LineItems = new List<CartLineItem>()
                {
                    new CartLineItem()
                    {
                        Id = 0,
                        CatalogItemId = state[FeatureSamplesApplication.OfferWithAddonId] as string,
                        FriendlyName = "Myofferpurchase",
                        Quantity = 3,
                        BillingCycle = BillingCycleType.Monthly,
                        AddonItems = new List<CartLineItem>
                        {
                            new CartLineItem
                            {
                                Id = 1,
                                CatalogItemId = state[FeatureSamplesApplication.OfferAddonOneId] as string,
                                BillingCycle = BillingCycleType.Monthly,
                                Quantity = 2,
                            },
                            new CartLineItem
                            {
                                Id = 2,
                                CatalogItemId = state[FeatureSamplesApplication.OfferAddonTwoId] as string,
                                BillingCycle = BillingCycleType.Monthly,
                                Quantity = 3
                            }
                        }
                    }
                }
            };

            cart = partnerOperations.Customers.ById(selectedCustomerId).Carts.Create(cart);
            Console.Out.WriteLine("Id: {0} ", cart.Id);
            Console.Out.WriteLine("Quantity: {0}", cart.LineItems.ToArray()[0].Quantity);
            Console.Out.WriteLine("OderGroup of Offer: {0}", cart.LineItems.FirstOrDefault().OrderGroup);
            Console.Out.WriteLine(
                "OderGroup of Offer Addon: {0}", 
                cart.LineItems.FirstOrDefault().AddonItems.FirstOrDefault().OrderGroup);
            Console.Out.WriteLine();
            state[FeatureSamplesApplication.CartsKey] = cart;
        }
    }
}