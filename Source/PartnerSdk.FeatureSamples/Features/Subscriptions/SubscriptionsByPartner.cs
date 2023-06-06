// <copyright file="SubscriptionsByPartner.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Subscriptions;
    using Newtonsoft.Json;

    /// <summary>
    /// Displays subscriptions which belong to a partner.
    /// </summary>
    internal class SubscriptionsByPartner : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Partner Subscriptions"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;

            // get the subscriptions for the customer
            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ByPartner("Some Partner Id").Get();

            // display the subscriptions
            Console.Out.WriteLine("Subscription count: " + customerSubscriptions.TotalCount);

            foreach (var subscription in customerSubscriptions.Items)
            {
                Console.Out.WriteLine("Uri: {0}", subscription.Links.Self);
                Console.Out.WriteLine("Quantity: {0}", subscription.Quantity);
                Console.Out.WriteLine();
                Console.Out.WriteLine(JsonConvert.SerializeObject(subscription));
                Console.Out.WriteLine();
            }
        }
    }
}
