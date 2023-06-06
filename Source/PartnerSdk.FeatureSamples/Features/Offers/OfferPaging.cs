// <copyright file="OfferPaging.cs" company="Microsoft Corporation">
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
    /// Showcases getting paged offers.
    /// </summary>
    internal class OfferPaging : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Offer Paging"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            int offset = 0;
            const int PageSize = 20;

            var scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create("en-US"));
            ResourceCollection<Offer> offersSegment = scopedPartnerOperations.Offers.ByCountry("US").Get(offset, PageSize);
            var offersEnumerator = scopedPartnerOperations.Enumerators.Offers.Create(offersSegment);

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
