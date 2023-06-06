// <copyright file="CustomerProducts.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerProducts
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Customers;
    using Models.Products;
    using RequestContext;

    /// <summary>
    /// Showcases getting products by customer id and target view.
    /// </summary>
    internal class CustomerProducts : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Products By Customer Id And Collection Id"; }
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

            // retrieve the default catalog view from the application state
            var targetView = state[FeatureSamplesApplication.ProductTargetViewKey] as string;

            // get the products available to this customer
            ResourceCollection<Product> products = partnerOperations.Customers.ById(customerId).Products.ByTargetView(targetView).Get();

            // display the products
            Console.Out.WriteLine("products count: " + products.TotalCount);

            foreach (var product in products.Items)
            {
                Console.Out.WriteLine("Id: {0}", product.Id);
                Console.Out.WriteLine("Title: {0}", product.Title);
                Console.Out.WriteLine("Product type: {0}", product.ProductType.Id);
                Console.Out.WriteLine();
            }

            // store the customer products into the application state
            state.Add(FeatureSamplesApplication.CustomerProductsKey, new List<Product>(products.Items));
        }
    }
}
