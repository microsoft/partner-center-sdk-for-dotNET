// -----------------------------------------------------------------------
// <copyright file="CustomerUserAssignedGroup2Licenses.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerUser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Models.Customers;
    using Models.Licenses;
    using RequestContext;

    /// <summary>
    /// Showcases for assigned licenses to a customer user.
    /// </summary>
    internal class CustomerUserAssignedGroup2Licenses : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer User Group2 Assigned Licenses"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            string selectedCustomerId = string.Empty;
            string selectedCustomerUserId = string.Empty;
            ResourceCollection<License> customerUserGroup2AssignedLicenses;

            // Read customer list who have DAP from state variable.
            string customerIdWithProducts = state[FeatureSamplesApplication.CustomerWithProducts] as string;

            List<LicenseGroupId> licenseGroupId = new List<LicenseGroupId>() { LicenseGroupId.Group2 };
            if (customerIdWithProducts != null)
            {
                // All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
                IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

                var customerUsers = scopedPartnerOperations.Customers.ById(customerIdWithProducts).Users.Get();
                if (customerUsers != null && customerUsers.TotalCount > 0)
                {
                    // Looping customer user collection to find out which customer user has Group2 assigned licenses greater than 0
                    foreach (var user in customerUsers.Items)
                    {
                        customerUserGroup2AssignedLicenses = partnerOperations.Customers.ById(customerIdWithProducts).Users.ById(user.Id).Licenses.Get(licenseGroupId);
                        if (customerUserGroup2AssignedLicenses != null && customerUserGroup2AssignedLicenses.TotalCount > 0)
                        {
                            selectedCustomerId = customerIdWithProducts;
                            selectedCustomerUserId = user.Id;

                            break;
                        }
                    }
                }
            }

            // get customer user Group2 assigned licenses information.
            customerUserGroup2AssignedLicenses = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(selectedCustomerUserId).Licenses.Get(licenseGroupId);
            if (customerUserGroup2AssignedLicenses != null && customerUserGroup2AssignedLicenses.TotalCount > 0)
            {
                ConsoleHelper.WriteLicensesToConsole(customerUserGroup2AssignedLicenses.Items);
            }
        }
    }
}
