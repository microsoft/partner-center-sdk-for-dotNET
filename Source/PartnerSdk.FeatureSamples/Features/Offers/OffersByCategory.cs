// <copyright file="OffersByCategory.cs" company="Microsoft Corporation">
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
    /// Showcases getting offers for a category.
    /// </summary>
    internal class OffersByCategory : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Offers By Category"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the offer categories from the application state
            var offerCategories = state[FeatureSamplesApplication.OfferCategoriesKey] as List<OfferCategory>;

            OfferCategory selectedOfferCategory = offerCategories[0];
            Console.Out.WriteLine("Selected offer category: {0}", selectedOfferCategory.Name);

            var scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create("fr-FR"));

            Console.Out.WriteLine("Getting offers in fr-FR locale");

            var offerCollectionOperations = scopedPartnerOperations.Offers.ByCountry("US").ByCategory(selectedOfferCategory.Id);
            var offers = offerCollectionOperations.Get();

            // display the category offers
            Console.Out.WriteLine("offers count: " + offers.TotalCount);

            foreach (var offer in offers.Items)
            {
                Console.Out.WriteLine("Name: {0}", offer.Name);
                Console.Out.WriteLine("Category: {0}", offer.Category.Id);
                Console.Out.WriteLine();
            }

            Console.Out.WriteLine("Getting offers in default locale");
            offerCollectionOperations = partnerOperations.Offers.ByCountry("US").ByCategory(selectedOfferCategory.Id);
            offers = offerCollectionOperations.Get();

            Console.Out.WriteLine("offers count: " + offers.TotalCount);

            foreach (var offer in offers.Items)
            {
                Console.Out.WriteLine("Name: {0}", offer.Name);
                Console.Out.WriteLine("Category: {0}", offer.Category.Id);
                Console.Out.WriteLine();
            }
        }
    }
}
