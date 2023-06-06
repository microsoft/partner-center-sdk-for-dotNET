// -----------------------------------------------------------------------
// <copyright file="Availabilities.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Products;

    /// <summary>
    /// Showcases getting availabilities.
    /// </summary>
    internal class Availabilities : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Availabilities"; }
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

            // get the availabilities
            var availabilities = partnerOperations.Products.ByCountry("US").ById(products[0].Id).Skus.ById(skus[0].Id).Availabilities.Get();

            // display the availabilities
            Console.Out.WriteLine("availabilities count: " + availabilities.TotalCount);

            foreach (var availability in availabilities.Items)
            {
                Console.Out.WriteLine("Id: {0}", availability.Id);
                Console.Out.WriteLine("Default currency: {0}", availability.DefaultCurrency?.Code);

                Console.Out.WriteLine("Segment: {0}", availability.Segment);
                Console.Out.WriteLine("Id of Product: {0}", availability.Product?.Id);
                Console.Out.WriteLine("Id of Sku: {0}", availability.Sku?.Id);
                Console.Out.WriteLine();
            }

            // display the availabilities with IncludedQuantityOptions property
            var productHavingIncludedQuantityOptions = products.FirstOrDefault(p => p.ProductType.SubType.Id == "Databricks")?.Id;
            var skuHavingIncludedQuantityOptions = partnerOperations.Products.ByCountry("US").ById(productHavingIncludedQuantityOptions).Skus.Get().Items.FirstOrDefault();
            availabilities = partnerOperations.Products.ByCountry("US").ById(productHavingIncludedQuantityOptions).Skus.ById(skuHavingIncludedQuantityOptions.Id).Availabilities.Get();

            Console.Out.WriteLine("count of availabilities having IncludedQuantityOptions: " + availabilities.TotalCount);
            foreach (var availability in availabilities.Items)
            {
                Console.Out.WriteLine("Id: {0}", availability.Id);
                Console.Out.WriteLine("Default currency: {0}", availability.DefaultCurrency?.Code);

                Console.Out.WriteLine("Segment: {0}", availability.Segment);
                Console.Out.WriteLine("Id of Product: {0}", availability.Product?.Id);
                Console.Out.WriteLine("Id of Sku: {0}", availability.Sku?.Id);
                Console.Out.WriteLine();

                foreach (var includedQuantity in availability.IncludedQuantityOptions)
                {
                    Console.Out.WriteLine("Units of Included Quantity: {0}", includedQuantity.Units);
                    Console.Out.WriteLine("Description of Included Quantity: {0}", includedQuantity.Description);
                }
            }

            // store the availabilities into the application state
            state.Add(FeatureSamplesApplication.AvailabilitiesKey, new List<Availability>(availabilities.Items));
        }
    }
}
