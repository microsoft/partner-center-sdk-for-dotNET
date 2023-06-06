// <copyright file="GetSubscriptionActivationLinks.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// Retrieves activation links of a customer's subscription.
    /// </summary>
    internal class GetSubscriptionActivationLinks : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get subscription activation links"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and subscription from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerForOrderSvc] as string;

            // get the subscriptions for the customer
            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Get();
            
            // find a subscription with activation links
            Subscription selectedSubscription = customerSubscriptions.Items.Where(subscription => (subscription.Links.ActivationLinks != null)).FirstOrDefault();

            if (selectedSubscription == null)
            {
                Console.Out.WriteLine("There are no subscriptions that can be activated");
                Console.Out.WriteLine();
            }
            else
            {
                var subscriptionActivationLinks = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).ActivationLinks.Get();

                Console.Out.WriteLine("Number of activation links: {0}", subscriptionActivationLinks?.TotalCount);

                foreach (var activationLink in subscriptionActivationLinks?.Items)
                {
                    Console.Out.WriteLine("Activation Link: {0}", activationLink.Link.Uri.ToString());
                    Console.Out.WriteLine();
                }
            }
        }
    }
}
