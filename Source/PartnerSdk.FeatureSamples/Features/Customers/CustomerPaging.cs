// <copyright file="CustomerPaging.cs" company="Microsoft Corporation">
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
    using RequestContext;

    /// <summary>
    /// Showcases customer paging.
    /// </summary>
    internal class CustomerPaging : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Paging"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var allCustomers = new List<Customer>();

            // all the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            // read customers into chunks of 40s
            var customersBatch = scopedPartnerOperations.Customers.Query(QueryFactory.Instance.BuildIndexedQuery(40));
            var customersEnumerator = scopedPartnerOperations.Enumerators.Customers.Create(customersBatch);

            while (customersEnumerator.HasValue)
            {
                Console.Out.WriteLine("Customer count: " + customersEnumerator.Current.TotalCount);

                foreach (var customer in customersEnumerator.Current.Items)
                {
                    Console.Out.WriteLine("Customer Id: {0}", customer.Id);
                    Console.Out.WriteLine("Company Name: {0}", customer.CompanyProfile.CompanyName);
                    Console.Out.WriteLine();
                }

                allCustomers.AddRange(customersBatch.Items);
                customersEnumerator.Next();
            }

            // add the customers to the application state
            state[FeatureSamplesApplication.CustomersKey] = allCustomers;
        }
    }
}
