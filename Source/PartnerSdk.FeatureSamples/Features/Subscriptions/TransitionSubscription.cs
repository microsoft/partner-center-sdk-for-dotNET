// <copyright file="TransitionSubscription.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models.PromotionEligibilities;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// Showcases transitioning a subscription.
    /// Steps taken:
    ///     Get subscriptions
    ///     Get transition eligibilities,
    ///     Create transition,
    ///     Get transitions
    /// </summary>
    internal class TransitionSubscription : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title => "Transition Subscription";

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

            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Get();

            Console.Out.WriteLine($"Customer:  {selectedCustomerId}");
            Console.Out.WriteLine("========================== Available Subscriptions ==========================");

            foreach (var sub in customerSubscriptions.Items)
            {
                Console.Out.WriteLine($"Subscription:  {sub.Id}  {sub.OfferId}  {sub.OfferName}");
            }

            Console.Out.WriteLine();
            Console.Out.Write("Enter subscriptionId to transition: ");
            var subscriptionIdForTransition = Console.ReadLine();

            Subscription selectedSubscription = customerSubscriptions.Items.Single(sub => sub.Id == subscriptionIdForTransition);
            Transition targetTransition = null;

            Console.Out.WriteLine($"Checking to see if subscription {selectedSubscription.Id} ({selectedSubscription.FriendlyName}) can be transitioned");

            ResourceCollection<TransitionEligibility> transitionEligibilities = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).TransitionEligibilities.Get();

            if (transitionEligibilities.TotalCount == 0)
            {
                Console.Out.WriteLine("> It cannot be transitioned");
                return;
            }

            var eligibleItems = transitionEligibilities.Items.ToList().FindAll(
            e => e.Eligibilities.FirstOrDefault() != null && e.Eligibilities.FirstOrDefault().IsEligible);

            Console.Out.WriteLine();
            Console.Out.WriteLine("========================== Available Transition Eligibilities ==========================");
            foreach (var eligibility in eligibleItems)
            {
                Console.WriteLine($"{eligibility.CatalogItemId}  {eligibility.Title}");
            }

            var firstEligibleItem = eligibleItems.FirstOrDefault();

            Console.Out.WriteLine();
            Console.Out.Write($"Enter the target CatalogItemId [Example - {firstEligibleItem.CatalogItemId}]: ");
            var catalogItemId = Console.ReadLine();
            if (string.IsNullOrEmpty(catalogItemId))
            {
                Console.Out.WriteLine("CatalogItemId is required for transition.");
                return;
            }

            var selectedEligibility = eligibleItems.Single(item => item.CatalogItemId == catalogItemId);

            targetTransition = new Transition()
            {
                ToCatalogItemId = selectedEligibility.CatalogItemId,
                Quantity = selectedEligibility.Quantity,
                TransitionType = selectedEligibility.Eligibilities.FirstOrDefault(detail => detail.IsEligible == true).TransitionType
            };

            // Supported by only modern office
            var psa = catalogItemId.Split(':');
            if (psa.Length > 1)
            {
                Console.WriteLine("Transition to an existing subscription (y/n) ? ");

                if (Console.ReadKey().KeyChar == 'y')
                {
                    var firstEligibleSubscriptionId = string.Empty;

                    Console.Out.WriteLine("==================== Available existing subscriptions matching target product =====================");
                    foreach (var sub in customerSubscriptions.Items)
                    {
                        if (!string.IsNullOrWhiteSpace(sub.OfferId) && sub.OfferId.Contains(psa[0]) && sub.Status == SubscriptionStatus.Active)
                        {
                            if (string.IsNullOrWhiteSpace(firstEligibleSubscriptionId))
                            {
                                firstEligibleSubscriptionId = sub.Id;
                            }

                            Console.Out.WriteLine($"Existing subscription:  {sub.Id} {sub.FriendlyName}");
                        }
                    }

                    Console.Out.WriteLine();
                    Console.Out.Write($"Enter the target ToSubscriptionId [Example - {firstEligibleSubscriptionId}]: ");
                    var existingSubscriptionId = Console.ReadLine();
                    if (string.IsNullOrEmpty(existingSubscriptionId))
                    {
                        Console.Out.WriteLine("ToSubscriptionId is required for transition to existing subscription.");
                        return;
                    }

                    var selectedExistingSubscription = customerSubscriptions.Items.Single(item => item.Id == existingSubscriptionId);

                    Console.Out.WriteLine();
                    Console.Out.WriteLine($"Selected subscription:  {selectedExistingSubscription.Id}  {selectedExistingSubscription.OfferId}  {selectedExistingSubscription.OfferName} as the target for transition.");
                    targetTransition.ToSubscriptionId = selectedExistingSubscription.Id;
                    targetTransition.TermDuration = selectedExistingSubscription.TermDuration;
                    targetTransition.BillingCycle = selectedExistingSubscription.BillingCycle.ToString();
                }

                // Term and billing cycle not editable if transitioning to existing subscription
                if (string.IsNullOrWhiteSpace(targetTransition.ToSubscriptionId))
                {
                    Console.Out.WriteLine();
                    Console.WriteLine("Change term/billing cycle during transition? (y/n)");

                    if (Console.ReadKey().KeyChar == 'y')
                    {
                        // Get availability
                        var availability = partnerOperations.Customers.ById(selectedCustomerId).Products.ById(psa[0]).Skus.ById(psa[1]).Availabilities.ById(psa[2]).Get();

                        Console.Out.WriteLine();
                        Console.Out.WriteLine("========================== Available Term duration & Billing cycle ==========================");
                        foreach (var term in availability.Terms)
                        {
                            Console.Out.WriteLine($"{term.Duration}  {term.BillingCycle}");
                        }

                        Console.Out.WriteLine();
                        Console.Out.Write($"Enter the To term duration [{availability.Terms.FirstOrDefault().Duration}] : ");
                        var changeToTermDuration = Console.ReadLine();
                        if (string.IsNullOrEmpty(changeToTermDuration))
                        {
                            changeToTermDuration = availability.Terms.FirstOrDefault().Duration;
                        }

                        targetTransition.TermDuration = changeToTermDuration;

                        Console.Out.WriteLine();
                        Console.Out.Write($"Enter the To billing cycle [{availability.Terms.FirstOrDefault().BillingCycle}] : ");
                        var billingCycle = Console.ReadLine();
                        if (string.IsNullOrEmpty(billingCycle))
                        {
                            billingCycle = availability.Terms.FirstOrDefault().BillingCycle;
                        }

                        targetTransition.BillingCycle = billingCycle;
                    }

                    Console.Out.WriteLine();
                    Console.WriteLine("Set promotion during transition? (y/n)");

                    if (Console.ReadKey().KeyChar == 'y')
                    {
                        Console.Out.WriteLine();
                        Console.Out.Write($"Enter the promotion country [US] : ");
                        var country = Console.ReadLine();
                        if (string.IsNullOrEmpty(country))
                        {
                            country = "US";
                        }

                        Console.Out.WriteLine();
                        Console.Out.Write($"Enter the promotion segment [Commercial] : ");
                        var segment = Console.ReadLine();
                        if (string.IsNullOrEmpty(segment))
                        {
                            segment = "Commercial";
                        }

                        // Get available promotions
                        var promotions = partnerOperations.ProductPromotions.ByCountry(country).BySegment(segment).Get();

                        Console.Out.WriteLine();
                        Console.Out.WriteLine("========================== Available promotions ==========================");
                        foreach (var promotion in promotions.Items)
                        {
                            if (promotion.RequiredProducts.Any(product => product.ProductId.Equals(psa[0]) && product.SkuId.Equals(psa[1])))
                            {
                                Console.Out.WriteLine($"{promotion.Name}  {promotion.Id}");
                            }
                        }

                        Console.Out.WriteLine();
                        var firstPromotion = promotions.Items.FirstOrDefault(promo => promo.RequiredProducts.Any(product => product.ProductId.Equals(psa[0]) && product.SkuId.Equals(psa[1])));
                        Console.Out.Write($"Enter the promotion [{firstPromotion.Id}] : ");
                        var selectedPromotionId = Console.ReadLine();
                        if (string.IsNullOrEmpty(selectedPromotionId))
                        {
                            selectedPromotionId = firstPromotion.Id;
                        }

                        targetTransition.PromotionId = selectedPromotionId;

                        Enum.TryParse<Models.PromotionEligibilities.Enums.BillingCycleType>(selectedSubscription.BillingCycle.ToString(), true, out var targetBillingCycle);
                        if (!string.IsNullOrWhiteSpace(targetTransition.BillingCycle))
                        {
                            Enum.TryParse(targetTransition.BillingCycle.ToString(), true, out targetBillingCycle);
                        }

                            // Build the promotion elibities request.
                            var promotionEligibilitiesRequest = new PromotionEligibilitiesRequest()
                        {
                            Items = new List<PromotionEligibilitiesRequestItem>()
                            {
                                new PromotionEligibilitiesRequestItem()
                                {
                                    Id = 0,
                                    CatalogItemId = targetTransition.ToCatalogItemId,
                                    TermDuration = string.IsNullOrWhiteSpace(targetTransition.TermDuration) ? selectedSubscription.TermDuration : targetTransition.TermDuration,
                                    BillingCycle = targetBillingCycle,
                                    Quantity = targetTransition.Quantity,
                                    PromotionId = targetTransition.PromotionId,
                                },
                            },
                        };

                        Console.Out.WriteLine("========================== Post Promotion Eligibilities ==========================");

                        var promotionEligibilities = partnerOperations.Customers.ById(selectedCustomerId).PromotionEligibilities.Post(promotionEligibilitiesRequest);

                        foreach (var eligibility in promotionEligibilities.Items)
                        {
                            Console.Out.WriteLine("Eligibility result for CatalogItemId: {0}", eligibility.CatalogItemId);
                            Console.Out.WriteLine("IsCustomerEligible: {0}", eligibility.Eligibilities.First().IsEligible.ToString());

                            if (!eligibility.Eligibilities.First().IsEligible)
                            {
                                Console.Out.WriteLine("Reasons for ineligibility:");
                                foreach (var error in eligibility.Eligibilities.First().Errors)
                                {
                                    Console.Out.WriteLine("Type: {0}", error.Type);
                                    Console.Out.WriteLine("Description: {0}", error.Description);
                                }
                            }
                        }

                        Console.Out.WriteLine("========================== Post Promotion Eligibilities ==========================");
                    }
                }
            }

            Console.Out.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Out.WriteLine($"Transitioning subscription {subscriptionIdForTransition} for customer {selectedCustomerId} to target CatalogItemId {targetTransition.ToCatalogItemId} with TransferType of {targetTransition.TransitionType}");

            if (!string.IsNullOrWhiteSpace(targetTransition.ToSubscriptionId))
            {
                Console.Out.WriteLine($"Transition is to an existing subscription {targetTransition.ToSubscriptionId} with term {targetTransition.TermDuration} and billing cycle {targetTransition.BillingCycle}");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(targetTransition.TermDuration) || !string.IsNullOrWhiteSpace(targetTransition.BillingCycle))
                {
                    Console.Out.WriteLine($"Transition includes term changed to {targetTransition.TermDuration} and billing cycle changed to {targetTransition.BillingCycle}");
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine();

            Transition transitionResult = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(subscriptionIdForTransition).Transitions.Create(targetTransition);

            // Check for any events with errors
            if (transitionResult.Events.Any())
            {
                foreach (var transitionEvent in transitionResult.Events)
                {
                    if (transitionEvent.Errors != null && transitionEvent.Errors.Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Out.WriteLine($"Transition errors encountered for event {transitionEvent.Name}: ");

                        foreach (var transitionError in transitionEvent.Errors)
                        {
                            Console.Out.WriteLine($"For event {transitionEvent.Name} there was this error {transitionError.Code} - {transitionError.Description}");
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine($"Request to transition subscription completed successfully.");

            Console.Out.WriteLine("========================== Transition Info ==========================");
            Console.Out.WriteLine($"Source Subscription Id: {transitionResult.FromSubscriptionId}");
            Console.Out.WriteLine($"Source Catalog Item Id: {transitionResult.FromCatalogItemId}");
            Console.Out.WriteLine($"Target Subscription Id: {transitionResult.ToSubscriptionId}");
            Console.Out.WriteLine($"Target Catalog Item Id: {transitionResult.ToCatalogItemId}");
            Console.Out.WriteLine($"Target TermDuration: {transitionResult.TermDuration}");
            Console.Out.WriteLine($"Target BillingCycle: {transitionResult.BillingCycle}");
            Console.Out.WriteLine($"Quantity transitioned: {transitionResult.Quantity}");
            Console.Out.WriteLine("========================== Transition Info ==========================");

            Console.ForegroundColor = ConsoleColor.White;

            Console.Out.WriteLine();
            Console.Out.WriteLine($"Getting transition history for subscription {subscriptionIdForTransition}.");

            ResourceCollection<Transition> transitionHistory = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(subscriptionIdForTransition).Transitions.Get();

            if (transitionHistory != null && transitionHistory.Items.Any())
            {
                foreach (var transition in transitionHistory.Items)
                {
                    Console.Out.WriteLine();
                    Console.Out.WriteLine("========================== Transition Info ==========================");
                    Console.Out.WriteLine($"Source Subscription Id: {transition.FromSubscriptionId}");
                    Console.Out.WriteLine($"Source Catalog Item Id: {transition.FromCatalogItemId}");
                    Console.Out.WriteLine($"Target Subscription Id: {transition.ToSubscriptionId}");
                    Console.Out.WriteLine($"Target Catalog Item Id: {transition.ToCatalogItemId}");
                    Console.Out.WriteLine($"Target TermDuration: {transition.TermDuration}");
                    Console.Out.WriteLine($"Target BillingCycle: {transition.BillingCycle}");
                    Console.Out.WriteLine($"Quantity transitioned: {transition.Quantity}");

                    foreach (var transitionEvent in transition.Events)
                    {
                        Console.Out.WriteLine();
                        Console.Out.WriteLine("     ===================== Transition Event ==========================");
                        Console.Out.WriteLine($"     Event Name: {transitionEvent.Name}");
                        Console.Out.WriteLine($"     Event Timestamp: {transitionEvent.Timestamp}");
                        Console.Out.WriteLine($"     Event Status: {transitionEvent.Status}");

                        if (transitionEvent.Errors != null && transitionEvent.Errors.Any())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Out.WriteLine($"     {transitionEvent.Name} Event Errors:");

                            foreach (var transitionError in transitionEvent.Errors)
                            {
                                Console.Out.WriteLine($"     Error {transitionError.Code} - {transitionError.Description}");
                            }
                        }

                        Console.Out.WriteLine("     ===================== Transition Event ==========================");
                    }

                    Console.Out.WriteLine("========================== Transition Info ==========================");
                }
            }
            else
            {
                Console.Out.WriteLine($"No transition history found for subscription {subscriptionIdForTransition}.");
            }
        }
    }
}
