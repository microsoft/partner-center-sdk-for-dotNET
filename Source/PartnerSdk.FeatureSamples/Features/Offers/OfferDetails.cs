// -----------------------------------------------------------------------
// <copyright file="OfferDetails.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Offers
{
    using System;
    using System.Collections.Generic;
    using Models.Offers;
    using RequestContext;

    /// <summary>
    /// Showcases getting an offer by Id.
    /// </summary>
    internal class OfferDetails : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Offer By Id"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // retrieve the offers from the application state
             var offers = state[FeatureSamplesApplication.OffersKey] as List<Offer>;

            Console.WriteLine("Getting offer details for: " + offers[0].Id);

            var offerDetails = partnerOperations.Offers.ByCountry("US").ById(offers[0].Id).Get();

            Console.Out.WriteLine("Name: {0}", offerDetails.Name);
            Console.Out.WriteLine("Category: {0}", offerDetails.Category.Id);
            Console.Out.WriteLine();

            var scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create("en-US"));

            // get and display the offer add ons
            var addOns = partnerOperations.Offers.ByCountry("US").ById(offers[0].Id).AddOns.Get();

            Console.WriteLine("Offer add ons: {0}", addOns.TotalCount);

            foreach (var addOn in addOns.Items)
            {
                Console.Out.WriteLine("Name: {0}", addOn.Name);
                Console.Out.WriteLine("Category: {0}", addOn.Category.Id);
                Console.Out.WriteLine();
            }
        }
    }
}
