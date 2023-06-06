// -----------------------------------------------------------------------
// <copyright file="CustomerAvailabilityDetails.cs" company="Microsoft Corporation">
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
    /// Showcases getting customer availability.
    /// </summary>
    internal class CustomerAvailabilityDetails : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Availability For Customer"; }
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

            // retrieve the skus from the application state
            var skus = state[FeatureSamplesApplication.CustomerSkusKey] as List<Sku>;

            // retrieve the availabilites from the application state
            var availabilities = state[FeatureSamplesApplication.AvailabilitiesKey] as List<Availability>;
            
            var availability = partnerOperations.Customers.ById(customerId).Products.ById(products[0].Id).Skus.ById(skus[0].Id).Availabilities.ById(availabilities[0].Id).Get();

            Console.Out.WriteLine("Id: {0}", availability.Id);
            Console.Out.WriteLine("Default currency: {0}", availability.DefaultCurrency?.Code);

            Console.Out.WriteLine("Segment: {0}", availability.Segment);
            Console.Out.WriteLine();
        }
    }
}
