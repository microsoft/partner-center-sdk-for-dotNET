// -----------------------------------------------------------------------
// <copyright file="CreateOrder.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Orders
{
    using System;
    using System.Collections.Generic;
    using Models.Customers;
    using Models.Offers;
    using Models.Orders;

    /// <summary>
    /// Showcases creating an order.
    /// </summary>
    internal class CreateOrder : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Create Order"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;
            var offers = state[FeatureSamplesApplication.OffersKey] as List<Offer>;

            var randomIndex = (new Random()).Next(0, offers.Count);
            var order = new Order()
            {
                ReferenceCustomerId = selectedCustomerId,
                LineItems = new List<OrderLineItem>()
                {
                    new OrderLineItem()
                    {
                        OfferId = offers[randomIndex].Id,
                        FriendlyName = offers[randomIndex].Name + "My offer purchase",
                        Quantity = offers[randomIndex].MinimumQuantity + 1
                    }
                }
            };

            order = partnerOperations.Customers.ById(selectedCustomerId).Orders.Create(order);

            Console.Out.WriteLine("Id: {0}", order.Id);
            Console.Out.WriteLine("Creation Date: {0}", order.CreationDate);
            Console.Out.WriteLine();
        }
    }
}
