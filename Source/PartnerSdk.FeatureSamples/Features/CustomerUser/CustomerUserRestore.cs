// -----------------------------------------------------------------------
// <copyright file="CustomerUserRestore.cs" company="Microsoft Corporation">
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
    /// Showcases customer user restore services.
    /// </summary>
    internal class CustomerUserRestore : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer User Restore"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            string selectedCustomerId = string.Empty;

            // read the first customer user in the collection from the application state.
            Customer selectedCustomer = null;
            var customerList = partnerOperations.Customers.Get().Items;

            foreach (Customer customer in customerList)
            {
                // get customer information
                Customer customerInfo = partnerOperations.Customers.ById(customer.Id).Get();

                // Check for the delegated access of the customer
                if (customerInfo.AllowDelegatedAccess == true)
                {
                    selectedCustomerId = customer.Id;
                    selectedCustomer = customer;
                    break;
                }
            }

            if (null != selectedCustomer)
            {
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

                // Get user details.
                User customerUserInfo = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Get();
                if (string.Equals(customerUserInfo.UserPrincipalName, userToCreate.UserPrincipalName) && string.Equals(customerUserInfo.FirstName, userToCreate.FirstName) && string.Equals(customerUserInfo.DisplayName, userToCreate.DisplayName) && string.Equals(customerUserInfo.LastName, userToCreate.LastName))
                {
                    Console.WriteLine("The get operation was successful.");
                    Console.WriteLine("First Name: " + customerUserInfo.FirstName);
                    Console.WriteLine("Last Name: " + customerUserInfo.LastName);
                    Console.WriteLine("Display Name: " + customerUserInfo.DisplayName);
                    Console.WriteLine("Id: " + customerUserInfo.Id);
                }

                // Try to delete the created user.
                partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Delete();

                // Try to restore the deleted user by specifying update attribute state as Active. And try to update the other attributes.
                var userToUpdate = new CustomerUser()
                {
                    PasswordProfile = new PasswordProfile() { ForceChangePassword = true, Password = "testPassword" },
                    DisplayName = "Roger Federer",
                    FirstName = "Roger",
                    LastName = "Federer",
                    UsageLocation = "US",
                    UserPrincipalName = createdUser.UserPrincipalName,
                    State = UserState.Active
                };

                // Update customer user information.
                User updatedCustomerUserInfo = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Patch(userToUpdate);

                // Get user details.
                customerUserInfo = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Get();
                if (string.Equals(customerUserInfo.UserPrincipalName, updatedCustomerUserInfo.UserPrincipalName) && string.Equals(customerUserInfo.FirstName, updatedCustomerUserInfo.FirstName) && string.Equals(customerUserInfo.DisplayName, updatedCustomerUserInfo.DisplayName) && string.Equals(customerUserInfo.LastName, updatedCustomerUserInfo.LastName))
                {
                    Console.WriteLine("The restore and update operation together was successful.");
                    Console.WriteLine("First Name: " + customerUserInfo.FirstName);
                    Console.WriteLine("Last Name: " + customerUserInfo.LastName);
                    Console.WriteLine("Display Name: " + customerUserInfo.DisplayName);
                    Console.WriteLine("Id: " + customerUserInfo.Id);
                }

                // Try to delete the created user.
                partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Delete();
            }
            else
            {
                Console.WriteLine("Cannot find a customer with delegated access permissions.");
            }
        }
    }
}
