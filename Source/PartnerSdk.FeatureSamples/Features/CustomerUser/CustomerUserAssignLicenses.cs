// -----------------------------------------------------------------------
// <copyright file="CustomerUserAssignLicenses.cs" company="Microsoft Corporation">
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
    using Models.Users;
    using RequestContext;

    /// <summary>
    /// Showcases for assigned licenses to a customer user.
    /// </summary>
    internal class CustomerUserAssignLicenses : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Assign Licenses To Customer User"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            Customer selectedCustomer = new Customer();
            var customerList = partnerOperations.Customers.Get().Items;
            bool isCustomerWithSubscribedSkusExist = false;

            // All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            bool isNonGroup1SkusReturned = false;

            ResourceCollection<SubscribedSku> customerSubscribedSkus = null;
            foreach (Customer customer in customerList)
            {
                // get customer's subscribed skus information.
                // Note: The Get method returns only Group1 subscribed skus if no parameter is passed.
                customerSubscribedSkus = partnerOperations.Customers.ById(customer.Id).SubscribedSkus.Get();

                if (customerSubscribedSkus != null)
                {
                    foreach (SubscribedSku sku in customerSubscribedSkus.Items)
                    {
                        if (sku.ProductSku.LicenseGroupId != LicenseGroupId.Group1)
                        {
                            // Sku with license group other than Group1 was returned
                            isNonGroup1SkusReturned = true;
                            break;
                        }
                    }

                    if (customerSubscribedSkus.TotalCount > 0)
                    {
                        selectedCustomer = customer;
                        isCustomerWithSubscribedSkusExist = true;
                        break;
                    }
                }
            }

            if (isNonGroup1SkusReturned)
            {
                Console.WriteLine("Sku with other than Group1 license group id was returned.");
            }
            else
            {
                if (!isCustomerWithSubscribedSkusExist)
                {
                    Console.WriteLine("No customer with SubscribedSkus was found.");
                }
                else
                {
                    // Create a user for this customer.
                    var userToCreate = new CustomerUser()
                    {
                        PasswordProfile = new PasswordProfile() { ForceChangePassword = true, Password = "testPassword" },
                        DisplayName = "TestDisplayName",
                        FirstName = "TestFirstName",
                        LastName = "TestLastName",
                        UsageLocation = "US",
                        UserPrincipalName = Guid.NewGuid().ToString("N") + "@" + selectedCustomer.CompanyProfile.Domain.ToString()
                    };

                    User createdUser = partnerOperations.Customers.ById(selectedCustomer.Id).Users.Create(userToCreate);

                    // Prepare license request.
                    LicenseUpdate updateLicense = new LicenseUpdate();
                    string customerId = selectedCustomer.Id;
                    string userId = createdUser.Id;

                    // Select the first subscribed sku.
                    SubscribedSku sku = customerSubscribedSkus.Items.First();
                    LicenseAssignment license = new LicenseAssignment();
                    license.SkuId = sku.ProductSku.Id;
                    license.ExcludedPlans = null;

                    List<LicenseAssignment> licenseList = new List<LicenseAssignment>();
                    licenseList.Add(license);
                    updateLicense.LicensesToAssign = licenseList;

                    // Assign licenses to the user.
                    var assignLicense = partnerOperations.Customers.ById(selectedCustomer.Id).Users.ById(userId).LicenseUpdates.Create(updateLicense);

                    // get customer user assigned licenses information after assigning the license.
                    // Note: The Get method returns only Group1 licenses if no parameter is passed.
                    var customerUserAssignedLicenses = partnerOperations.Customers.ById(selectedCustomer.Id).Users.ById(userId).Licenses.Get();

                    if (customerUserAssignedLicenses != null && customerUserAssignedLicenses.TotalCount > 0)
                    {
                        Console.WriteLine("License was successfully assigned to the user.");
                        License userLicense = customerUserAssignedLicenses.Items.First();

                        var servicePlans = userLicense.ServicePlans.ToList();
                        Console.Out.WriteLine("Customer User License ServicePlans Count: {0}", servicePlans.Count);
                        foreach (ServicePlan servicePlan in servicePlans)
                        {
                            Console.Out.WriteLine("Customer User License service plan display name: {0}", servicePlan.DisplayName);
                            Console.Out.WriteLine("Customer User License service plan service name: {0}", servicePlan.ServiceName);
                            Console.Out.WriteLine("Customer User License service plan service id: {0}", servicePlan.Id);
                            Console.Out.WriteLine("Customer User License service plan capability status: {0}", servicePlan.CapabilityStatus);
                            Console.WriteLine();
                        }
                    }

                    // Remove the assigned license.
                    updateLicense.LicensesToAssign = null;
                    updateLicense.LicensesToRemove = new List<string>() { sku.ProductSku.Id };
                    assignLicense = partnerOperations.Customers.ById(selectedCustomer.Id).Users.ById(userId).LicenseUpdates.Create(updateLicense);

                    // get customer user assigned licenses information after removing the license.
                    // Note: The Get method returns only Group1 licenses if no parameter is passed.
                    customerUserAssignedLicenses = partnerOperations.Customers.ById(selectedCustomer.Id).Users.ById(userId).Licenses.Get();
                    if (customerUserAssignedLicenses != null && customerUserAssignedLicenses.TotalCount > 0)
                    {
                        Console.WriteLine("Remove license operation failed.");
                    }
                    else
                    {
                        Console.WriteLine("License was successfully removed.");
                    }

                    // Try to delete the created user.
                    partnerOperations.Customers.ById(selectedCustomer.Id).Users.ById(createdUser.Id).Delete();
                }
            }
        }
    }
}