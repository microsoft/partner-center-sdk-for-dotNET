// -----------------------------------------------------------------------
// <copyright file="AllCustomerUsageRecords.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Showcases partner's all customers usage record.
    /// </summary>
    internal class AllCustomerUsageRecords : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "All Customers Usage Record"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // get the summary of usage-based subscriptions for the customer
            var usageRecords = partnerOperations.Customers.UsageRecords.Get();

            foreach (var record in usageRecords.Items)
            {
                Console.Out.WriteLine("Customer Id: {0}", record.ResourceId);
                Console.Out.WriteLine("Customer Name: {0}", record.ResourceName);
                Console.Out.WriteLine("Usage Spending Budget: {0:C}", record.Budget.Amount);
                Console.Out.WriteLine("IsUpgraded: {0}", record.IsUpgraded);

                var currencySymbol = string.Empty;

                if (record.CurrencyLocale != null)
                {
                    var region = new RegionInfo(record.CurrencyLocale.ToString());
                    currencySymbol = region.CurrencySymbol;
                }

                Console.Out.WriteLine("Total Cost: {0} {1:F} {2}", currencySymbol, record.TotalCost, record.CurrencyCode);
                Console.Out.WriteLine("USD Total Cost: {0:F}", record.USDTotalCost);
                Console.Out.WriteLine("% Used: {0} %", record.PercentUsed);
                Console.Out.WriteLine();
            }
        }
    }
}
