// <copyright file="ProductsByTargetSegment.cs" company="Microsoft Corporation">
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
    /// Showcases products.
    /// </summary>
    internal class ProductsByTargetSegment : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Products by country, target view and target segment"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create("en-US"));

            // retrieve the default catalog view from the application state
            var targetView = state[FeatureSamplesApplication.ProductTargetViewKey] as string;

            // get the products available to this partner
            ResourceCollection<Product> products = partnerOperations.Products.ByCountry("US").ByTargetView(targetView).ByTargetSegment("commercial").Get();

            // display the products
            Console.Out.WriteLine("products count: " + products.TotalCount);

            foreach (var product in products.Items)
            {
                Console.Out.WriteLine("Id: {0}", product.Id);
                Console.Out.WriteLine("Title: {0}", product.Title);
                Console.Out.WriteLine("Product type: {0}", product.ProductType.Id);
                Console.Out.WriteLine();
            }
        }
    }
}
