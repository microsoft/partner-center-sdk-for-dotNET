// <copyright file="UpdateOverage.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models.Invoices;
    using Models.Subscriptions;

    /// <summary>
    /// Updates overage for subscriptions.
    /// </summary>
    internal class UpdateOverage : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Update Subscription Overage"; }
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

            // 1. Get subscriptions
            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Get();

            // 2. Check overage eligible subscriptions
            if (!customerSubscriptions.Items.Any(sub => sub.ConsumptionType == "overage"))
            {
                Console.Out.WriteLine("No overage eligible subscription found for the customer");
                return;
            }

            // 3. Check Modern Azure plan subscription and entitlement
            Subscription modernAzureSubscription = customerSubscriptions.Items.FirstOrDefault(sub => sub.BillingType == BillingType.Usage && !sub.OfferId.StartsWith("MS-AZR"));

            string azureEntitlementIdForOverage = null;
            if (modernAzureSubscription == null)
            {
                Console.WriteLine("No existing Azure plan found, a new azure plan will be purchased.");
            }
            else
            {
                Console.Out.WriteLine("Modern Azure Subscription Id: {0}", modernAzureSubscription.Id);

                var azureEntitlments = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(modernAzureSubscription.Id).GetAzurePlanSubscriptionEntitlements();
                var azureEntitlementForOverage = azureEntitlments.Items.FirstOrDefault(e => e.FriendlyName == "Subscription 1");
                azureEntitlementIdForOverage = azureEntitlementForOverage?.Id;
                if (modernAzureSubscription == null)
                {
                    Console.Out.WriteLine("No existing modern Azure plan entitlement found, a new azure entitlement will be created.");
                }
                else
                {
                    Console.Out.WriteLine("========================== Available Azure entitlements (Azure subscriptions) ==========================");
                    foreach (var entitlment in azureEntitlments.Items)
                    {
                        Console.Out.WriteLine($"Id: {entitlment.Id}, Friendly name: {entitlment.FriendlyName}");
                    }

                    Console.Out.Write($"Enter Azure entitlement Id for overage (Leaving empty will consider 'Subscription 1' entitlement if exists or else it will create new) [{azureEntitlementIdForOverage}]:");
                    azureEntitlementIdForOverage = Console.ReadLine();
                }
            }

            var overage = new Overage
            {
                AzureEntitlementId = azureEntitlementIdForOverage,
                OverageEnabled = true,
            };

            // 4. Create or update overage
            var updatedOverage = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Overage.Put(overage);

            Console.Out.WriteLine("========================== Updated Overage ==========================");
            Console.Out.WriteLine("Overage Azure entitlement id: {0}", updatedOverage.AzureEntitlementId);
            Console.Out.WriteLine("Overage enabled flag: {0}", updatedOverage.OverageEnabled);
            Console.Out.WriteLine("========================== Updated Overage ==========================");

            // 5. Get overage
            var newOverageItems = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Overage.Get();
            var newOverage = newOverageItems.Items.FirstOrDefault(o => o.Type == "PhoneServices");

            Console.Out.WriteLine("========================== New Overage ==========================");
            Console.Out.WriteLine("Overage Azure entitlement id: {0}", newOverage.AzureEntitlementId);
            Console.Out.WriteLine("Overage enabled flag: {0}", newOverage.OverageEnabled);
            Console.Out.WriteLine("========================== New Overage ==========================");

            if (!string.Equals(updatedOverage.AzureEntitlementId, newOverage.AzureEntitlementId))
            {
                Console.Out.WriteLine("Overaage update process hasn't completed yet.");
            }
        }
    }
}
