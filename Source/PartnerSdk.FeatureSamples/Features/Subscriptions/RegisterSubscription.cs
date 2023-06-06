// <copyright file="RegisterSubscription.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Register a subscription to enable Azure Reserved instance purchase.
    /// </summary>
    internal class RegisterSubscription : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Register a subscription to enable Azure Reserved instance purchase."; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and subscription from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerForRegistrationStatusDemo] as string;
            var selectedSubscriptionId = state[FeatureSamplesApplication.SubscriptionForRegistrationStatusDemo] as string;

            var subscriptionRegistrationDetails = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscriptionId).Registration.Register();

            Task callTask = Task.Run(() => subscriptionRegistrationDetails);
            callTask.Wait();

            Console.Out.WriteLine("Registration status link: {0}", subscriptionRegistrationDetails);
            Console.Out.WriteLine();
        }
    }
}
