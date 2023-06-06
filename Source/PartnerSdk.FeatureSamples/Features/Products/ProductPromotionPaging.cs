// <copyright file="ProductPromotionPaging.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Products
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Products;
    using RequestContext;

    /// <summary>
    /// Showcases getting paged product promotions.
    /// </summary>
    internal class ProductPromotionPaging : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Product Promotion Paging"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create("en-US"));
            ResourceCollection<ProductPromotion> productPromotionsSegment = scopedPartnerOperations.ProductPromotions.ByCountry("US").BySegment("commercial").Get();
            var productPromotionsEnumerator = scopedPartnerOperations.Enumerators.ProductPromotions.Create(productPromotionsSegment);

            while (productPromotionsEnumerator.HasValue)
            {
                Console.Out.WriteLine("product promotions page count: " + productPromotionsEnumerator.Current.TotalCount);

                foreach (var productPromotion in productPromotionsEnumerator.Current.Items)
                {
                    Console.Out.WriteLine("Name: {0}", productPromotion.Name);
                    Console.Out.WriteLine();
                }

                productPromotionsEnumerator.Next();
            }
        }
    }
}
