// -----------------------------------------------------------------------
// <copyright file="CustomerSubscribedSkus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerSubscribedSkus
{
    using System;
    using System.Collections.Generic;
    using Models.Customers;
    using Models.Licenses;
    using RequestContext;

    /// <summary>
    /// Showcases for subscribed products of a customer tenant when no parameter is passed in Get method call.
    /// Expected behavior- When no parameter is passed, the Get() method returns only Group1 SKUs.
    /// </summary>
    internal class CustomerSubscribedSkus : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Subscribed Skus"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            // Read customer list.
            var customersBatch = (List<Customer>)state[FeatureSamplesApplication.CustomersList];

            if (customersBatch != null)
            {
                bool atleastOneCustomer = false;
                foreach (var customer in customersBatch)
                {
                    // read the selected customer id from the application state.
                    string selectedCustomerId = customer.Id;

                    // get customer user subscribed skus information.
                    // Note: The Get method returns only Group1 subscribed skus if no parameter is passed.
                    var customerUserSubscribedSkus = partnerOperations.Customers.ById(selectedCustomerId).SubscribedSkus.Get();

                    bool isNonGroup1SkuReturned = false;

                    // Verify that all skus have group1 license group id.
                    if (customerUserSubscribedSkus != null)
                    {
                        // Check if any non-Group1 sku was returned.
                        foreach (SubscribedSku sku in customerUserSubscribedSkus.Items)
                        {
                            if (sku.ProductSku.LicenseGroupId != LicenseGroupId.Group1)
                            {
                                isNonGroup1SkuReturned = true;
                                Console.WriteLine("Sku with license group id other than Group1 was returned.");
                                break;
                            }
                        }
                    }

                    if (!isNonGroup1SkuReturned)
                    {
                        if (customerUserSubscribedSkus != null && customerUserSubscribedSkus.TotalCount > 0)
                        {
                            ConsoleHelper.WriteSkusToConsole(customerUserSubscribedSkus.Items);

                            atleastOneCustomer = true;
                            break;
                        }
                    }
                }

                if (atleastOneCustomer == false)
                {
                    Console.Out.WriteLine("No Customer is having Gtoup1 Subscribed Sku.");
                }
            }
        }
    }
}
