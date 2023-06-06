// -----------------------------------------------------------------------
// <copyright file="CancelAzurePlanEntitlement.cs" company="Microsoft Corporation">
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
    /// Cancel Azure entitlement.
    /// </summary>
    /// <seealso cref="Microsoft.Store.PartnerCenter.FeatureSamples.IUnitOfWork" />
    public class CancelAzurePlanEntitlement : IUnitOfWork
    {
        /// <summary>
        /// The Azure entitlement 'Active' status.
        /// </summary>
        private const string AzureEntitlementStatus = "active";

        /// <summary>
        /// Gets the title of the unit of work.
        /// </summary>
        public string Title => "Cancel an Azure subscription (entitlement)";

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

            Console.Out.WriteLine("========================== Azure Plan ==========================");
            Console.Out.WriteLine($"Name:           {usageRecords.Items.FirstOrDefault().ResourceName}");
            Console.Out.WriteLine($"Id:             {usageRecords.Items.FirstOrDefault().OfferId}");
            Console.Out.WriteLine($"OfferId:        {selectedSubscriptionId}");
            Console.Out.WriteLine($"Currency:       {usageRecords.Items.FirstOrDefault().CurrencyCode}");
            Console.Out.WriteLine("========================== Azure Plan ==========================");

            var azurePlanEntitlements =
                partnerOperations.Customers.ById(selectedCustomerId)
                .Subscriptions.ById(selectedSubscriptionId)
                .GetAzurePlanSubscriptionEntitlements();

            // Filter out only active Azure entitlements.
            var activeAzureEntitlements = azurePlanEntitlements.Items.Where(e => e.Status.ToLowerInvariant() == AzureEntitlementStatus);
            
            Console.Out.WriteLine();
            if (activeAzureEntitlements.Count() > 0)
            {
                Console.Out.WriteLine("========================== Available Azure Entitlements ==========================");
                foreach (var azureEntitlement in activeAzureEntitlements)
                {
                    Console.Out.WriteLine($"Friendly name: {azureEntitlement.FriendlyName} - ID: {azureEntitlement.Id}");
                }

                Console.Out.WriteLine("========================== Available Azure Entitlements ==========================");

                Console.Out.WriteLine();
                Console.Out.Write("Enter Azure entitlement Id to cancel: ");
                var azureEntitlementId = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(azureEntitlementId))
                {
                    Console.Out.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Out.WriteLine("Azure entitlement Id cannot be null");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Out.WriteLine();
                    return;
                }

                if (!activeAzureEntitlements.Any(e => e.Id.Equals(azureEntitlementId, StringComparison.InvariantCultureIgnoreCase)))
                {
                    Console.Out.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Out.WriteLine("Entered Azure entitlement Id is incorrect.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Out.WriteLine();
                    return;
                }

                Console.Out.WriteLine();
                Console.Out.Write("Enter the cancellation reason code (ex: compromise): ");
                var selectedCancellationReasonCode = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(selectedCancellationReasonCode))
                {
                    Console.Out.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Out.WriteLine("Azure entitlement cancellation reason code cannot be null");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Out.WriteLine();
                    return;
                }

                if (selectedCancellationReasonCode != "compromise")
                {
                    Console.Out.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Out.Write("Entered cancellation reason code is not supported. Please enter valid cancellation reason code.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Out.WriteLine();
                    return;
                }

                var azureEntitlementCancellationRequestContent = new AzureEntitlementCancellationRequestContent
                {
                    CancellationReason = selectedCancellationReasonCode
                };

                var selectedAzureEntitlement = azurePlanEntitlements.Items.Single(e => e.Id == azureEntitlementId);
                AzureEntitlement cancelledAzureEntitlement;

                Console.Out.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Cancel selected Azure entitlement [{selectedAzureEntitlement.Id}] (y/n) ? ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Out.WriteLine();

                if (Console.ReadKey().KeyChar == 'y')
                {
                    cancelledAzureEntitlement = partnerOperations.Customers.ById(selectedCustomerId)
                        .Subscriptions.ById(selectedSubscriptionId)
                        .AzureEntitlements.ById(azureEntitlementId)
                        .Cancel(azureEntitlementCancellationRequestContent);

                    Console.Out.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Out.WriteLine($"Azure entitlement has been canceled. Changes may take up to 10 minutes to reflect.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Out.WriteLine();

                    Console.Out.WriteLine("========================== Canceled Azure Entitlements ==========================");
                    Console.Out.WriteLine("FriendlyName: {0}", cancelledAzureEntitlement.FriendlyName);
                    Console.Out.WriteLine("Id: {0}", cancelledAzureEntitlement.Id);
                    Console.Out.WriteLine("SubscriptionId: {0}", cancelledAzureEntitlement.SubscriptionId);
                    Console.Out.WriteLine("Status: {0}", cancelledAzureEntitlement.Status);
                    Console.Out.WriteLine("Uri: {0}", cancelledAzureEntitlement.Links.Self.Uri);
                    Console.Out.WriteLine("========================== Canceled Azure Entitlements ==========================");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine($"No active Azure entitlement/'s found for Azure plan with Id: {selectedSubscriptionId}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Out.WriteLine();
            }
        }
    }
}
