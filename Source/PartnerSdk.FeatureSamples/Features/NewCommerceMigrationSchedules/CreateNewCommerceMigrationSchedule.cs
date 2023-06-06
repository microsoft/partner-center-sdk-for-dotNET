// -----------------------------------------------------------------------
// <copyright file="CreateNewCommerceMigrationSchedule.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.NewCommerceMigrationSchedules
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Customers;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrationSchedules;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// Showcases creating a New-Commerce migration.
    /// </summary>
    internal class CreateNewCommerceMigrationSchedule : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Create a New-Commerce migration schedule"; }
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
                    Console.Out.WriteLine($"ID: {subscription.Id}  |  Name: {subscription.FriendlyName}  |  Status: {subscription.Status}  | Quantity: {subscription.Quantity}  | TermDuration:  {subscription.TermDuration}  |  BillingCycle:  {subscription.BillingCycle}");
                }
            }

            Console.Out.WriteLine(string.Empty);
            var subscriptionId = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(subscriptionId))
            {
                Console.Out.WriteLine("Please enter a non-empty subscription ID");
                subscriptionId = Console.ReadLine();
            }

            Console.Out.Write($"\nDo you want to use a specific target date to schedule the migration?[y/n]\n");

            var shouldUseTargetDate = Console.ReadLine();

            DateTime? targetDate = null;
            bool? migrateOnRenewal = null;
            if (!string.IsNullOrWhiteSpace(shouldUseTargetDate) && string.Equals(shouldUseTargetDate, "y", StringComparison.OrdinalIgnoreCase))
            {
                Console.Out.Write($"\nEnter the target date to schedule the migration\n");
                targetDate = DateTime.Parse(Console.ReadLine());
            }
            else
            {
                migrateOnRenewal = true;
            }

            Console.Out.Write($"\nEnter the quantity if different from the legacy subscription\n");
            var quantityInput = Console.ReadLine();
            int.TryParse(quantityInput, out int quantity);

            Console.Out.Write($"\nEnter the term duration if different from the legacy subscription\n");
            var termDurationInput = Console.ReadLine();
            string termDuration = null;
            if (!string.IsNullOrWhiteSpace(termDurationInput))
            {
                termDuration = termDurationInput;
            }

            Console.Out.Write($"\nEnter the billing cycle if different from the legacy subscription\n");
            var billingCycleInput = Console.ReadLine();
            string billingCycle = null;
            if (!string.IsNullOrWhiteSpace(billingCycleInput))
            {
                billingCycle = billingCycleInput;
            }

            var newCommerceMigrationSchedule = new NewCommerceMigrationSchedule()
            {
                CurrentSubscriptionId = subscriptionId,
                TargetDate = targetDate,
                MigrateOnRenewal = migrateOnRenewal,
                Quantity = quantity,
                TermDuration = termDuration,
                BillingCycle = billingCycle,
            };

            Console.Out.WriteLine("Creating New-Commerce migration schedule...\n");
            var createdNewCommerceSchedule = customerOperations.NewCommerceMigrationSchedules.Create(newCommerceMigrationSchedule);

            Console.Out.WriteLine("Successfully created a New-Commerce migration!\n");

            Console.Out.WriteLine("Getting newly created New-Commerce migration...\n");
            newCommerceMigrationSchedule = customerOperations.NewCommerceMigrationSchedules.ById(createdNewCommerceSchedule.Id).Get();

            Console.Out.WriteLine("Successfully retrieved the New-Commerce migration by ID!\n");

            Console.Out.WriteLine("New-Commerce migration ID: {0}", newCommerceMigrationSchedule.Id);

            if (newCommerceMigrationSchedule.TargetDate.HasValue)
            {
                Console.Out.WriteLine("Target Date: {0}", newCommerceMigrationSchedule.TargetDate);
            }

            if (newCommerceMigrationSchedule.MigrateOnRenewal.HasValue)
            {
                Console.Out.WriteLine("MigrateOnRenewal: {0}", newCommerceMigrationSchedule.MigrateOnRenewal);
            }

            Console.Out.WriteLine("Status: {0}", newCommerceMigrationSchedule.Status);
            Console.Out.WriteLine("Quantity: {0}", newCommerceMigrationSchedule.Quantity);
            Console.Out.WriteLine("Term duration: {0}", newCommerceMigrationSchedule.TermDuration);
            Console.Out.WriteLine("Billing cycle: {0}", newCommerceMigrationSchedule.BillingCycle);
            Console.Out.WriteLine("Purchased full term: {0}", newCommerceMigrationSchedule.PurchaseFullTerm);

            Console.Out.WriteLine();

            Console.Out.WriteLine("\n\n========================== Getting all New-Commerce migrations for this customer ==========================\n");

            var newCommerceMigrationSchedulesResponse = customerOperations.NewCommerceMigrationSchedules.Get(customerTenantId, null, null);

            foreach (var nceMigrationSchedule in newCommerceMigrationSchedulesResponse.NewCommerceMigrationSchedules)
            {
                Console.Out.WriteLine("New-Commerce migration schedule id: {0}", nceMigrationSchedule.Id);
                Console.Out.WriteLine("New-Commerce migration schedule status: {0}", nceMigrationSchedule.Status);
                if (newCommerceMigrationSchedule.TargetDate.HasValue)
                {
                    Console.Out.WriteLine("Target date: {0}", nceMigrationSchedule.TargetDate.Value.ToString("r"));
                }

                if (newCommerceMigrationSchedule.MigrateOnRenewal.HasValue)
                {
                    Console.Out.WriteLine("MigrateOnRenewal: {0}", nceMigrationSchedule.MigrateOnRenewal.Value);
                }

                Console.Out.WriteLine("Legacy subscription ID: {0}", nceMigrationSchedule.CurrentSubscriptionId);

                if (!string.IsNullOrWhiteSpace(nceMigrationSchedule.NewCommerceMigrationId))
                {
                    Console.Out.WriteLine("New-Commerce migration ID: {0}", nceMigrationSchedule.NewCommerceMigrationId);
                }

                if (nceMigrationSchedule.CustomTermEndDate.HasValue)
                {
                    Console.Out.WriteLine("New-Commerce subscription custom term end date: {0}", nceMigrationSchedule.CustomTermEndDate.Value.ToString("r"));
                }

                if (nceMigrationSchedule.SubscriptionEndDate.HasValue)
                {
                    Console.Out.WriteLine("New-Commerce subscription end date: {0}", nceMigrationSchedule.SubscriptionEndDate.Value.ToString("r"));
                }

                Console.Out.WriteLine("Status: {0}", nceMigrationSchedule.Status);
                Console.Out.WriteLine("Quantity: {0}", nceMigrationSchedule.Quantity);
                Console.Out.WriteLine("Term duration: {0}", nceMigrationSchedule.TermDuration);
                Console.Out.WriteLine("Billing cycle: {0}", nceMigrationSchedule.BillingCycle);
                Console.Out.WriteLine("Purchased full term: {0}", nceMigrationSchedule.PurchaseFullTerm);

                if (!string.IsNullOrWhiteSpace(nceMigrationSchedule.ExternalReferenceId))
                {
                    Console.Out.WriteLine("External reference ID: {0}", nceMigrationSchedule.ExternalReferenceId);
                }

                Console.Out.WriteLine();
            }
        }
    }
}