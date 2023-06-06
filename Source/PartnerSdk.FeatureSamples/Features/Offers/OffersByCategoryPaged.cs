// <copyright file="OffersByCategoryPaged.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Offers
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Offers;
    using RequestContext;
    
    /// <summary>
    /// Showcases getting paged offers for a category.
    /// </summary>
    internal class OffersByCategoryPaged : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Offers By Category Paged"; }
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

            var scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create("en-US"));
            var offerCollectionOperations = scopedPartnerOperations.Offers.ByCountry("US").ByCategory(selectedOfferCategory.Id);

            int offset = 0;
            const int PageSize = 10;
            
            ResourceCollection<Offer> offersSegment = offerCollectionOperations.Get(offset, PageSize);
            var offersEnumerator = partnerOperations.Enumerators.Offers.Create(offersSegment);

            while (offersEnumerator.HasValue)
            {
                Console.Out.WriteLine("offers page count: " + offersEnumerator.Current.TotalCount);

                foreach (var offer in offersEnumerator.Current.Items)
                {
                    Console.Out.WriteLine("Name: {0}", offer.Name);
                    Console.Out.WriteLine("Category: {0}", offer.Category.Id);
                    Console.Out.WriteLine();
                }

                offersEnumerator.Next();
            }
        }
    }
}
