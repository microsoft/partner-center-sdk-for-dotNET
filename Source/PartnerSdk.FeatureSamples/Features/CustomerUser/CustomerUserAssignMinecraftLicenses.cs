// -----------------------------------------------------------------------
// <copyright file="CustomerUserAssignMinecraftLicenses.cs" company="Microsoft Corporation">
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
    internal class CustomerUserAssignMinecraftLicenses : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Assign Minecraft License To Customer User"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // Mine craft product id.
            string minecraftProductSkuId = "984df360-9a74-4647-8cf8-696749f6247a";

            bool isCustomerWithMineraftSkusExist = false;
            string customerId = state[FeatureSamplesApplication.DirectResellerTestAccountCustomerTenantId] as string;
            string userId = state[FeatureSamplesApplication.DirectResellerTestAccountUserId] as string;

            // All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            List<LicenseGroupId> group2LicenseGroupId = new List<LicenseGroupId>() { LicenseGroupId.Group2 };
            ResourceCollection<SubscribedSku> customerSubscribedSkus = null;

            customerSubscribedSkus = partnerOperations.Customers.ById(customerId).SubscribedSkus.Get(group2LicenseGroupId);

            if (customerSubscribedSkus != null && customerSubscribedSkus.TotalCount > 0)
            {
                // Find the customer with minecraft license
                foreach (SubscribedSku sku in customerSubscribedSkus.Items)
                {
                    if (sku.ProductSku.Id.ToString() == minecraftProductSkuId)
                    {
                        isCustomerWithMineraftSkusExist = true;
                        break;
                    }
                }
            }

            if (!isCustomerWithMineraftSkusExist)
            {
                Console.WriteLine("No customer with Minecraft sku was found.");
            }
            else
            {
                // Prepare license request.
                LicenseUpdate updateLicense = new LicenseUpdate();

                // Add the minecraft sku.
                LicenseAssignment license = new LicenseAssignment();
                license.SkuId = minecraftProductSkuId;
                license.ExcludedPlans = null;

                List<LicenseAssignment> licenseList = new List<LicenseAssignment>();
                licenseList.Add(license);
                updateLicense.LicensesToAssign = licenseList;

                // Assign minecraft license to the user.
                var assignMinecraftLicense = partnerOperations.Customers.ById(customerId).Users.ById(userId).LicenseUpdates.Create(updateLicense);

                // get customer user assigned Group2 licenses information after assigning the license.
                var customerUserAssignedGroup2Licenses = partnerOperations.Customers.ById(customerId).Users.ById(userId).Licenses.Get(group2LicenseGroupId);

                if (customerUserAssignedGroup2Licenses != null && customerUserAssignedGroup2Licenses.Items.FirstOrDefault(x => x.ProductSku.Id.Equals(minecraftProductSkuId)) != null)
                {
                    Console.WriteLine("Minecraft license was successfully assigned to the user.");
                    License userLicense = customerUserAssignedGroup2Licenses.Items.FirstOrDefault(x => x.ProductSku.Id.Equals(minecraftProductSkuId));

                    Console.Out.WriteLine("Customer User License name : {0}", userLicense.ProductSku.Name);
                    Console.Out.WriteLine("Customer User License Group Id : {0}", userLicense.ProductSku.LicenseGroupId);
                }

                // Remove the assigned minecraft license.
                updateLicense.LicensesToAssign = null;
                updateLicense.LicensesToRemove = new List<string>() { minecraftProductSkuId };
                assignMinecraftLicense = partnerOperations.Customers.ById(customerId).Users.ById(userId).LicenseUpdates.Create(updateLicense);

                // get customer user assigned Group2 licenses information after removing the license.
                customerUserAssignedGroup2Licenses = partnerOperations.Customers.ById(customerId).Users.ById(userId).Licenses.Get(group2LicenseGroupId);
                if (customerUserAssignedGroup2Licenses != null && customerUserAssignedGroup2Licenses.Items.FirstOrDefault(x => x.ProductSku.Id.Equals(minecraftProductSkuId)) == null)
                {
                    Console.WriteLine("Remove minecraft license operation failed.");
                }
                else
                {
                    Console.WriteLine("Minecraft license was successfully removed.");
                }
            }
        }
    }
}