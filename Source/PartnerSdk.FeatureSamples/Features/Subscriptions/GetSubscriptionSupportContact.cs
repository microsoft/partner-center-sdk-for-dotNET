// <copyright file="GetSubscriptionSupportContact.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using Models.Subscriptions;

    /// <summary>
    /// Retrieves the support contact of a customer's subscription.
    /// </summary>
    internal class GetSubscriptionSupportContact : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get subscription support contact"; }
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

            var supportContact = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscription.Id).SupportContact.Get();

            Console.Out.WriteLine("Uri: {0}", supportContact.Links.Self);
            Console.Out.WriteLine("Name: {0}", supportContact.Name);
            Console.Out.WriteLine("Tenant Id: {0}", supportContact.SupportTenantId);
            Console.Out.WriteLine("Mpn Id: {0}", supportContact.SupportMpnId);
        }
    }
}
