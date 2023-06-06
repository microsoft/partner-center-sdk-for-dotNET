// <copyright file="CustomerServiceCostSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerServiceCosts
{   
    using System;
    using System.Collections.Generic;
    using Models.ServiceCosts;

    /// <summary>
    /// Showcases customer service cost summary.
    /// </summary>
    internal class CustomerServiceCostSummary : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Customer Serivce Cost Summary"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerWithServiceCosts] as string;

            var serviceCostsSummary = partnerOperations.Customers.ById(selectedCustomerId).ServiceCosts.ByBillingPeriod(ServiceCostsBillingPeriod.MostRecent).Summary.Get();

            // Display service costs summary.
            Console.Out.WriteLine("Customer Id: {0} ", serviceCostsSummary.CustomerId);
            Console.Out.WriteLine("Billing Start Date: {0} ", serviceCostsSummary.BillingStartDate);
            Console.Out.WriteLine("Billing End Date: {0} ", serviceCostsSummary.BillingEndDate);
            Console.Out.WriteLine("Pretax Total: {0} ", serviceCostsSummary.PretaxTotal);
            Console.Out.WriteLine("Tax: {0} ", serviceCostsSummary.Tax);
            Console.Out.WriteLine("After Tax Total: {0} ", serviceCostsSummary.AfterTaxTotal);
            Console.Out.WriteLine("Currency Code: {0} ", serviceCostsSummary.CurrencyCode);
            Console.Out.WriteLine("Currency Symbol: {0} ", serviceCostsSummary.CurrencySymbol);

            // Display service cost summary details per currency
            foreach (var detail in serviceCostsSummary.Details)
            {
                Console.Out.WriteLine("Invoice Type: {0} ", detail.InvoiceType);
                Console.Out.WriteLine("Customer Id: {0} ", detail.Summary.CustomerId);
                Console.Out.WriteLine("Billing Start Date: {0} ", detail.Summary.BillingStartDate);
                Console.Out.WriteLine("Billing End Date: {0} ", detail.Summary.BillingEndDate);
                Console.Out.WriteLine("Pretax Total: {0} ", detail.Summary.PretaxTotal);
                Console.Out.WriteLine("Tax: {0} ", detail.Summary.Tax);
                Console.Out.WriteLine("After Tax Total: {0} ", detail.Summary.AfterTaxTotal);
                Console.Out.WriteLine("Currency Code: {0} ", detail.Summary.CurrencyCode);
                Console.Out.WriteLine("Currency Symbol: {0} ", detail.Summary.CurrencySymbol);
            }

            Console.Out.WriteLine();
        }
    }
}
