// -----------------------------------------------------------------------
// <copyright file="ProductDetailsByReservationScope.cs" company="Microsoft Corporation">
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
    /// Showcases getting a product by Id by Reservation Scope
    /// </summary>
    internal class ProductDetailsByReservationScope : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Product By Id By Reservation Scope"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // retrieve the productId from the application state
            var productId = state[FeatureSamplesApplication.AzurePlanProductId] as string;

            Console.WriteLine("Getting product details for: " + productId);

            var productDetails = partnerOperations.Products.ByCountry("US").ById(productId).ByReservationScope("AzurePlan").Get();

            Console.Out.WriteLine("Title: {0}", productDetails.Title);
            Console.Out.WriteLine("SKUs link: {0}", productDetails.Links.Skus.Uri);
            Console.Out.WriteLine("Description: {0}", productDetails.Description);
            Console.Out.WriteLine();
        }
    }
}
