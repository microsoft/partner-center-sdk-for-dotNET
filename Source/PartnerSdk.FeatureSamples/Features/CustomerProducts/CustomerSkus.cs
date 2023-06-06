// -----------------------------------------------------------------------
// <copyright file="CustomerSkus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerProducts
{
    using System;
    using System.Collections.Generic;
    using Models.Products;

    /// <summary>
    /// Showcases getting skus for a product by customer id.
    /// </summary>
    internal class CustomerSkus : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Skus For Customer"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // retrieve the customer from the application state
            var customerId = state[FeatureSamplesApplication.CustomerWithProducts] as string;

            // retrieve the products from the application state
            var products = state[FeatureSamplesApplication.CustomerProductsKey] as List<Product>;

            // get the skus available to this product
            var skus = partnerOperations.Customers.ById(customerId).Products.ById(products[0].Id).Skus.Get();
            
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

            // store the skus into the application state
            state.Add(FeatureSamplesApplication.CustomerSkusKey, new List<Sku>(skus.Items));
        }
    }
}
