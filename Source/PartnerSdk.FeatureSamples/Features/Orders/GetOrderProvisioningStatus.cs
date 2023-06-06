// -----------------------------------------------------------------------
// <copyright file="GetOrderProvisioningStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Orders
{
    using System;
    using System.Collections.Generic;
    using Models.Customers;
    using Models.Orders;

    /// <summary>
    /// Showcases getting an order.
    /// </summary>
    internal class GetOrderProvisioningStatus : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get order provisioning status"; }
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

            var provisioningStatusList = partnerOperations.Customers.ById(selectedCustomerId).Orders.ById(selectedOrder.Id).ProvisioningStatus.Get();

            Console.Out.WriteLine("Order Id: {0}", selectedOrder.Id);
            foreach (var provisioningStatus in provisioningStatusList.Items)
            {
                foreach (var orderItem in selectedOrder.LineItems)
                {
                    if (orderItem.LineItemNumber == provisioningStatus.LineItemNumber)
                    {
                        Console.Out.WriteLine("Provisioning Status: {0}", provisioningStatus.Status);
                    }
                }
            }

            Console.Out.WriteLine();
        }
    }
}
