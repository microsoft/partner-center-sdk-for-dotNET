// -----------------------------------------------------------------------
// <copyright file="CustomerUserDelete.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerUser
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.Exceptions;
    using Models.Customers;
    using Models.Users;
    using RequestContext;

    /// <summary>
    /// Showcases delete customer user services.
    /// </summary>
    internal class CustomerUserDelete : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer User Delete"; }
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

            // Delete customer user
            partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Delete();

            // Display success message
            Console.Out.WriteLine("Customer successfully deleted.");

            // get customer user information which is just deleted to make sure deletion is success. Exception will be thrown in this case as user not found now.
            User customerUserInfo = new User();
            try
            {
                customerUserInfo = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(createdUser.Id).Get();
            }
            catch (PartnerException ex)
            {
                if (ex.ServiceErrorPayload.ErrorCode == "3000")
                {
                    Console.Out.WriteLine("User does not exists and has been successfully deleted. {0} " + ex.ToString());
                }
            }

            Console.Out.WriteLine();
        }
    }
}
