// -----------------------------------------------------------------------
// <copyright file="GetOrderActivationLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Orders
{
    using System;
    using System.Collections.Generic;
    using Models.Orders;

    /// <summary>
    /// Showcases getting an order.
    /// </summary>
    internal class GetOrderActivationLinks : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get order Activation Links"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and the order from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.ActivationLinksCustomerKey] as string;
            var selectedOrderId = state[FeatureSamplesApplication.OrdersKey] as string;

            var orderActivationLinks = partnerOperations.Customers.ById(selectedCustomerId).Orders.ById(selectedOrderId).OrderActivationLinks.Get();

            Console.Out.WriteLine("ActivationLinks: {0}", orderActivationLinks);
            Console.Out.WriteLine();
        }
    }
}