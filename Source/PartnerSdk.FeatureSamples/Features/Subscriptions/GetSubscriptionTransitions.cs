// <copyright file="GetSubscriptionTransitions.cs" company="Microsoft Corporation">
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
    /// Showcases getting previous transitions of a subscription.
    /// Steps taken:
    ///     Get subscriptions
    ///     Get transitions
    /// </summary>
    internal class GetSubscriptionTransitions : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title => "Get Subscription Transitions";

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

            Console.Out.WriteLine("========================== Available Subscriptions ==========================");
            foreach (var sub in customerSubscriptions.Items.Where((sub) => string.Equals("OnlineServicesNCE", sub?.ProductType?.Id)))
            {
                Console.Out.WriteLine($"Subscription:  {sub.Id}  {sub.OfferId}  {sub.OfferName}");
            }

            Console.Out.WriteLine();
            Console.Out.Write("Enter subscriptionId to get transition history: ");
            var subscriptionIdForTransition = Console.ReadLine();

            Subscription selectedSubscription = customerSubscriptions.Items.Single(sub => sub.Id == subscriptionIdForTransition);

            Console.Out.Write("[Optional] Enter the operationId of the transition: ");
            var operationId = Console.ReadLine();

            ResourceCollection<Transition> transitionHistory = null;

            if (string.IsNullOrWhiteSpace(operationId))
            {
                Console.Out.WriteLine($"Getting transition history for subscription {subscriptionIdForTransition}.");
                transitionHistory = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(subscriptionIdForTransition).Transitions.Get();
            } 
            else
            {
                Console.Out.WriteLine($"Getting transition with operationId {operationId} for subscription {subscriptionIdForTransition}.");
                transitionHistory = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(subscriptionIdForTransition).Transitions.Get(operationId);
            }

            if (transitionHistory != null && transitionHistory.Items.Any())
            {
                foreach (var transition in transitionHistory.Items)
                {
                    Console.Out.WriteLine($"Transition OperationId: {transition.OperationId}");
                    Console.Out.WriteLine($"Transition from source CatalogItemId {transition.FromCatalogItemId} to target CatalogItemId {transition.ToCatalogItemId}:");
                    foreach (var transitionEvent in transition.Events)
                    {
                        Console.Out.WriteLine($"Transition event {transitionEvent.Name} on {transitionEvent.Timestamp} {transitionEvent.Status}");

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
                }
            }
            else
            {
                Console.Out.WriteLine($"No transition history found for subscription {subscriptionIdForTransition}.");
            }
        }
    }
}
