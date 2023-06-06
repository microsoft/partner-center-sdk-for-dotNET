// -----------------------------------------------------------------------
// <copyright file="CustomerUserAssignGroup1Licenses.cs" company="Microsoft Corporation">
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
    using Models.Licenses;
    using RequestContext;

    /// <summary>
    /// Showcases for assigned licenses to a customer user.
    /// </summary>
    internal class CustomerUserAssignGroup1Licenses : IUnitOfWork
    {
        /// <summary>
        /// Product Sku Id of One Drive.
        /// </summary>
        private const string OneDriveProductSkuId = "e6778190-713e-4e4f-9119-8b8238de25df";

        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Assign Group1 Licenses To Customer User"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            bool isCustomerWithSubscribedSkusExist = false;
            string customerId = state[FeatureSamplesApplication.DirectResellerTestAccountCustomerTenantId] as string;
            string userId = state[FeatureSamplesApplication.DirectResellerTestAccountUserId] as string;
            List<LicenseGroupId> group1LicenseGroupId = new List<LicenseGroupId>() { LicenseGroupId.Group1 };

            // All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            ResourceCollection<SubscribedSku> customerSubscribedSkus = null;

            // get customer's subscribed skus information.
            customerSubscribedSkus = partnerOperations.Customers.ById(customerId).SubscribedSkus.Get(group1LicenseGroupId);
            if (customerSubscribedSkus != null && customerSubscribedSkus.TotalCount > 0)
            {
                isCustomerWithSubscribedSkusExist = true;
            }

            if (!isCustomerWithSubscribedSkusExist)
            {
                Console.WriteLine("No customer with Group1 SubscribedSkus was found.");
            }
            else
            {
                // Prepare license request.
                LicenseUpdate updateLicense = new LicenseUpdate()
                {
                    LicensesToAssign = new List<LicenseAssignment>()
                    {
                        new LicenseAssignment
                        {
                            ExcludedPlans = new List<string> { "94065c59-bc8e-4e8b-89e5-5138d471eaff", "113feb6c-3fe4-4440-bddc-54d774bf0318", "882e1d05-acd1-4ccb-8708-6ee03664b117" },
                            SkuId = OneDriveProductSkuId
                        }
                    }
                };

                // Assign licenses to the user.
                var assignLicense = partnerOperations.Customers.ById(customerId).Users.ById(userId).LicenseUpdates.Create(updateLicense);

                // get customer user assigned licenses information after assigning the license.
                var customerUserAssignedGroup1Licenses = partnerOperations.Customers.ById(customerId).Users.ById(userId).Licenses.Get(group1LicenseGroupId);
                var userLicense = customerUserAssignedGroup1Licenses.Items.FirstOrDefault(x => x.ProductSku.Id.Equals(OneDriveProductSkuId));

                if (customerUserAssignedGroup1Licenses != null && userLicense != null)
                {
                    Console.WriteLine("Group1 License was successfully assigned to the user.");
                    Console.Out.WriteLine("Customer User License Group Id: {0}", userLicense.ProductSku.LicenseGroupId);

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
                updateLicense.LicensesToRemove = new List<string>() { OneDriveProductSkuId };
                assignLicense = partnerOperations.Customers.ById(customerId).Users.ById(userId).LicenseUpdates.Create(updateLicense);

                // get customer user assigned licenses information after removing the license.
                customerUserAssignedGroup1Licenses = partnerOperations.Customers.ById(customerId).Users.ById(userId).Licenses.Get(group1LicenseGroupId);
                userLicense = customerUserAssignedGroup1Licenses.Items.FirstOrDefault(x => x.ProductSku.Id.Equals(OneDriveProductSkuId));
                if (customerUserAssignedGroup1Licenses != null && userLicense != null)
                {
                    Console.WriteLine("Remove Group1 license operation failed.");
                }
                else
                {
                    Console.WriteLine("Group1 License was successfully removed.");
                }
            }
        }
    }
}