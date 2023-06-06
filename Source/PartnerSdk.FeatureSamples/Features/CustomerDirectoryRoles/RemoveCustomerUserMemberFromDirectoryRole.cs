// -----------------------------------------------------------------------
// <copyright file="RemoveCustomerUserMemberFromDirectoryRole.cs" company="Microsoft Corporation">
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
    /// Showcases remove customer user from directory role service.
    /// </summary>
    internal class RemoveCustomerUserMemberFromDirectoryRole : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Remove customer user from a directory role"; }
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

            UserMember userToUpdate = new UserMember()
            {
                UserPrincipalName = createdUser.UserPrincipalName,
                DisplayName = createdUser.DisplayName,
                Id = createdUser.Id
            };

            // Add this customer user to the selected directory role.
            var userMemberAdded = partnerOperations.Customers.ById(selectedCustomer.Id).DirectoryRoles.ById(selectedRole.Id).UserMembers.Create(userToUpdate);

            try
            {
                // Remove customer user from selected directory role.
                partnerOperations.Customers.ById(selectedCustomer.Id).DirectoryRoles.ById(selectedRole.Id).UserMembers.ById(userMemberAdded.Id).Delete();

                Console.WriteLine("The user with user principal name: {0} was removed from directory role: {1}", userMemberAdded.UserPrincipalName, selectedRole.Name);
            }
            catch
            {
                throw;
            }

            // Cleaning up, delete user.
            partnerOperations.Customers.ById(selectedCustomer.Id).Users.ById(createdUser.Id).Delete();
        }
    }
}
