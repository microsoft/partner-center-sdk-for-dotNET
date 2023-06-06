// -----------------------------------------------------------------------
// <copyright file="CustomerSkuDetailsByReservationScope.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerProducts
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Showcases getting a sku by customer id and reservation scope
    /// </summary>
    internal class CustomerSkuDetailsByReservationScope : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Sku For Customer By Reservation Scope"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // retrieve the customer from the application state
            var customerId = state[FeatureSamplesApplication.SelectedProductUpgradeCustomerKey] as string;

            // retrieve the products from the application state
            var productId = state[FeatureSamplesApplication.AzurePlanProductId] as string;

            // retrieve the skus from the application state
            var skuId = state[FeatureSamplesApplication.AzurePlanSkuId] as string;

            var sku = partnerOperations.Customers.ById(customerId).Products.ById(productId).Skus.ById(skuId).ByCustomerReservationScope("AzurePlan").Get();

            Console.Out.WriteLine("Id: {0}", sku.Id);
            Console.Out.WriteLine("Title: {0}", sku.Title);
            Console.Out.WriteLine("Minimum quantity: {0}", sku.MinimumQuantity);
            Console.Out.WriteLine("Maximum quantity: {0}", sku.MaximumQuantity);
            Console.Out.WriteLine();
        }
    }
}
