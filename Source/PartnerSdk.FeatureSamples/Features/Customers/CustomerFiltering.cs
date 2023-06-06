// <copyright file="CustomerFiltering.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Customers
{
    using System;
    using System.Collections.Generic;
    using Models.Customers;
    using Models.Query;

    /// <summary>
    /// Showcases customer filtering.
    /// </summary>
    internal class CustomerFiltering : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Filtering, Customers who start with: A"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // try searching for customers whom display names start with "A"
            var customers = partnerOperations.Customers.Query(QueryFactory.Instance.BuildSimpleQuery(new SimpleFieldFilter(CustomerSearchField.CompanyName.ToString(), FieldFilterOperation.StartsWith, "A")));

            Console.Out.WriteLine("Customer count: " + customers.TotalCount);

            foreach (var customer in customers.Items)
            {
                Console.Out.WriteLine("Customer Id: {0}", customer.Id);
                Console.Out.WriteLine("Company Name: {0}", customer.CompanyProfile.CompanyName);
                Console.Out.WriteLine();
            }
        }
    }
}
