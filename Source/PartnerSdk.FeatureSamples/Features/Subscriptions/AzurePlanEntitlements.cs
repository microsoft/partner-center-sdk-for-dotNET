// <copyright file="AzurePlanEntitlements.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// Get list of entitlements under an Azure Plan subscription
    /// </summary>
    internal class AzurePlanEntitlements : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get list of entitlements under an Azure Plan subscription"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and subscription from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedProductUpgradeCustomerKey] as string;
            var selectedProductName = state[FeatureSamplesApplication.SelectedProductName] as string;
            var usageRecords = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.UsageRecords.Get();
            var selectedSubscriptionId = usageRecords.Items.FirstOrDefault(i => i.ResourceName == selectedProductName.ToString()).ResourceId;

            var azurePlanEntitlements = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscriptionId).GetAzurePlanSubscriptionEntitlements();

            IList<AzureEntitlement> azurePlanEntitlementsList = new List<AzureEntitlement>(azurePlanEntitlements.Items);

            if (azurePlanEntitlementsList.Count > 0)
            {
                foreach (var entitlement in azurePlanEntitlementsList)
                {
                    Console.Out.WriteLine("friendlyName: {0}", entitlement.FriendlyName);
                    Console.Out.WriteLine("id: {0}", entitlement.Id);
                    Console.Out.WriteLine("subscriptionId: {0}", entitlement.SubscriptionId);
                    Console.Out.WriteLine("status: {0}", entitlement.Status);
                    Console.Out.WriteLine("uri: {0}", entitlement.Links.Self.Uri);
                    Console.Out.WriteLine();
                }
            }
            else
            {
                Console.Out.WriteLine("This subscription contains no entitlements!");
                Console.Out.WriteLine();
            }            
        }
    }
}
