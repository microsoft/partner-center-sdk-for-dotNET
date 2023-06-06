// <copyright file="PartnerUsageSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Customers
{
    using System;
    using System.Collections.Generic;
    using Models.Customers;

    /// <summary>
    /// Showcases partner usage summary
    /// </summary>
    internal class PartnerUsageSummary : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Partner Usage Summary"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
           // get the summary of usage-based subscriptions for the customer
            var usageSummary = partnerOperations.UsageSummary.Get();

            Console.Out.WriteLine("Partner Id: {0}", usageSummary.ResourceId);
            Console.Out.WriteLine("Partner Name: {0}", usageSummary.ResourceName);
            Console.Out.WriteLine("BillingStartDate: {0}", usageSummary.BillingStartDate);
            Console.Out.WriteLine("BillingEndDate: {0}", usageSummary.BillingEndDate);
            Console.Out.WriteLine();
            Console.Out.WriteLine("Number of customers over their spending budget: {0}", usageSummary.CustomersOverBudget);
            Console.Out.WriteLine("Number of customer trending over: {0}", usageSummary.CustomersTrendingOver);
            Console.Out.WriteLine();
            Console.Out.WriteLine("Total cost: {0}", usageSummary.TotalCost);
            Console.Out.WriteLine("Currency locale: {0}", usageSummary.CurrencyLocale);
            Console.Out.WriteLine();
        }
    }
}
