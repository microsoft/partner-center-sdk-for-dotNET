// <copyright file="GetSubscription.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using Models.Subscriptions;
    using Newtonsoft.Json;

    /// <summary>
    /// Retrieves a single subscription.
    /// </summary>
    internal class GetSubscription : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Subscription"; }
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
            var selectedSubscription = (state[FeatureSamplesApplication.SubscriptionKey] as List<Subscription>)[0];

            var subscriptionDetails = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).Get();

            Console.Out.WriteLine("Uri: {0}", subscriptionDetails.Links.Self);
            Console.Out.WriteLine("Quantity: {0}", subscriptionDetails.Quantity);
            Console.Out.WriteLine("OfferName: {0}", subscriptionDetails.OfferName);
            Console.Out.WriteLine();
            Console.Out.WriteLine(JsonConvert.SerializeObject(subscriptionDetails));
            Console.Out.WriteLine();
        }
    }
}
