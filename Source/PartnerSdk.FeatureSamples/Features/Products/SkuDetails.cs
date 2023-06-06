// -----------------------------------------------------------------------
// <copyright file="SkuDetails.cs" company="Microsoft Corporation">
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
    /// Showcases getting sku details.
    /// </summary>
    internal class SkuDetails : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Sku Details"; }
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

            // retrieve the skus from the application state
            var skus = state[FeatureSamplesApplication.SkusKey] as List<Sku>;

            // get the sku details
            var sku = partnerOperations.Products.ByCountry("US").ById(products[0].Id).Skus.ById(skus[0].Id).Get();
            
            Console.Out.WriteLine("Id: {0}", sku.Id);
            Console.Out.WriteLine("Title: {0}", sku.Title);
            Console.Out.WriteLine("Description: {0}", sku.Description);
            Console.Out.WriteLine("Minimum quantity: {0}", sku.MinimumQuantity);
            Console.Out.WriteLine("Maximum quantity: {0}", sku.MaximumQuantity);
            Console.Out.WriteLine("Is trial: {0}", sku.IsTrial);
            Console.Out.WriteLine();
        }
    }
}
