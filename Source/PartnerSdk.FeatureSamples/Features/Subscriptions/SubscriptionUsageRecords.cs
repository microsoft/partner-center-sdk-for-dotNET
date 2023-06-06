// -----------------------------------------------------------------------
// <copyright file="SubscriptionUsageRecords.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Retrieves a single customer's subscription usage records.
    /// </summary>
    internal class SubscriptionUsageRecords : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get subscription usage records"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// This method shows how to retrieve subscription's usage records.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and subscription from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerForUsageDemo] as string;

            var usageRecords = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.UsageRecords.Get();

            Console.Out.WriteLine("CustomerId: {0}", selectedCustomerId);
            Console.Out.WriteLine("Number of subscriptions: {0}", usageRecords.TotalCount);
            Console.Out.WriteLine();

            foreach (var usageRecord in usageRecords.Items)
            {
                Console.Out.WriteLine("Id: {0}", usageRecord.ResourceId);
                Console.Out.WriteLine("Name: {0}", usageRecord.ResourceName);
                Console.Out.WriteLine("Status: {0}", usageRecord.Status);
                Console.Out.WriteLine("Partner On Record: {0}", usageRecord.PartnerOnRecord);
                Console.Out.WriteLine("Offer Id: {0}", usageRecord.OfferId);
                Console.Out.WriteLine("TotalCost: {0:F} {1}", usageRecord.TotalCost, usageRecord.CurrencyCode);
                Console.Out.WriteLine("TotalCostUSD: {0:F}", usageRecord.USDTotalCost);

                Console.Out.WriteLine();
            }
        }
    }
}
