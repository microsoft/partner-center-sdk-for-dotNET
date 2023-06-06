// <copyright file="GetAzurePlanEntitlement.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Get an Azure entitlement
    /// </summary>
    internal class GetAzurePlanEntitlement : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get an Azure entitlement"; }
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

            var azurePlanEntitlements = partnerOperations.Customers.ById(selectedCustomerId)
                .Subscriptions.ById(selectedSubscriptionId)
                .AzureEntitlements.Get();

            if (azurePlanEntitlements.TotalCount > 0)
            {
                foreach (var azureEntitlement in azurePlanEntitlements.Items)
                {
                    Console.Out.WriteLine($"Friendly name: {azureEntitlement.FriendlyName} - ID: {azureEntitlement.Id}");
                }

                Console.Out.WriteLine();
                Console.Out.Write("Enter Azure entitlement Id to retrieve: ");
                var enteredAzureEntitlementId = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(enteredAzureEntitlementId))
                {
                    Console.Out.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Out.WriteLine("Azure entitlement Id cannot be null");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Out.WriteLine();
                    return;
                }

                if (!azurePlanEntitlements.Items.Any(e => e.Id.Equals(enteredAzureEntitlementId, StringComparison.InvariantCultureIgnoreCase)))
                {
                    Console.Out.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Out.WriteLine("Entered Azure entitlement Id is incorrect.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Out.WriteLine();
                    return;
                }

                var enteredAzureEntitlement = partnerOperations.Customers.ById(selectedCustomerId)
                    .Subscriptions.ById(selectedSubscriptionId)
                    .AzureEntitlements.ById(enteredAzureEntitlementId)
                    .Get();

                Console.Out.WriteLine();
                Console.Out.WriteLine("FriendlyName: {0}", enteredAzureEntitlement.FriendlyName);
                Console.Out.WriteLine("Id: {0}", enteredAzureEntitlement.Id);
                Console.Out.WriteLine("SubscriptionId: {0}", enteredAzureEntitlement.SubscriptionId);
                Console.Out.WriteLine("Status: {0}", enteredAzureEntitlement.Status);
                Console.Out.WriteLine("Uri: {0}", enteredAzureEntitlement.Links.Self.Uri);
            }
            else
            {
                Console.Out.WriteLine("This Azure Plan contains no entitlements!");
                Console.Out.WriteLine();
            }
        }
    }
}
