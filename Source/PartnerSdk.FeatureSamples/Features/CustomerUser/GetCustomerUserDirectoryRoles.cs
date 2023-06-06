// -----------------------------------------------------------------------
// <copyright file="GetCustomerUserDirectoryRoles.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerUser
{
    using System;
    using System.Collections.Generic;
    using Models.Users;

    /// <summary>
    /// Showcases get customer user directory roles service.
    /// </summary>
    internal class GetCustomerUserDirectoryRoles : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Customer User Directory Roles"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partner">A reference to the partner.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partner, IDictionary<string, object> state)
        {
            // read the first customer user in the collection from the application state.
            CustomerUser selectedCustomerUser = ((List<CustomerUser>)state[FeatureSamplesApplication.CustomerUsersCollection])[0];

            // read the selected customer id from the application state.
            string selectedCustomerId = state[FeatureSamplesApplication.CustomersUserKey].ToString();

            // get user directory roles.
            var userMemberships = partner.Customers.ById(selectedCustomerId).Users.ById(selectedCustomerUser.Id).DirectoryRoles.Get();

            if (userMemberships != null && userMemberships.TotalCount > 0)
            {
                Console.WriteLine("Customer user directory role count: " + userMemberships.TotalCount);
                Console.WriteLine("User Principal Name: {0} ", selectedCustomerUser.UserPrincipalName);
                Console.WriteLine("Display Name: {0}", selectedCustomerUser.DisplayName);
                Console.WriteLine();

                foreach (var membership in userMemberships.Items)
                {
                    Console.WriteLine("Role Id: {0} ", membership.Id);
                    Console.WriteLine();
                }
            }
        }
    }
}
