// <copyright file="UpdateSubscriptionNextChargeInstruction.cs" company="Microsoft Corporation">
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
    using Models.Subscriptions;

    /// <summary>
    /// Updates the next charge instruction of a subscription.
    /// </summary>
    internal class UpdateSubscriptionNextChargeInstruction : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Update subscription next charge instruction"; }
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

            // 1. Get subscription
            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Get();

            Console.Out.WriteLine("========================== Available Subscriptions ==========================");
            foreach (var sub in customerSubscriptions.Items)
            {
                Console.Out.WriteLine($"Subscription:  {sub.Id}  {sub.OfferId}  {sub.OfferName}");
            }

            Console.Out.WriteLine();
            Console.Out.Write("Enter subscriptionId for scheduled charge cycle change: ");
            var subscriptionId = Console.ReadLine();

            Subscription selectedSubscription = customerSubscriptions.Items.Single(sub => sub.Id == subscriptionId);
            Console.Out.WriteLine("========================== Selected Subscription ==========================");
            Console.Out.WriteLine("Subscription Id: {0}", selectedSubscription.Id);
            Console.Out.WriteLine("Subscription next charge cycle ProductId: {0}", selectedSubscription.NextChargeInstructions?.Product.ProductId);
            Console.Out.WriteLine("Subscription next charge cycle SkuId: {0}", selectedSubscription.NextChargeInstructions?.Product.SkuId);
            Console.Out.WriteLine("Subscription next charge cycle AvailabilityId: {0}", selectedSubscription.NextChargeInstructions?.Product.AvailabilityId);
            Console.Out.WriteLine("Subscription next charge cycle BillingCycle: {0}", selectedSubscription.NextChargeInstructions?.Product.BillingCycle);
            Console.Out.WriteLine("Subscription next charge cycle TermDuration: {0}", selectedSubscription.NextChargeInstructions?.Product.TermDuration);
            Console.Out.WriteLine("Subscription next charge cycle quantity: {0}", selectedSubscription.NextChargeInstructions?.Quantity);

            var psa = selectedSubscription.OfferId.Split(':');
            var productId = psa[0];
            var skuId = psa[1];
            var availabilityId = psa[2];
            var termDuration = selectedSubscription.TermDuration;
            var quantity = selectedSubscription.Quantity;

            // Get availabilities for the selected subscription
            var availability = partnerOperations.Customers.ById(selectedCustomerId).Products.ById(productId).Skus.ById(skuId).Availabilities.ById(availabilityId).Get();

            Console.Out.WriteLine("========================== Available Billing cycle & terms ==========================");
            foreach (var term in availability.Terms)
            {
                Console.Out.WriteLine($"{term.BillingCycle}  {term.Duration}");
            }

            Console.Out.Write($"Enter the To billing cycle: [{availability.Terms.FirstOrDefault().BillingCycle}] : ");
            var billingCycle = Console.ReadLine();
            if (string.IsNullOrEmpty(billingCycle))
            {
                billingCycle = availability.Terms.FirstOrDefault().BillingCycle;
            }

            var changeToBillingCycle = (BillingCycleType)Enum.Parse(typeof(BillingCycleType), billingCycle);

            // Update subscription next charge instruction change
            selectedSubscription.NextChargeInstructions = new NextChargeInstructions
            {
                Product = new ProductTerm
                {
                    ProductId = productId,
                    SkuId = skuId,
                    AvailabilityId = availabilityId,
                    BillingCycle = changeToBillingCycle,
                    TermDuration = termDuration,
                },
                Quantity = quantity,
            };

            var updatedSubscription = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).Patch(selectedSubscription);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine($"Request to update subscription next charge instructions was completed successfully. Changes may take a little bit to show up.");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
