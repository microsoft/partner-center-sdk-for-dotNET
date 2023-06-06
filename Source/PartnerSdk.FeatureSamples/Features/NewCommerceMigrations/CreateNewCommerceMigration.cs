// -----------------------------------------------------------------------
// <copyright file="CreateNewCommerceMigration.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.NewCommerceMigrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Customers;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// Showcases creating a New-Commerce migration.
    /// </summary>
    internal class CreateNewCommerceMigration : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Create a New-Commerce migration"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            ResourceCollection<Customer> customers = partnerOperations.Customers.Get();

            Console.Out.WriteLine("\n\n========================== Please select one of the following customer IDs ==========================\n");
            foreach (var customer in customers.Items)
            {
                Console.Out.WriteLine($"ID: {customer.Id}  |  Name: {customer.CompanyProfile?.CompanyName} ");
            }

            var customerTenantId = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(customerTenantId))
            {
                Console.Out.WriteLine("Please enter a non-empty customer ID");
                customerTenantId = Console.ReadLine();
            }

            var customerOperations = partnerOperations.Customers.ById(customerTenantId);

            ResourceCollection<Subscription> customerSubscriptions = customerOperations.Subscriptions.Get();

            Console.Out.WriteLine("\n\n========================== Please select one of the following Legacy subscription IDs ==========================\n");
            foreach (var subscription in customerSubscriptions.Items)
            {
                if (!subscription.OfferId.Contains(":"))
                {
                    Console.Out.WriteLine($"ID: {subscription.Id}  |  Name: {subscription.FriendlyName}  |  Status: {subscription.Status}");
                }
            }

            Console.Out.WriteLine(string.Empty);
            var subscriptionId = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(subscriptionId))
            {
                Console.Out.WriteLine("Please enter a non-empty subscription ID");
                subscriptionId = Console.ReadLine();
            }

            Console.Out.WriteLine("\n\n========================== Available subscriptions to align with ==========================\n");
            foreach (var sub in customerSubscriptions.Items)
            {
                if (!string.Equals("OnlineServicesNCE", sub.ProductType?.Id) || !string.Equals(SubscriptionStatus.Active, sub.Status) || sub.IsTrial)
                {
                    continue;
                }

                Console.Out.WriteLine($"Subscription:  {sub.Id} {sub.FriendlyName} {sub.CommitmentEndDate:u} {sub.TermDuration}");
            }

            Console.Out.Write($"\nEnter the custom term end date for the New-Commerce subscription or leave blank to keep your current end date\n");
            DateTime? customTermEndDate = null;

            var customTermEndDateValue = Console.ReadLine();
            if (!string.IsNullOrEmpty(customTermEndDateValue))
            {
                customTermEndDate = DateTime.Parse(customTermEndDateValue);
            }

            var newCommerceMigration = new NewCommerceMigration()
            {
                CurrentSubscriptionId = subscriptionId,
                CustomTermEndDate = customTermEndDate
            };

            Console.Out.WriteLine("Validating New-Commerce migration...\n");
            var newCommerceMigrationEligibility = customerOperations.NewCommerceMigrations.Validate(newCommerceMigration);

            if (newCommerceMigrationEligibility.IsEligible)
            {
                Console.Out.WriteLine("Subscription is eligible! Creating New-Commerce migration...\n");
                newCommerceMigration = customerOperations.NewCommerceMigrations.Create(newCommerceMigration);

                Console.Out.WriteLine("Successfully created a New-Commerce migration!\n");

                Console.Out.WriteLine("Getting newly created New-Commerce migration...\n");
                newCommerceMigration = customerOperations.NewCommerceMigrations.ById(newCommerceMigration.Id).Get();

                Console.Out.WriteLine("Successfully retrieved the New-Commerce migration by ID!\n");

                Console.Out.WriteLine("New-Commerce migration ID: {0}", newCommerceMigration.Id);
                Console.Out.WriteLine("Started Date: {0}", newCommerceMigration.StartedTime);

                if (newCommerceMigration.CustomTermEndDate.HasValue)
                {
                    Console.Out.WriteLine("Custom Term End Date: {0}", newCommerceMigration.CustomTermEndDate);
                }

                Console.Out.WriteLine();

                Console.Out.WriteLine("\n\n========================== Getting the events for this New-Commerce migration ==========================\n");

                IEnumerable<NewCommerceMigrationEvent> newCommerceMigrationEvents = customerOperations.NewCommerceMigrations.GetEvents(newCommerceMigration.Id, newCommerceMigration.CurrentSubscriptionId);

                foreach (var migrationEvent in newCommerceMigrationEvents)
                {
                    Console.Out.WriteLine("New-Commerce migration event ID: {0}", migrationEvent.Id);
                    Console.Out.WriteLine("Created time: {0}", migrationEvent.CreatedTime.Value.ToString("r"));
                    Console.Out.WriteLine("Event name: {0}", migrationEvent.EventName);
                    Console.Out.WriteLine();
                }
            }
            else
            {
                Console.Out.WriteLine($"Subscription is not eligible for migration to New-Commerce :(");
            }
            
            Console.Out.WriteLine();

            Console.Out.WriteLine("\n\n========================== Getting all New-Commerce migrations for this customer ==========================\n");

            var newCommerceMigrationsResponse = customerOperations.NewCommerceMigrations.Get(customerTenantId, null, null);

            foreach (var nceMigration in newCommerceMigrationsResponse.NewCommerceMigrations)
            {
                Console.Out.WriteLine("New-Commerce migration ID: {0}", nceMigration.Id);
                Console.Out.WriteLine("Status: {0}", nceMigration.Status);
                Console.Out.WriteLine("Started time: {0}", nceMigration.StartedTime.Value.ToString("r"));

                if (nceMigration.CompletedTime.HasValue)
                {
                    Console.Out.WriteLine("Completed time: {0}", nceMigration.CompletedTime.Value.ToString("r"));
                }

                Console.Out.WriteLine("Legacy subscription ID: {0}", nceMigration.CurrentSubscriptionId);

                if (!string.IsNullOrWhiteSpace(nceMigration.NewCommerceSubscriptionId))
                {
                    Console.Out.WriteLine("New-Commerce subscription ID: {0}", nceMigration.NewCommerceSubscriptionId);
                }

                if (nceMigration.CustomTermEndDate.HasValue)
                {
                    Console.Out.WriteLine("New-Commerce subscription custom term end date: {0}", nceMigration.CustomTermEndDate.Value.ToString("r"));
                }

                if (nceMigration.SubscriptionEndDate.HasValue)
                {
                    Console.Out.WriteLine("New-Commerce subscription end date: {0}", nceMigration.SubscriptionEndDate.Value.ToString("r"));
                }

                if (!string.IsNullOrWhiteSpace(nceMigration.NewCommerceOrderId))
                {
                    Console.Out.WriteLine("New-Commerce order ID: {0}", nceMigration.NewCommerceOrderId);
                }

                Console.Out.WriteLine("Catalog item ID: {0}", nceMigration.CatalogItemId);
                Console.Out.WriteLine("Quantity: {0}", nceMigration.Quantity);
                Console.Out.WriteLine("Term duration: {0}", nceMigration.TermDuration);
                Console.Out.WriteLine("Billing cycle: {0}", nceMigration.BillingCycle);
                Console.Out.WriteLine("Purchased full term: {0}", nceMigration.PurchaseFullTerm);        

                if (!string.IsNullOrWhiteSpace(nceMigration.ExternalReferenceId))
                {
                    Console.Out.WriteLine("External reference ID: {0}", nceMigration.ExternalReferenceId);
                }

                Console.Out.WriteLine();
            }
        }
    }
}
