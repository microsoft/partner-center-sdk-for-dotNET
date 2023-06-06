// <copyright file="SubscriptionAddons.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using Models.Customers;
    using Models.Subscriptions;

    /// <summary>
    /// Retrieves a single subscription's add-ons.
    /// </summary>
    internal class SubscriptionAddons : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Subscription Addons"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// This method shows how to retrieve subscription's add-ons.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and subscription from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;
            var selectedSubscription = (state[FeatureSamplesApplication.SubscriptionKey] as List<Subscription>)[0];

            var subscriptionDetails = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).AddOns.Get();

            Console.Out.WriteLine("Quantity: {0}", subscriptionDetails.TotalCount);

            foreach (var subscription in subscriptionDetails.Items)
            {
                Console.Out.WriteLine("Addon: {0}", subscription.FriendlyName);
            }

            Console.Out.WriteLine();
        }
    }
}
