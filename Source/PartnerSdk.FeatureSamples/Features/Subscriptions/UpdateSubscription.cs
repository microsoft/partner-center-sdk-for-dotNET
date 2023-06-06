// <copyright file="UpdateSubscription.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models.Offers;
    using Microsoft.Store.PartnerCenter.RequestContext;
    using Models.Subscriptions;

    /// <summary>
    /// Updates a subscription.
    /// </summary>
    internal class UpdateSubscription : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Update Subscription"; }
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

            Console.Out.WriteLine("========================== Available Active Subscriptions ==========================");
            foreach (var sub in customerSubscriptions.Items)
            {
                if (sub.Status == SubscriptionStatus.Active)
                {
                    Console.Out.WriteLine($"Subscription:  {sub.Id}  {sub.OfferId}  {sub.FriendlyName}");
                }
            }

            Console.Out.WriteLine();
            Console.Out.Write("Enter subscriptionId for update: ");
            var subscriptionId = Console.ReadLine();

            Subscription selectedSubscription = customerSubscriptions.Items.Single(sub => sub.Id == subscriptionId);

            Console.Out.WriteLine("========================== Existing Subscription ==========================");
            Console.Out.WriteLine($"Uri:           {selectedSubscription.Links.Self.Uri.ToString()}");
            Console.Out.WriteLine($"Quantity:      {selectedSubscription.Quantity}");
            Console.Out.WriteLine($"PartnerId:     {selectedSubscription.PartnerId}");
            Console.Out.WriteLine($"Term Duration: {selectedSubscription.TermDuration}");
            Console.Out.WriteLine($"Billing Cycle: {selectedSubscription.BillingCycle}");
            Console.Out.WriteLine("========================== Existing Subscription ==========================");

            Subscription updatedSubscription;

            Console.WriteLine($"Update quantity for subscription [{selectedSubscription.Id}] (y/n) ? ");

            if (Console.ReadKey().KeyChar == 'y')
            {
                selectedSubscription.Quantity++;

                updatedSubscription = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).Patch(selectedSubscription);

                Console.Out.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Out.WriteLine($"Quantity successfully been updated on subscription {updatedSubscription.Id}. Changes may take a little bit to show up.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Out.WriteLine();

                return;
            }

            Console.Out.WriteLine();
            Console.WriteLine($"Update partner on record for subscription [{selectedSubscription.Id}] (y/n) ? ");

            if (Console.ReadKey().KeyChar == 'y')
            {
                selectedSubscription.PartnerId = selectedSubscription.PartnerId != null
                ? (selectedSubscription.PartnerId.Equals("4458802") ? "4579995" : "4458802")
                : null;

                updatedSubscription = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).Patch(selectedSubscription);

                Console.Out.WriteLine("========================== Updated Subscription ==========================");
                Console.Out.WriteLine("Uri:       {0}", updatedSubscription.Links.Self.Uri.ToString());
                Console.Out.WriteLine("PartnerId: {0}", updatedSubscription.PartnerId);
                Console.Out.WriteLine("========================== Updated Subscription ==========================");

                return;
            }

            Console.Out.WriteLine();
            Console.WriteLine($"Update term duration and billing cycle for subscription [{selectedSubscription.Id}] (y/n) ? ");

            if (Console.ReadKey().KeyChar == 'y')
            {
                var psa = selectedSubscription.OfferId.Split(':');

                if (psa.Length > 1)
                {
                    // Get availabilities
                    var availability = partnerOperations.Customers.ById(selectedCustomerId).Products.ById(psa[0]).Skus.ById(psa[1]).Availabilities.ById(psa[2]).Get();

                    Console.Out.WriteLine();
                    Console.Out.WriteLine("========================== Available Term duration & Billing cycle ==========================");
                    foreach (var term in availability.Terms)
                    {
                        Console.Out.WriteLine($"{term.Duration}  {term.BillingCycle}");
                    }

                    Console.Out.WriteLine();
                    Console.Out.Write($"Enter the term duration change (Must be longer than current term) [Example - {availability.Terms.FirstOrDefault().Duration}] : ");
                    var changeToTermDuration = Console.ReadLine();
                    if (string.IsNullOrEmpty(changeToTermDuration))
                    {
                        changeToTermDuration = availability.Terms.FirstOrDefault().Duration;
                    }

                    Console.Out.WriteLine();
                    Console.Out.Write($"Enter the To billing cycle [Example - {availability.Terms.FirstOrDefault().BillingCycle}] : ");
                    var billingCycle = Console.ReadLine();
                    if (string.IsNullOrEmpty(billingCycle))
                    {
                        billingCycle = availability.Terms.FirstOrDefault().BillingCycle;
                    }

                    updatedSubscription = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).Patch(
                        new Subscription()
                        {
                            TermDuration = changeToTermDuration,
                            BillingCycle = (BillingCycleType)Enum.Parse(typeof(BillingCycleType), billingCycle)
                        });

                    Console.Out.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Out.WriteLine($"Term duration and billing cycle have successfully been updated on subscription {updatedSubscription.Id}. Changes may take a little bit to show up.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Out.WriteLine();
                }

                return;
            }

            Console.Out.WriteLine();
            Console.WriteLine($"Suspend subscription [{selectedSubscription.Id}] (y/n) ? : ");

            if (Console.ReadKey().KeyChar == 'y')
            {
                var partnerOpsWithNewRequestId = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid(), Guid.NewGuid()));
                updatedSubscription = partnerOpsWithNewRequestId.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).Patch(
                    new Subscription()
                    {
                        Status = SubscriptionStatus.Suspended
                    });

                updatedSubscription =
                    partnerOperations.Customers.ById(selectedCustomerId)
                        .Subscriptions.ById(selectedSubscription.Id)
                        .Get();
                Console.Out.WriteLine("========================== Updated Subscription ==========================");
                Console.Out.WriteLine($"Uri:           {updatedSubscription.Links.Self}");
                Console.Out.WriteLine($"Quantity:      {updatedSubscription.Quantity}");
                Console.Out.WriteLine($"PartnerId:     {updatedSubscription.PartnerId}");
                Console.Out.WriteLine($"Term Duration: {updatedSubscription.TermDuration}");
                Console.Out.WriteLine($"Billing Cycle: {updatedSubscription.BillingCycle}");
                Console.Out.WriteLine($"Status:        {updatedSubscription.Status}");
                Console.Out.WriteLine("========================== Updated Subscription ==========================");
            }

            Console.Out.WriteLine();
        }
    }
}
