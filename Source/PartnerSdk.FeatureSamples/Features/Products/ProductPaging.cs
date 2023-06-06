// <copyright file="ProductPaging.cs" company="Microsoft Corporation">
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
    /// Showcases getting paged products.
    /// </summary>
    internal class ProductPaging : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Product Paging"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create("en-US"));
            ResourceCollection<Product> productsSegment = scopedPartnerOperations.Products.ByCountry("US").ByTargetView("Azure").Get();
            var productsEnumerator = scopedPartnerOperations.Enumerators.Products.Create(productsSegment);

            while (productsEnumerator.HasValue)
            {
                Console.Out.WriteLine("products page count: " + productsEnumerator.Current.TotalCount);

                foreach (var product in productsEnumerator.Current.Items)
                {
                    Console.Out.WriteLine("Title: {0}", product.Title);
                    Console.Out.WriteLine();
                }

                productsEnumerator.Next();
            }
        }
    }
}
