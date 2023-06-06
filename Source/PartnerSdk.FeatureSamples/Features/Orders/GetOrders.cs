// -----------------------------------------------------------------------
// <copyright file="GetOrders.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Orders
{
    using System;
    using System.Collections.Generic;
    using Models.Orders;
    using Newtonsoft.Json;

    /// <summary>
    /// Showcases getting all orders.
    /// </summary>
    internal class GetOrders : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get All Orders"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;

            var orders = partnerOperations.Customers.ById(selectedCustomerId).Orders.Get();

            // display the orders
            Console.Out.WriteLine("Order count: " + orders.TotalCount);

            IList<Order> orderList = new List<Order>(orders.Items);

            foreach (var order in orderList)
            {
                Console.Out.WriteLine("Id: {0}", order.Id);
                Console.Out.WriteLine("Creation Date: {0}", order.CreationDate);
                Console.Out.WriteLine("Status: {0}", order.Status);
                Console.Out.WriteLine("Currency: {0}", order.CurrencyCode);
                Console.Out.WriteLine();
                Console.Out.WriteLine(JsonConvert.SerializeObject(order));
                Console.Out.WriteLine();
            }

            // store the subscriptions in the application state
            state[FeatureSamplesApplication.OrdersKey] = orderList;
        }
    }
}
