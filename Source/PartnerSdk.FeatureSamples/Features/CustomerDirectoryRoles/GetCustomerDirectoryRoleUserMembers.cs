// -----------------------------------------------------------------------
// <copyright file="GetCustomerDirectoryRoleUserMembers.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerDirectoryRoles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Query;
    using Models.Roles;
    using PartnerCenter.CustomerDirectoryRoles;

    /// <summary>
    /// Showcases get customer users by directory role service.
    /// </summary>
    public class GetCustomerDirectoryRoleUserMembers : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get customer user by directory role with paging"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // Read the selected customer user id from the application state.
            string selectedCustomerId = state[FeatureSamplesApplication.CustomersUserKey].ToString();

            // Read all directory roles to get the first one's id.
            var selectedRole = partnerOperations.Customers.ById(selectedCustomerId).DirectoryRoles.Get().Items.First();

            // Get all members having the selected directory role.
            var userMembers = partnerOperations.Customers.ById(selectedCustomerId).DirectoryRoles.ById(selectedRole.Id).UserMembers.Get();

            Console.WriteLine("Role Name: " + selectedRole.Name);
            Console.WriteLine("Role Id: " + selectedRole.Id);

            foreach (var userMember in userMembers.Items)
            {
                // Display customer user information.
                Console.WriteLine("Customer User Principal Name: {0} ", userMember.UserPrincipalName);
                Console.WriteLine("Display Name: {0} ", userMember.DisplayName);
                Console.WriteLine("Member Id: {0}", userMember.Id);
                Console.WriteLine();
            }

            var allUserMembers = new List<UserMember>();

            // Check pagination feature.
            var userMembersBatch = partnerOperations.Customers.ById(selectedCustomerId).DirectoryRoles.ById(selectedRole.Id).UserMembers.Query(QueryFactory.Instance.BuildIndexedQuery(10));

            while (userMembersBatch != null && userMembersBatch.TotalCount > 0 && userMembersBatch.Links.Next != null)
            {
                Console.WriteLine("User member count in this batch: " + userMembersBatch.TotalCount);
                Console.WriteLine();

                foreach (var userMember in userMembersBatch.Items)
                {
                    // Display user information.
                    Console.WriteLine("Customer User Principal Name: {0} ", userMember.UserPrincipalName);
                    Console.WriteLine("Display Name: {0} ", userMember.DisplayName);
                    Console.WriteLine("Member Id: {0}", userMember.Id);
                    Console.WriteLine();
                }

                allUserMembers.AddRange(userMembersBatch.Items);
                IQuery seekQuery = QueryFactory.Instance.BuildSeekQuery(SeekOperation.Next, token: userMembersBatch.ContinuationToken);
                userMembersBatch = partnerOperations.Customers.ById(selectedCustomerId).DirectoryRoles.ById(selectedRole.Id).UserMembers.Query(seekQuery);
            }
        }
    }
}
