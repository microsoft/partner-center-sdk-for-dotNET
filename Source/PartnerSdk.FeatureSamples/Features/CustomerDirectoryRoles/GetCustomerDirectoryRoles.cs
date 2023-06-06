// -----------------------------------------------------------------------
// <copyright file="GetCustomerDirectoryRoles.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerDirectoryRoles
{
    using System;
    using System.Collections.Generic;
    using PartnerCenter.CustomerDirectoryRoles;

    /// <summary>
    /// Showcases get customer directory role service.
    /// </summary>
    internal class GetCustomerDirectoryRoles : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get customer directory roles"; }
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

            var directoryRoles = partnerOperations.Customers.ById(selectedCustomerId).DirectoryRoles.Get();

            if (directoryRoles != null && directoryRoles.TotalCount > 0)
            {
                Console.WriteLine("Total directory roles: {0}", directoryRoles.TotalCount);

                foreach (var role in directoryRoles.Items)
                {
                    // Display directory role information.
                    Console.WriteLine("Directory role Id: {0} ", role.Id);
                    Console.WriteLine("Directory role Name: {0}", role.Name);
                    Console.WriteLine();
                }
            }
        }
    }
}
