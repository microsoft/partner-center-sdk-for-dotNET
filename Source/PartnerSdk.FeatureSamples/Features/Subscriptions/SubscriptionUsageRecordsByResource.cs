// -----------------------------------------------------------------------
// <copyright file="SubscriptionUsageRecordsByResource.cs" company="Microsoft Corporation">
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
    internal class SubscriptionUsageRecordsByResource : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get subscription usage records by resource"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// This method shows how to retrieve a subscription's resource usage records.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and subscription from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerForUsageDemo] as string;
            var selectedSubscriptionId = state[FeatureSamplesApplication.SubscriptionForUsageDemo] as string;

            var usageRecords = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscriptionId).UsageRecords.ByResource.Get();

            Console.Out.WriteLine("CustomerId: {0}", selectedCustomerId);
            Console.Out.WriteLine("SubscriptionId: {0}", selectedSubscriptionId);
            Console.Out.WriteLine("Quantity: {0}", usageRecords.TotalCount);
            Console.Out.WriteLine();

            foreach (var usageRecord in usageRecords.Items)
            {
                Console.Out.WriteLine("ResourceName: {0}", usageRecord.ResourceName);
                Console.Out.WriteLine("ResourceType: {0}", usageRecord.ResourceType);
                Console.Out.WriteLine("ResourceURI: {0}", usageRecord.ResourceUri);
                Console.Out.WriteLine("ResourceGroupName: {0}", usageRecord.ResourceGroupName);
                Console.Out.WriteLine("EntitlementId: {0}", usageRecord.EntitlementId);
                Console.Out.WriteLine("EntitlementName: {0}", usageRecord.EntitlementName);
                Console.Out.WriteLine("TotalCost: {0:F} {1}", usageRecord.TotalCost, usageRecord.CurrencyCode);
                Console.Out.WriteLine("TotalCostUSD: {0:F}", usageRecord.USDTotalCost);
                Console.Out.WriteLine();
            }
        }
    }
}
