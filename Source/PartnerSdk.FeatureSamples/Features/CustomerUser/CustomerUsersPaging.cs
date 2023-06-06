// -----------------------------------------------------------------------
// <copyright file="CustomerUsersPaging.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerUser
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.CustomerUsers;
    using Microsoft.Store.PartnerCenter.Models.Query;
    using Microsoft.Store.PartnerCenter.RequestContext;
    using Models.Users;

    /// <summary>
    /// Showcases customer user paging services.
    /// </summary>
    internal class CustomerUsersPaging : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Users Paging"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // Read the customer id from the application state.
            var selectedCustomerId = state[FeatureSamplesApplication.CustomersUserKey] as string;

            // All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            // Read 40 customer users in a batch
            var customerUsersBatch = scopedPartnerOperations.Customers.ById(selectedCustomerId).Users.Query(QueryFactory.Instance.BuildIndexedQuery(25));
            var customerUsersEnumerator = scopedPartnerOperations.Enumerators.CustomerUsers.Create(customerUsersBatch);

            while (customerUsersEnumerator.HasValue)
            {
                Console.Out.WriteLine("Customer user page count: " + customerUsersEnumerator.Current.TotalCount);

                foreach (var user in customerUsersEnumerator.Current.Items)
                {
                    Console.Out.WriteLine("Customer User Id: {0}", user.Id);
                    Console.Out.WriteLine("Company User Name: {0}", user.DisplayName);
                    Console.Out.WriteLine();
                }

                if (customerUsersBatch.Links.Next == null)
                {
                    break;
                }

                IQuery seekQuery = QueryFactory.Instance.BuildSeekQuery(SeekOperation.Next, token: customerUsersBatch.ContinuationToken);
                customerUsersBatch = scopedPartnerOperations.Customers.ById(selectedCustomerId).Users.Query(seekQuery);
                customerUsersEnumerator = scopedPartnerOperations.Enumerators.CustomerUsers.Create(customerUsersBatch);
            }
        }
    }
}