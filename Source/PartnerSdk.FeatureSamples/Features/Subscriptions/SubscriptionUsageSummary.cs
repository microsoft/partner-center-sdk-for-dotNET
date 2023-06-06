// <copyright file="SubscriptionUsageSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using Models.Customers;
    using Models.Subscriptions;

    /// <summary>
    /// Retrieves a single subscription's usage summary.
    /// </summary>
    internal class SubscriptionUsageSummary : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Subscription Usage Summary"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// This method shows how to retrieve subscription's usage summary.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and subscription from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerForUsageDemo] as string;
            var selectedSubscriptionId = state[FeatureSamplesApplication.SubscriptionForUsageDemo] as string;

            var usageSummary = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscriptionId).UsageSummary.Get();

            Console.Out.WriteLine("Subscription Id: {0}", usageSummary.ResourceId);
            Console.Out.WriteLine("Subscription Name: {0}", usageSummary.ResourceName);
            Console.Out.WriteLine("BillingStartDate: {0}", usageSummary.BillingStartDate);
            Console.Out.WriteLine("BillingEndDate: {0}", usageSummary.BillingEndDate);
            Console.Out.WriteLine("TotalCost: {0:F} {1}", usageSummary.TotalCost, usageSummary.CurrencyCode);
            Console.Out.WriteLine("TotalCostUSD: {0:F}", usageSummary.USDTotalCost);

            Console.Out.WriteLine();
        }
    }
}
