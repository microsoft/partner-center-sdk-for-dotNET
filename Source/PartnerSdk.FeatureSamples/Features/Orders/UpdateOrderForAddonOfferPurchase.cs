// -----------------------------------------------------------------------
// <copyright file="UpdateOrderForAddonOfferPurchase.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Customers;
    using Models.Orders;

    /// <summary>
    /// Showcases updating an order.
    /// </summary>
    internal class UpdateOrderForAddonOfferPurchase : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Update order for Addon offer purchase"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and the order from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;
            string baseOfferId = "16879030-563B-4C89-9586-2CB79ED270EE"; // "Intune (Government Pricing)"
            string baseOfferName = "Intune ";

            // "id": "1641FE13-5A95-4BA7-A5EB-740CAEE8D509"
            // "name": "Intune Additional Storage (Government Pricing)"
            string addonOfferId = "1641FE13-5A95-4BA7-A5EB-740CAEE8D509";
            string addonOfferName = "Intune Additional Storage";

            this.PurchaseAddOn(
                partnerOperations,
                selectedCustomerId,
                baseOfferId,
                baseOfferName,
                addonOfferId,
                addonOfferName);
        }

        /// <summary>
        /// Purchases the add on.
        /// </summary>
        /// <param name="partnerOperations">The partner operations.</param>
        /// <param name="selectedCustomerId">The selected customer identifier.</param>
        /// <param name="baseOfferId">The base offer identifier.</param>
        /// <param name="baseOfferName">Name of the base offer.</param>
        /// <param name="addOnOfferId">The add on offer identifier.</param>
        /// <param name="addOnOfferName">Name of the add on offer.</param>
        public void PurchaseAddOn(
            IPartner partnerOperations,
            string selectedCustomerId,
            string baseOfferId,
            string baseOfferName,
            string addOnOfferId,
            string addOnOfferName)
        {
            // buy the base offer
            var order = new Order()
            {
                ReferenceCustomerId = selectedCustomerId,
                LineItems = new List<OrderLineItem>()
                {
                    new OrderLineItem()
                    {
                        OfferId = baseOfferId,
                        FriendlyName = baseOfferName + "My Base offer",
                        Quantity = 1
                    }
                }
            };

            order = partnerOperations.Customers.ById(selectedCustomerId).Orders.Create(order);

            Console.Out.WriteLine("Id: {0}", order.Id);
            Console.Out.WriteLine("Creation Date: {0}", order.CreationDate);

            var resultingSubscriptionId = order.LineItems.ToArray()[0].SubscriptionId;
            Console.Out.WriteLine("Resulting subscritption id: {0}", resultingSubscriptionId);

            // lets try buying the add-on offer
            // Patching the order to buy the add-on
            var orderTobeUpdated = new Order()
            {
                ReferenceCustomerId = selectedCustomerId,
                LineItems = new List<OrderLineItem>()
                {
                    new OrderLineItem()
                    {
                        OfferId = addOnOfferId,
                        FriendlyName = addOnOfferName + "My Add on offer",
                        Quantity = 1,
                        LineItemNumber = 0,
                        ParentSubscriptionId = resultingSubscriptionId
                    }
                }
            };

            order = partnerOperations.Customers.ById(selectedCustomerId).Orders.ById(order.Id).Patch(orderTobeUpdated);

            Console.Out.WriteLine("Resukt order line items");

            Console.Out.WriteLine("Id: {0}", order.Id);
            Console.Out.WriteLine("Creation Date: {0}", order.CreationDate);

            foreach (var linteItem in order.LineItems)
            {
                Console.Out.WriteLine("Resulting offer id: {0}", linteItem.OfferId);
                Console.Out.WriteLine("Resulting subscritption id: {0}", linteItem.SubscriptionId);
            }
        }
    }
}
