// <copyright file="OfferCategories.cs" company="Microsoft Corporation">
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
    /// Showcases offer categories.
    /// </summary>
    internal class OfferCategories : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Offer Categories"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create("en-US"));

            // get all offer categories
            ResourceCollection<OfferCategory> offerCategoryResults = partnerOperations.With(RequestContextFactory.Instance.Create()).OfferCategories.ByCountry("US").Get();

            Console.Out.WriteLine("Offer Categories count: {0}", offerCategoryResults.TotalCount);

            var offerCategories = new List<OfferCategory>(offerCategoryResults.Items);

            foreach (var offerCategory in offerCategories)
            {
                Console.Out.WriteLine("Name: {0}", offerCategory.Name);
                Console.Out.WriteLine("Country: {0}", offerCategory.Country);
                Console.Out.WriteLine("Locale: {0}", offerCategory.Locale);
                Console.Out.WriteLine();
            }

            // save the offer categories into the application state
            state[FeatureSamplesApplication.OfferCategoriesKey] = offerCategories;
        }
    }
}
