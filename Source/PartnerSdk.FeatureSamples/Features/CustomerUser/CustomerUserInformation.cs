// -----------------------------------------------------------------------
// <copyright file="CustomerUserInformation.cs" company="Microsoft Corporation">
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
    /// Showcases customer user information services.
    /// </summary>
    internal class CustomerUserInformation : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer User Detail"; }
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
            CustomerUser selectedCustomerUser = ((List<CustomerUser>)state[FeatureSamplesApplication.CustomerUsersCollection])[0];

            // read the selected customer user id from the application state.
            string selectedCustomerId = state[FeatureSamplesApplication.CustomersUserKey].ToString();

            // get customer user information
            CustomerUser customerUserInfo = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(selectedCustomerUser.Id).Get();

            // display customer user info
            Console.Out.WriteLine("Customer User Id: {0} ", customerUserInfo.Id);
            Console.Out.WriteLine("Customer User Name: {0}", customerUserInfo.DisplayName);
            Console.Out.WriteLine("Company User UPN : {0}", customerUserInfo.UserPrincipalName);
            Console.Out.WriteLine();
        }
    }
}
