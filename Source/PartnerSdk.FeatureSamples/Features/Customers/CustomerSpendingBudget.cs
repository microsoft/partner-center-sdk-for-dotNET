// <copyright file="CustomerSpendingBudget.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Customers
{
    using System;
    using System.Collections.Generic;
    using Models.Usage;

    /// <summary>
    /// Showcases customer spending budget.
    /// </summary>
    internal class CustomerSpendingBudget : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Usage Spending Budget"; }
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
            var usageBudget = partnerOperations.Customers.ById(selectedCustomerId).UsageBudget.Get();

            Console.Out.WriteLine("Current Spending Budget: {0}", usageBudget.Amount ?? -1);
            Console.Out.WriteLine();

            const decimal TestValue = 300.00M;

            var newUsageBudget = new Models.Usage.SpendingBudget
            {
                Amount = usageBudget.Amount == TestValue ? null : (decimal?)TestValue
            };

            usageBudget = partnerOperations.Customers.ById(selectedCustomerId).UsageBudget.Patch(newUsageBudget);

            Console.Out.WriteLine("New Spending Budget: {0}", usageBudget.Amount);
        }
    }
}
