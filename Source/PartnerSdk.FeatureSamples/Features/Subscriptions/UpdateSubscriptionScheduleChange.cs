// <copyright file="UpdateSubscriptionScheduleChange.cs" company="Microsoft Corporation">
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
    /// Updates a subscription.
    /// </summary>
    internal class UpdateSubscriptionScheduleChange : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Update Subscription scheduled change"; }
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
                Console.Out.WriteLine($"Subscription:  {sub.Id}  {sub.FriendlyName}  {sub.OfferName}");
            }

            Console.Out.Write("Enter subscriptionId for scheduled change: ");
            var subscriptionId = Console.ReadLine();

            Subscription selectedSubscription = customerSubscriptions.Items.Single(sub => sub.Id == subscriptionId);
            Console.Out.WriteLine("========================== Selected Subscription ==========================");
            Console.Out.WriteLine("Subscription Id: {0}", selectedSubscription.Id);
            Console.Out.WriteLine("Subscription scheduled next term ProductId: {0}", selectedSubscription.ScheduledNextTermInstructions?.Product.ProductId);
            Console.Out.WriteLine("Subscription scheduled next term SkuId: {0}", selectedSubscription.ScheduledNextTermInstructions?.Product.SkuId);
            Console.Out.WriteLine("Subscription scheduled next term AvailabilityId: {0}", selectedSubscription.ScheduledNextTermInstructions?.Product.AvailabilityId);
            Console.Out.WriteLine("Subscription scheduled next term BillingCycle: {0}", selectedSubscription.ScheduledNextTermInstructions?.Product.BillingCycle);
            Console.Out.WriteLine("Subscription scheduled next term TermDuration: {0}", selectedSubscription.ScheduledNextTermInstructions?.Product.TermDuration);
            Console.Out.WriteLine("Subscription scheduled next term PromotionId: {0}", selectedSubscription.ScheduledNextTermInstructions?.Product.PromotionId);
            Console.Out.WriteLine("Subscription scheduled next term quantity: {0}", selectedSubscription.ScheduledNextTermInstructions?.Quantity);
            Console.Out.WriteLine("Subscription scheduled next term custom term end date: {0}", selectedSubscription.ScheduledNextTermInstructions?.CustomTermEndDate?.ToString("u"));
            
            // 2. Get transition eligibilities
            var transitionEligibilities = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).TransitionEligibilities.Get("Scheduled");

            var eligibleItems = transitionEligibilities.Items.ToList().FindAll(
                e => e.Eligibilities.FirstOrDefault() != null && e.Eligibilities.FirstOrDefault().IsEligible);

            if (eligibleItems == null || !eligibleItems.Any())
            {
                Console.WriteLine("No eligible transitions found for the provided subscription");
                return;
            }

            Console.Out.WriteLine("========================== Available Transition Eligibilities ==========================");
            foreach (var eligibility in eligibleItems)
            {
                Console.WriteLine($"{eligibility.CatalogItemId}  {eligibility.Title}");
            }

            var firstEligibleItem = eligibleItems.FirstOrDefault();

            Console.Out.Write($"Enter the To offer id: [{firstEligibleItem.CatalogItemId}]");
            var catalogItemId = Console.ReadLine();
            if (string.IsNullOrEmpty(catalogItemId))
            {
                catalogItemId = selectedSubscription.OfferId;
            }

            var catalog = catalogItemId.Split(':');
            var changeToProductId = catalog[0];
            var changeToSkuId = catalog[1];
            var changeToAvailabilityId = catalog[2];

            // 3. Get availabilities for the change to catalog
            var availability = partnerOperations.Customers.ById(selectedCustomerId).Products.ById(changeToProductId).Skus.ById(changeToSkuId).Availabilities.ById(changeToAvailabilityId).Get();

            Console.Out.WriteLine("========================== Available Billing cycle & terms ==========================");
            foreach (var term in availability.Terms)
            {
                Console.Out.WriteLine($"{term.BillingCycle}  {term.Duration}");
            }

            Console.Out.Write($"Enter the To billing cycle: [{availability.Terms.FirstOrDefault().BillingCycle}]");
            var billingCycle = Console.ReadLine();
            if (string.IsNullOrEmpty(billingCycle))
            {
                billingCycle = availability.Terms.FirstOrDefault().BillingCycle;
            }

            var changeToBillingCycle = (BillingCycleType)Enum.Parse(typeof(BillingCycleType), billingCycle);

            Console.Out.Write($"Enter the To term duration: [{availability.Terms.FirstOrDefault().Duration}]");
            var changeToTermDuration = Console.ReadLine();
            if (string.IsNullOrEmpty(changeToTermDuration))
            {
                changeToTermDuration = availability.Terms.FirstOrDefault().Duration;
            }

            Console.Out.Write($"Enter the To promotion id: [current promotion id: {selectedSubscription.PromotionId}]");
            var changeToPromotionId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(changeToPromotionId))
            {
                changeToPromotionId = null;
            }

            Console.Out.Write($"Enter the To quantity: [{selectedSubscription.Quantity}]");
            var changeToQuantity = selectedSubscription.Quantity;
            var quantity = Console.ReadLine();
            if (!string.IsNullOrEmpty(quantity))
            {
                changeToQuantity = int.Parse(quantity);
            }

            Console.Out.WriteLine("========================== Available Subscriptions To Align With ==========================");
            foreach (var sub in customerSubscriptions.Items)
            {   
                if (!string.Equals("OnlineServicesNCE", sub.ProductType?.Id) || !string.Equals(SubscriptionStatus.Active, sub.Status) || sub.IsTrial) 
                { 
                    continue; 
                }

                Console.Out.WriteLine($"Subscription:  {sub.Id} {sub.FriendlyName} {sub.CommitmentEndDate:u} {sub.TermDuration}");
            }

            Console.Out.Write($"Enter the To custom term end date: [current term end date: {selectedSubscription.CommitmentEndDate:u}]");
            DateTime? changeToCustomTermEndDate = null;
            var customTermEndDate = Console.ReadLine();
            if (!string.IsNullOrEmpty(customTermEndDate))
            {
                changeToCustomTermEndDate = DateTime.Parse(customTermEndDate);
            }

            // 4. Update subscription schedule change
            selectedSubscription.ScheduledNextTermInstructions = new ScheduledNextTermInstructions
            {
                Product = new ProductTerm
                {
                    ProductId = changeToProductId,
                    SkuId = changeToSkuId,
                    AvailabilityId = changeToAvailabilityId,
                    BillingCycle = changeToBillingCycle,
                    TermDuration = changeToTermDuration,
                    PromotionId = changeToPromotionId,
                },
                Quantity = changeToQuantity,
                CustomTermEndDate = changeToCustomTermEndDate,
            };

            var updatedSubscription = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).Patch(selectedSubscription);

            Console.Out.WriteLine("========================== Updated Subscription ==========================");
            Console.Out.WriteLine("Subscription Id: {0}", selectedSubscription.Id);
            Console.Out.WriteLine("Subscription scheduled next term ProductId: {0}", updatedSubscription.ScheduledNextTermInstructions.Product.ProductId);
            Console.Out.WriteLine("Subscription scheduled next term SkuId: {0}", updatedSubscription.ScheduledNextTermInstructions.Product.SkuId);
            Console.Out.WriteLine("Subscription scheduled next term AvailabilityId: {0}", updatedSubscription.ScheduledNextTermInstructions.Product.AvailabilityId);
            Console.Out.WriteLine("Subscription scheduled next term BillingCycle: {0}", updatedSubscription.ScheduledNextTermInstructions.Product.BillingCycle);
            Console.Out.WriteLine("Subscription scheduled next term TermDuration: {0}", updatedSubscription.ScheduledNextTermInstructions.Product.TermDuration);
            Console.Out.WriteLine("Subscription scheduled next term PromotionId: {0}", updatedSubscription.ScheduledNextTermInstructions?.Product.PromotionId);
            Console.Out.WriteLine("Subscription scheduled next term quantity: {0}", updatedSubscription.ScheduledNextTermInstructions?.Quantity);
            Console.Out.WriteLine("Subscription scheduled next term custom term end date: {0}", updatedSubscription.ScheduledNextTermInstructions?.CustomTermEndDate?.ToString("u"));           
            Console.Out.WriteLine("========================== Updated Subscription ==========================");
        }
    }
}
