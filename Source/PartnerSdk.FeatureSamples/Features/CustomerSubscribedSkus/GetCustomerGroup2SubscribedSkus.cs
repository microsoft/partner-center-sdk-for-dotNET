// -----------------------------------------------------------------------
// <copyright file="GetCustomerGroup2SubscribedSkus.cs" company="Microsoft Corporation">
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
    /// Showcases for subscribed products of a customer tenant.
    /// </summary>
    internal class GetCustomerGroup2SubscribedSkus : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Customer Group2 Subscribed Skus"; }
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

                    // get customer user Group2 subscribed skus information.
                    List<LicenseGroupId> licenseGroupId = new List<LicenseGroupId>() { LicenseGroupId.Group2 };
                    var customerUserSubscribedSkus = partnerOperations.Customers.ById(selectedCustomerId).SubscribedSkus.Get(licenseGroupId);
                    if (customerUserSubscribedSkus != null && customerUserSubscribedSkus.TotalCount > 0)
                    {
                        ConsoleHelper.WriteSkusToConsole(customerUserSubscribedSkus.Items);

                        atleastOneCustomer = true;
                        break;
                    }
                }

                if (atleastOneCustomer == false)
                {
                    Console.Out.WriteLine("No Customer is having Group2 Subscribed Sku.");
                }
            }
        }
    }
}
