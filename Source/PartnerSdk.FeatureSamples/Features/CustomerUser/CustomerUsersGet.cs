// -----------------------------------------------------------------------
// <copyright file="CustomerUsersGet.cs" company="Microsoft Corporation">
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
    /// Showcases customer user collection services.
    /// </summary>
    internal class CustomerUsersGet : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Users Get Collection"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            string selectedCustomerId = string.Empty;
            var allCustomerUsers = new List<CustomerUser>();

            // All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            // Looping customer collection to find out customers who has users greater than 0
            List<Customer> customersBatch = (List<Customer>)scopedPartnerOperations.Customers.Query(QueryFactory.Instance.BuildIndexedQuery(15)).Items;

            // Unable to remove list elements from foreach loop http://stackoverflow.com/questions/1582285/how-to-remove-elements-from-a-generic-list-while-iterating-over-it
            // Store the indexes of the customers who do not have DAP and users and remove them after loop.
            List<string> customerIdsToBeRemoved = new List<string>();

            foreach (Customer customer in customersBatch)
            {
                // get customer information
                Customer customerInfo = scopedPartnerOperations.Customers.ById(customer.Id).Get();

                // Check for the delegated access of the customer
                if (customerInfo.AllowDelegatedAccess == true)
                {
                    var customerUser = scopedPartnerOperations.Customers.ById(customer.Id).Users.Get();
                    if (customerUser.TotalCount > 0)
                    {
                        selectedCustomerId = customer.Id;
                        break;
                    }
                }

                // this customer does not have DAP permission or has no users, so remove it after loop exit.
                customerIdsToBeRemoved.Add(customerInfo.Id);
            }

            // remove the customers who do not have DAP.
            customersBatch.RemoveAll(item => customerIdsToBeRemoved.Contains(item.Id));

            // Add the customer list to the application state.
            state[FeatureSamplesApplication.CustomersList] = customersBatch;

            // Read all customer users .
            var customerUsers = scopedPartnerOperations.Customers.ById(selectedCustomerId).Users.Get();

            if (customerUsers != null && customerUsers.TotalCount > 0)
            {
                Console.Out.WriteLine("Customer user count: " + customerUsers.TotalCount);

                foreach (var user in customerUsers.Items)
                {
                    Console.Out.WriteLine("Customer User Id: {0}", user.Id);
                    Console.Out.WriteLine("Company User Name: {0}", user.DisplayName);
                    Console.Out.WriteLine();
                }

                allCustomerUsers.AddRange(customerUsers.Items);
            }

            // Add the customer users to the application state
            state[FeatureSamplesApplication.CustomerUsersCollection] = allCustomerUsers;

            // Add the customer id in the application state
            state[FeatureSamplesApplication.CustomersUserKey] = selectedCustomerId;
        }
    }
}
