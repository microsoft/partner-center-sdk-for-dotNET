// <copyright file="ProductPromotions.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Models.Products;
    using RequestContext;

    /// <summary>
    /// Showcases product promotions.
    /// </summary>
    internal class ProductPromotions : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Product promotions by country and segment"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create("en-US"));

            // retrieve the default segment from the application state
            var segment = state[FeatureSamplesApplication.ProductPromotionSegmentKey] as string;

            // get the product promotions available to this partner
            ResourceCollection<ProductPromotion> productPromotions = scopedPartnerOperations.ProductPromotions.ByCountry("US").BySegment(segment).Get();

            // display the product promotions
            Console.Out.WriteLine("product promotions count: " + productPromotions.TotalCount);

            foreach (var productPromotion in productPromotions.Items)
            {
                Console.Out.WriteLine("Id: {0}", productPromotion.Id);
                Console.Out.WriteLine("Title: {0}", productPromotion.Name);
                Console.Out.WriteLine("Product Id: {0}", productPromotion.RequiredProducts.FirstOrDefault().ProductId);
                Console.Out.WriteLine();
            }

            // store the product promotions into the application state
            state.Add(FeatureSamplesApplication.ProductPromotionsKey, new List<ProductPromotion>(productPromotions.Items));
        }
    }
}
