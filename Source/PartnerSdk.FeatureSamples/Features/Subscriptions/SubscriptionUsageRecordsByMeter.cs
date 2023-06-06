// -----------------------------------------------------------------------
// <copyright file="SubscriptionUsageRecordsByMeter.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Retrieves a single subscription's usage records.
    /// </summary>
    internal class SubscriptionUsageRecordsByMeter : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get subscription usage records by meter"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// This method shows how to retrieve a subscription's meter usage records.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and subscription from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerForUsageDemo] as string;
            var selectedSubscriptionId = state[FeatureSamplesApplication.SubscriptionForUsageDemo] as string;

            var usageRecords = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscriptionId).UsageRecords.ByMeter.Get();

            Console.Out.WriteLine("CustomerId: {0}", selectedCustomerId);
            Console.Out.WriteLine("SubscriptionId: {0}", selectedSubscriptionId);
            Console.Out.WriteLine("Quantity: {0}", usageRecords.TotalCount);
            Console.Out.WriteLine();

            foreach (var usageRecord in usageRecords.Items)
            {
                Console.Out.WriteLine("Meter Id: {0}", usageRecord.MeterId);
                Console.Out.WriteLine("Meter Name: {0}", usageRecord.MeterName);
                Console.Out.WriteLine("Category: {0}", usageRecord.Category);
                Console.Out.WriteLine("SubCategory: {0}", usageRecord.Subcategory);
                Console.Out.WriteLine("QuantityUsed: {0}", usageRecord.QuantityUsed);
                Console.Out.WriteLine("Unit: {0}", usageRecord.Unit);
                Console.Out.WriteLine("TotalCost: {0:F} {1}", usageRecord.TotalCost, usageRecord.CurrencyCode);
                Console.Out.WriteLine("TotalCostUSD: {0:F}", usageRecord.USDTotalCost);
                Console.Out.WriteLine();
            }
        }
    }
}
