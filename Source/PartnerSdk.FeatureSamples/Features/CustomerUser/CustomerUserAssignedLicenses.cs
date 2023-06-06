// -----------------------------------------------------------------------
// <copyright file="CustomerUserAssignedLicenses.cs" company="Microsoft Corporation">
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
    /// Showcases for assigned licenses to a customer user when no parameter is passed when calling Get() method.
    /// Expected behavior- When no parameter is passed, the Licenses.Get() returns only Group1 licenses.
    /// </summary>
    internal class CustomerUserAssignedLicenses : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer User Assigned Licenses"; }
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
            ResourceCollection<License> customerUserAssignedLicenses;

            // Read customer list who have DAP from state variable.
            var customersBatch = partnerOperations.Customers.Get().Items;
            bool isNonGroup1LicenseReturned = false;

            if (customersBatch != null)
            {
                // Looping customer collection to find out customers who has users greater than 0
                foreach (Customer customer in customersBatch)
                {
                    // All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
                    IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

                    var customerUsers = scopedPartnerOperations.Customers.ById(customer.Id).Users.Get();
                    if (customerUsers != null && customerUsers.TotalCount > 0)
                    {
                        // Looping customer user collection to find out which customer user has assigned licenses greater than 0
                        foreach (var user in customerUsers.Items)
                        {
                            // Note: The Get method will return only Group1 licenses if no parameters are passed.
                            customerUserAssignedLicenses = partnerOperations.Customers.ById(customer.Id).Users.ById(user.Id).Licenses.Get();

                            if (customerUserAssignedLicenses != null)
                            {
                                // Check that all licenses belong to license group id Group1.
                                foreach (License licesnse in customerUserAssignedLicenses.Items)
                                {
                                    if (licesnse.ProductSku.LicenseGroupId != LicenseGroupId.Group1)
                                    {
                                        isNonGroup1LicenseReturned = true;
                                        Console.WriteLine("License for which license group id is other than Group1 was returned.");
                                        break;
                                    }
                                }

                                if (isNonGroup1LicenseReturned == false && customerUserAssignedLicenses.TotalCount > 0)
                                {
                                    selectedCustomerId = customer.Id;
                                    selectedCustomerUserId = user.Id;

                                    break;
                                }
                            }
                        }
                    }

                    if ((selectedCustomerId != string.Empty) && (selectedCustomerUserId != string.Empty))
                    {
                        break;
                    }
                }
            }

            if (!isNonGroup1LicenseReturned)
            {
                // get customer user assigned licenses information.
                customerUserAssignedLicenses = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(selectedCustomerUserId).Licenses.Get();
                if (customerUserAssignedLicenses != null && customerUserAssignedLicenses.TotalCount > 0)
                {
                    ConsoleHelper.WriteLicensesToConsole(customerUserAssignedLicenses.Items);
                }
            }
        }
    }
}
