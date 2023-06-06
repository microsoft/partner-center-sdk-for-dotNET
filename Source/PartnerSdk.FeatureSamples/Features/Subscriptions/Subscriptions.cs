// <copyright file="Subscriptions.cs" company="Microsoft Corporation">
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
    /// Displays all subscriptions for a certain customer.
    /// </summary>
    internal class Subscriptions : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Subscriptions"; }
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
            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Get();

            // display the subscriptions
            Console.Out.WriteLine("Subscription count: " + customerSubscriptions.TotalCount);

            IList<Subscription> subscriptions = new List<Subscription>(customerSubscriptions.Items);

            foreach (var subscription in subscriptions)
            {
                Console.Out.WriteLine("Uri: {0}", subscription.Links.Self.Uri);
                Console.Out.WriteLine("Quantity: {0}", subscription.Quantity);
                Console.Out.WriteLine("State: {0}", subscription.Status);
                Console.Out.WriteLine("OfferName: {0}", subscription.OfferName);
                Console.Out.WriteLine("TermDuration: {0}", subscription.TermDuration);
                if (string.IsNullOrWhiteSpace(subscription.RenewalTermDuration))
                {
                    Console.Out.WriteLine("RenewalTermDuration: {0}", subscription.RenewalTermDuration);
                }

                Console.Out.WriteLine();
                Console.Out.WriteLine(JsonConvert.SerializeObject(subscription));
                Console.Out.WriteLine();
            }

            // store the subscriptions in the application state
            state[FeatureSamplesApplication.SubscriptionKey] = subscriptions;
        }
    }
}
