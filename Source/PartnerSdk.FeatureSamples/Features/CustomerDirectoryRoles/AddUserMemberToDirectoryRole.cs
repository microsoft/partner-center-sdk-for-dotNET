// -----------------------------------------------------------------------
// <copyright file="AddUserMemberToDirectoryRole.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerDirectoryRoles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Customers;
    using Models.Roles;
    using Models.Users;
    using RequestContext;

    /// <summary>
    /// Showcases add a customer user to directory role service.
    /// </summary>
    internal class AddUserMemberToDirectoryRole : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Add customer user to a directory role"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            Customer selectedCustomer = ((List<Customer>)state[FeatureSamplesApplication.CustomersList])[0];

            // All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id.
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            var userToCreate = new CustomerUser()
            {
                PasswordProfile = new PasswordProfile() { ForceChangePassword = true, Password = "testPassword" },
                DisplayName = "TestDisplayName",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                UsageLocation = "US",
                UserPrincipalName = Guid.NewGuid().ToString("N") + "@" + selectedCustomer.CompanyProfile.Domain.ToString()
            };

            // Create a new customer user.
            User createdUser = partnerOperations.Customers.ById(selectedCustomer.Id).Users.Create(userToCreate);

            // Read all directory roles to get the first one's id.
            var selectedRole = partnerOperations.Customers.ById(selectedCustomer.Id).DirectoryRoles.Get().Items.First();

            UserMember userToAdd = new UserMember()
            {
                UserPrincipalName = createdUser.UserPrincipalName,
                DisplayName = createdUser.DisplayName,
                Id = createdUser.Id
            };

            // Add this customer user to the selected directory role.
            var userMemberAdded = partnerOperations.Customers.ById(selectedCustomer.Id).DirectoryRoles.ById(selectedRole.Id).UserMembers.Create(userToAdd);

            if (userMemberAdded != null)
            {
                Console.WriteLine("Below Customer user was added to directory role with name: {0}", selectedRole.Name);
                Console.WriteLine("User Principal Name: {0} ", userMemberAdded.UserPrincipalName);
                Console.WriteLine("Display Name: {0} ", userMemberAdded.DisplayName);
                Console.WriteLine("Member Id: {0}", userMemberAdded.Id);
                Console.WriteLine();
            }

            // Cleaning up, delete user.
            partnerOperations.Customers.ById(selectedCustomer.Id).Users.ById(createdUser.Id).Delete();
        }
    }
}