// <copyright file="SubscriptionProvisioningStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Store.PartnerCenter.Models;
    using Models.Subscriptions;

    /// <summary>
    /// Retrieves subscription provisioning status details.
    /// </summary>
    internal class SubscriptionProvisioningStatus : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Subscription Provisioning status details"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and subscription from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;

            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Get();

            if (customerSubscriptions == null || customerSubscriptions.TotalCount == 0)
            {
                Console.Out.WriteLine("There are no subscriptions for this customer");
                Console.Out.WriteLine();
                return;
            }

            Subscription selectedSubscription = customerSubscriptions.Items.FirstOrDefault();

            var subscriptionProvisioningDetails = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription?.Id).ProvisioningStatus.Get();

            Console.Out.WriteLine("SkuId: {0}", subscriptionProvisioningDetails.SkuId);
            Console.Out.WriteLine("Quantity: {0}", subscriptionProvisioningDetails.Quantity);
            Console.Out.WriteLine("Provisioning Status: {0}", subscriptionProvisioningDetails.Status);
            Console.Out.WriteLine("End date: {0}", subscriptionProvisioningDetails.EndDate);
            Console.Out.WriteLine();
        }
    }
}
