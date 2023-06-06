// <copyright file="Offers.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Offers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Models.Offers;
    using RequestContext;

    /// <summary>
    /// Showcases offers.
    /// </summary>
    internal class Offers : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "All Offers"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create("en-US"));

            // get the offers available to this partner
            ResourceCollection<Offer> offers = partnerOperations.Offers.ByCountry("US").Get();

            // display the offers
            Console.Out.WriteLine("offers count: " + offers.TotalCount);

            foreach (var offer in offers.Items)
            {
                Console.Out.WriteLine("Id: {0}", offer.Id);
                Console.Out.WriteLine("Name: {0}", offer.Name);
                Console.Out.WriteLine("Category: {0}", offer.Category.Id);

                if (offer.UpgradeTargetOffers != null && offer.UpgradeTargetOffers.Any())
                {
                    Console.Out.WriteLine("Available Upgrades: {0}", offer.UpgradeTargetOffers.Aggregate(string.Empty, (current, transition) => current + (string.IsNullOrWhiteSpace(current) ? string.Empty : ", ") + transition));
                }

                Console.Out.WriteLine();
            }

            // store the offers into the application state
            state.Add(FeatureSamplesApplication.OffersKey, new List<Offer>(offers.Items));
        }
    }
}
