// -----------------------------------------------------------------------
// <copyright file="UpdateNewCommerceMigrationSchedule.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.NewCommerceMigrationSchedules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Customers;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrationSchedules;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// Showcases creating a New-Commerce migration.
    /// </summary>
    internal class UpdateNewCommerceMigrationSchedule : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Update a New-Commerce migration schedule"; }
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

            var newCommerceMigrationSchedules = customerOperations.NewCommerceMigrationSchedules.Get(customerTenantId: customerTenantId).NewCommerceMigrationSchedules;

            Console.Out.WriteLine("\n\n========================== Available schedule migrations ==========================\n");
            foreach (var existingNewCommerceMigrationSchedule in newCommerceMigrationSchedules)
            {
                Console.Out.WriteLine($"ID: {existingNewCommerceMigrationSchedule.Id}  |  CurrentSubscriptionId: {existingNewCommerceMigrationSchedule.CurrentSubscriptionId}  |  Status: {existingNewCommerceMigrationSchedule.Status}  | Quantity: {existingNewCommerceMigrationSchedule.Quantity}  | TermDuration:  {existingNewCommerceMigrationSchedule.TermDuration}  |  BillingCycle:  {existingNewCommerceMigrationSchedule.BillingCycle}");
            }

            Console.Out.WriteLine();
            Console.Out.WriteLine("Enter NewCommerceMigrationScheduleId for update");
            var newCommerceMigrationScheduleId = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(newCommerceMigrationScheduleId))
            {
                Console.Out.WriteLine("Please enter a non-empty NewCommerceMigrationScheduleId");
                newCommerceMigrationScheduleId = Console.ReadLine();
            }

            var selectedNewCommerceMigrationSchedule = newCommerceMigrationSchedules.Single(n => n.Id.Equals(newCommerceMigrationScheduleId));

            Console.Out.Write($"\nEnter the new quantity if you want to update\n");
            var quantityInput = Console.ReadLine();
            if (int.TryParse(quantityInput, out int quantity))
            {
                selectedNewCommerceMigrationSchedule.Quantity = quantity;
            }

            Console.Out.Write($"\nEnter the new term if you want to update\n");
            var termDurationInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(termDurationInput))
            {
                selectedNewCommerceMigrationSchedule.TermDuration = termDurationInput;
            }

            Console.Out.Write($"\nEnter the billing cycle if you want to update\n");
            var billingCycleInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(billingCycleInput))
            {
                selectedNewCommerceMigrationSchedule.BillingCycle = billingCycleInput;
            }

            Console.Out.WriteLine("Updating New-Commerce migration schedule...\n");
            var updatedNewCommerceSchedule = customerOperations.NewCommerceMigrationSchedules.ById(selectedNewCommerceMigrationSchedule.Id).Update(selectedNewCommerceMigrationSchedule);

            Console.Out.WriteLine("Successfully updated the New-Commerce migration schedule!\n");

            Console.Out.WriteLine("Getting updated New-Commerce migration schedule...\n");
            var newCommerceMigrationSchedule = customerOperations.NewCommerceMigrationSchedules.ById(updatedNewCommerceSchedule.Id).Get();

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

            Console.Out.WriteLine("\n\n========================== Getting all New-Commerce migration schedules for this customer ==========================\n");

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

                Console.Out.WriteLine("Status: {0}", nceMigrationSchedule.Status);
                Console.Out.WriteLine("New-Commerce subscription end date: {0}", nceMigrationSchedule.SubscriptionEndDate.Value.ToString("r"));
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