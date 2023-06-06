// -----------------------------------------------------------------------
// <copyright file="CustomerUserUpdate.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerUser
{
    using System;
    using System.Collections.Generic;
    using Models.Users;
    using PartnerCenter.Models.Customers;

    /// <summary>
    /// Showcases customer user update services.
    /// </summary>
    internal class CustomerUserUpdate : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer User Update"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            string selectedCustomerTenantId = string.Empty;

            // read the first customer user in the collection from the application state.
            Customer selectedCustomer = ((List<Customer>)state[FeatureSamplesApplication.CustomersList])[0];

            // read the selected customer user id from the application state.
            string selectedCustomerId = state[FeatureSamplesApplication.CustomersUserKey].ToString();

            // Create a new user in customer tenant
            var userToCreate = new CustomerUser()
            {
                PasswordProfile = new PasswordProfile() { ForceChangePassword = true, Password = "testPassword" },
                DisplayName = "TestDisplayName",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                UsageLocation = "US",
                UserPrincipalName = Guid.NewGuid().ToString("N") + "@" + selectedCustomer.CompanyProfile.Domain.ToString()
            };

            CustomerUser createdUser = partnerOperations.Customers.ById(selectedCustomerId).Users.Create(userToCreate);

            // Specifying update attributes.
            var userToUpdate = new CustomerUser()
            {
                PasswordProfile = new PasswordProfile() { ForceChangePassword = true, Password = "testPassword" },
                DisplayName = "Roger Federer",
                FirstName = "Roger",
                LastName = "Federer",
                UsageLocation = "US",
                UserPrincipalName = Guid.NewGuid().ToString("N") + "@" + selectedCustomer.CompanyProfile.Domain.ToString()
            };

            // Update customer user information.
            User updatedCustomerUserInfo = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Patch(userToUpdate);

            // Get user details.
            User customerUserInfo = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Get();

            if (string.Equals(customerUserInfo.UserPrincipalName, updatedCustomerUserInfo.UserPrincipalName) && string.Equals(customerUserInfo.FirstName, updatedCustomerUserInfo.FirstName) && string.Equals(customerUserInfo.DisplayName, updatedCustomerUserInfo.DisplayName) && string.Equals(customerUserInfo.LastName, updatedCustomerUserInfo.LastName))
            {
                Console.WriteLine("The update operation was successful.");
                Console.WriteLine("First Name: " + customerUserInfo.FirstName);
                Console.WriteLine("Last Name: " + customerUserInfo.LastName);
                Console.WriteLine("Display Name: " + customerUserInfo.DisplayName);
                Console.WriteLine("Id: " + customerUserInfo.Id);
            }
            else
            {
                Console.WriteLine("The update was not successful.");
            }

            // Try to delete the created user.
            partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Delete();
        }
    }
}
