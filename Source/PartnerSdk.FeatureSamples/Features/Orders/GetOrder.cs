// -----------------------------------------------------------------------
// <copyright file="GetOrder.cs" company="Microsoft Corporation">
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
    /// Showcases getting an order.
    /// </summary>
    internal class GetOrder : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Single Order"; }
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
            var selectedOrder = (state[FeatureSamplesApplication.OrdersKey] as List<Order>)[0];

            var order = partnerOperations.Customers.ById(selectedCustomerId).Orders.ById(selectedOrder.Id).Get();

            Console.Out.WriteLine("Id: {0}", order.Id);
            Console.Out.WriteLine("Creation Date: {0}", order.CreationDate);
            Console.Out.WriteLine();
            Console.Out.WriteLine(JsonConvert.SerializeObject(order));
            Console.Out.WriteLine();
        }
    }
}
