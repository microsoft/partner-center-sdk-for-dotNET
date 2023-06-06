// -----------------------------------------------------------------------
// <copyright file="SkusByTargetSegment.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Products
{
    using System;
    using System.Collections.Generic;
    using Models.Products;

    /// <summary>
    /// Showcases getting skus for a product by target segment.
    /// </summary>
    internal class SkusByTargetSegment : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Skus For Product By Target Segment"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // retrieve the products from the application state
            var products = state[FeatureSamplesApplication.ProductsKey] as List<Product>;

            // get the skus available to this product
            var skus = partnerOperations.Products.ByCountry("US").ById(products[0].Id).Skus.ByTargetSegment("commercial").Get();
            
            // display the skus
            Console.Out.WriteLine("skus count: " + skus.TotalCount);

            foreach (var sku in skus.Items)
            {
                Console.Out.WriteLine("Id: {0}", sku.Id);
                Console.Out.WriteLine("Title: {0}", sku.Title);
                Console.Out.WriteLine("Minimum quantity: {0}", sku.MinimumQuantity);
                Console.Out.WriteLine("Maximum quantity: {0}", sku.MaximumQuantity);
                Console.Out.WriteLine();
            }
        }
    }
}
