// <copyright file="UpgradeSubscription.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Store.PartnerCenter.FeatureSamples;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// Showcases upgrading a subscription.
    /// </summary>
    internal class UpgradeSubscription : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title => "Subscription Upgrades";

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerForOrderSvc] as string;

            if (selectedCustomerId == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("Customer ID cannot be null");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            // get the subscriptions for the customer
            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Get();

            if (customerSubscriptions?.Items == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("Subscriptions cannot be null");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            string subscriptionIdForUpgrade = string.Empty;
            Upgrade targetOffer = null;

            // display the subscriptions count
            Console.Out.WriteLine("Subscriptions count: " + customerSubscriptions.TotalCount);
            Console.Out.WriteLine();

            foreach (var subscription in customerSubscriptions.Items)
            {
                Console.Out.WriteLine("Checking to see if subscription {0} ({1}) can be upgraded", subscription.Id, subscription.FriendlyName);

                //// TODO: Remove this check when upgrades are supported for modern products
                if (subscription.OfferId.IndexOf('-') <= 0)
                {
                    Console.Out.WriteLine("There are no upgrades for modern products today");
                    continue;
                }

                ResourceCollection<Upgrade> subscriptionUpgrades = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(subscription.Id).Upgrades.Get();

                if (subscriptionUpgrades.TotalCount == 0)
                {
                    Console.Out.WriteLine("> It cannot be upgraded");
                }
                else
                {
                    Console.Out.WriteLine("Checking eligibility for upgrade");
                    foreach (Upgrade subscriptionUpgrade in subscriptionUpgrades.Items)
                    {
                        if (subscriptionUpgrade.IsEligible)
                        {
                            // Select the first available transition to perform upgrade
                            if (string.IsNullOrWhiteSpace(subscriptionIdForUpgrade))
                            {
                               subscriptionIdForUpgrade = subscription.Id;
                                targetOffer = subscriptionUpgrade;
                            }

                            Console.Out.WriteLine("Subscription can be upgraded to {0} ({1}) using UpgradeType {2}.  Valid quantities are from {3} to {4}", subscriptionUpgrade.TargetOffer.Id, subscriptionUpgrade.TargetOffer.Name, subscriptionUpgrade.UpgradeType, subscriptionUpgrade.TargetOffer.MinimumQuantity, subscriptionUpgrade.TargetOffer.MaximumQuantity);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Out.WriteLine("Subscription cannot be upgraded to {0} ({1}) using UpgradeType {2} due to the following reasons", subscriptionUpgrade.TargetOffer.Id, subscriptionUpgrade.TargetOffer.Name, subscriptionUpgrade.UpgradeType);
                            foreach (var error in subscriptionUpgrade.UpgradeErrors)
                            {
                                Console.Out.WriteLine("{0} - {1}  Additional Details: {2}", error.Code, error.Description, error.AdditionalDetails);
                            }

                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.Out.WriteLine();
                    }
                }

                Console.Out.WriteLine();
            }

            if (targetOffer == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("Unable to find any subscriptions that can be upgraded");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.Out.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Out.WriteLine("Upgrading Subscription {0} for Customer {1} to Target Offer {2} with TransferType of {3}", subscriptionIdForUpgrade, selectedCustomerId, targetOffer.TargetOffer.Name, targetOffer.UpgradeType);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine();

            UpgradeResult upgradeResult = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(subscriptionIdForUpgrade).Upgrades.Create(targetOffer);

            if (upgradeResult.UpgradeErrors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("Upgrade errors encountered: ");
                foreach (var upgradeError in upgradeResult.UpgradeErrors)
                {
                    Console.Out.WriteLine("{0} - {1}  Additional Details: {2}", upgradeError.Code, upgradeError.Description, upgradeError.AdditionalDetails);
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Out.WriteLine();
            }

            if (upgradeResult.LicenseErrors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("License errors encountered: ");
                foreach (var licenseError in upgradeResult.LicenseErrors)
                {
                    Console.Out.WriteLine("{0}:  {1} - {2}", licenseError.UserObjectId, licenseError.Name, licenseError.Email);
                    foreach (var exception in licenseError.Errors)
                    {
                        Console.WriteLine(exception.ToString());
                        Console.Out.WriteLine();
                    }

                    Console.Out.WriteLine();
                    Console.Out.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Out.WriteLine();
            }

            if (upgradeResult.UpgradeErrors.Any() || upgradeResult.LicenseErrors.Any())
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Upgrade {0} completed successfully.  Source SubscriptionId {1}, Target SubscriptionId {2}", upgradeResult.UpgradeType, upgradeResult.SourceSubscriptionId, upgradeResult.TargetSubscriptionId);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine();
            Console.Out.WriteLine();
        }
    }
}
