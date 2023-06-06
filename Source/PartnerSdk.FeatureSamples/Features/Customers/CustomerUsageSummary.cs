// <copyright file="CustomerUsageSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Customers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Showcases customer managed services.
    /// </summary>
    internal class CustomerUsageSummary : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Usage Summary"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerForUsageDemo] as string;

            // get the summary of usage-based subscriptions for the customer
            var usageSummary = partnerOperations.Customers.ById(selectedCustomerId).UsageSummary.Get();

            Console.Out.WriteLine("Customer Id: {0}", usageSummary.ResourceId);
            Console.Out.WriteLine("Customer Name: {0}", usageSummary.ResourceName);
            Console.Out.WriteLine("BillingStartDate: {0}", usageSummary.BillingStartDate);
            Console.Out.WriteLine("BillingEndDate: {0}", usageSummary.BillingEndDate);
            Console.Out.WriteLine("TotalCost: {0:F} {1}", usageSummary.TotalCost, usageSummary.CurrencyCode);
            Console.Out.WriteLine("TotalCostUSD: {0:F}", usageSummary.USDTotalCost);
            Console.Out.WriteLine("Spending Budget: {0:C}", usageSummary.Budget.Amount);
            Console.Out.WriteLine();
        }
    }
}
