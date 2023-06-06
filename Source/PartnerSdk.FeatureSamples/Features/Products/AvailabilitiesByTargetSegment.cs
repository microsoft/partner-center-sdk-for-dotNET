// -----------------------------------------------------------------------
// <copyright file="AvailabilitiesByTargetSegment.cs" company="Microsoft Corporation">
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
    /// Showcases getting availabilities by target segment.
    /// </summary>
    internal class AvailabilitiesByTargetSegment : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Availabilities By Target Segment"; }
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
            var availabilities = partnerOperations.Products.ByCountry("US").ById(products[0].Id).Skus.ById(skus[0].Id).Availabilities.ByTargetSegment("commercial").Get();

            // display the availabilities
            Console.Out.WriteLine("availabilities count: " + availabilities.TotalCount);

            foreach (var availability in availabilities.Items)
            {
                Console.Out.WriteLine("Id: {0}", availability.Id);
                Console.Out.WriteLine("Default currency: {0}", availability.DefaultCurrency?.Code);

                Console.Out.WriteLine("Segment: {0}", availability.Segment);
                Console.Out.WriteLine();
            }
        }
    }
}
