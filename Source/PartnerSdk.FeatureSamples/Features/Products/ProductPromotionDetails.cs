// -----------------------------------------------------------------------
// <copyright file="ProductPromotionDetails.cs" company="Microsoft Corporation">
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
    /// Showcases getting a product promotion by Id.
    /// </summary>
    internal class ProductPromotionDetails : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Product Promotion By Id"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // retrieve the product promotions from the application state
            var productPromotions = state[FeatureSamplesApplication.ProductPromotionsKey] as List<ProductPromotion>;

            Console.WriteLine("Getting product promotion details for: " + productPromotions[0].Id);

            var productPromotionDetails = partnerOperations.ProductPromotions.ByCountry("US").ById(productPromotions[0].Id).Get();

            Console.Out.WriteLine("Name: {0}", productPromotionDetails.Name);
            Console.Out.WriteLine("Description: {0}", productPromotionDetails.Description);
            Console.Out.WriteLine();
        }
    }
}
