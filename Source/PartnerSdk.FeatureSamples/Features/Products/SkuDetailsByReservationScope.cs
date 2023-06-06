// -----------------------------------------------------------------------
// <copyright file="SkuDetailsByReservationScope.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Products
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Showcases getting sku details by reservation scope.
    /// </summary>
    internal class SkuDetailsByReservationScope : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Sku Details By Reservation Scope"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // retrieve the products from the application state
            var productId = state[FeatureSamplesApplication.AzurePlanProductId] as string;

            // retrieve the skus from the application state
            var skuId = state[FeatureSamplesApplication.AzurePlanSkuId] as string;

            // get the sku details
            var sku = partnerOperations.Products.ByCountry("US").ById(productId).Skus.ById(skuId).ByReservationScope("AzurePlan").Get();

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
