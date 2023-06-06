// -----------------------------------------------------------------------
// <copyright file="SortedCustomerUsers.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerUser
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.Models.Query;
    using Microsoft.Store.PartnerCenter.RequestContext;
    using Models.Users;
    using PartnerCenter.Models.Customers;

    /// <summary>
    /// Showcases sorting of customer users.
    /// </summary>
    internal class SortedCustomerUsers : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer users sorted by name in descending order"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            string selectedCustomerId = string.Empty;

            if (state.ContainsKey(FeatureSamplesApplication.CustomersUserKey))
            {
                selectedCustomerId = state[FeatureSamplesApplication.CustomersUserKey].ToString();
            }

            if (string.IsNullOrEmpty(selectedCustomerId))
            {
                // Get the customers who has users greater than 0
                var customers = partnerOperations.Customers.Query(QueryFactory.Instance.BuildIndexedQuery(15)).Items;
                foreach (Customer customer in customers)
                {
                    var customerUser = partnerOperations.Customers.ById(customer.Id).Users.Get();
                    if (customerUser.TotalCount > 0)
                    {
                        selectedCustomerId = customer.Id;
                        break;
                    }

                    // Add the customer id in the application state
                    state[FeatureSamplesApplication.CustomersUserKey] = selectedCustomerId;
                }
            }

            // Read the customer users for selectedCustomerId providing the sort option.
            var customerUsers = partnerOperations.Customers.ById(selectedCustomerId)
                                                 .Users.Query(QueryFactory.Instance.BuildIndexedQuery(100, sortOption: new Sort("DisplayName", SortDirection.Descending)));

            if (customerUsers != null && customerUsers.TotalCount > 0)
            {
                Console.Out.WriteLine("Customer user count: " + customerUsers.TotalCount);
                Console.Out.WriteLine();

                foreach (var user in customerUsers.Items)
                {
                    Console.Out.WriteLine("Name: {0}", user.DisplayName);
                    Console.Out.WriteLine("Id: {0}", user.Id);
                    Console.Out.WriteLine();
                }
            }
        }
    }
}
