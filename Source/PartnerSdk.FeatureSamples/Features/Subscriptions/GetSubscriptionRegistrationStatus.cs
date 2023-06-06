// <copyright file="GetSubscriptionRegistrationStatus.cs" company="Microsoft Corporation">
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
    /// Retrieves subscription registration status details.
    /// </summary>
    internal class GetSubscriptionRegistrationStatus : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Subscription Registration status details"; }
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

            var subscriptionRegistrationDetails = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscriptionId).RegistrationStatus.Get();

            Task callTask = Task.Run(() => subscriptionRegistrationDetails);
            callTask.Wait();

            Console.Out.WriteLine("Subscription Id: {0}", subscriptionRegistrationDetails.SubscriptionId);
            Console.Out.WriteLine("Registration Status: {0}", subscriptionRegistrationDetails.Status);
            Console.Out.WriteLine();
        }
    }
}
