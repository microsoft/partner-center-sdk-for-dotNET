// -----------------------------------------------------------------------
// <copyright file="CustomerUserCreate.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerUser
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.RequestContext;
    using PartnerCenter.Models.Customers;
    using PartnerCenter.Models.Users;

    /// <summary>
    /// Showcases customer user collection services.
    /// </summary>
    internal class CustomerUserCreate : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Create Customer User"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            Customer selectedCustomer = ((List<Customer>)state[FeatureSamplesApplication.CustomersList])[0];

            // read the selected customer id from the application state.
            string selectedCustomerId = selectedCustomer.Id;

            // All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
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

            User createdUser = partnerOperations.Customers.ById(selectedCustomerId).Users.Create(userToCreate);

            Console.WriteLine("Here are the details of the created user:");
            Console.WriteLine("First Name: " + createdUser.FirstName);
            Console.WriteLine("Last Name: " + createdUser.LastName);
            Console.WriteLine("Display Name: " + createdUser.DisplayName);
            Console.WriteLine("Customer id: " + createdUser.Id);

            // Try to delete the created user.
            partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Delete();
        }
    }
}