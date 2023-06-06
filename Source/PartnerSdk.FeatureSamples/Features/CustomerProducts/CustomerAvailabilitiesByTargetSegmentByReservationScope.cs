// -----------------------------------------------------------------------
// <copyright file="CustomerAvailabilitiesByTargetSegmentByReservationScope.cs" company="Microsoft Corporation">
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
    /// Showcases getting customer availabilities by target segment by reservation scope.
    /// </summary>
    internal class CustomerAvailabilitiesByTargetSegmentByReservationScope : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Availabilities For Customer By Target Segment By Reservation Scope"; }
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

            // retrieve the productId from the application state
            var productId = state[FeatureSamplesApplication.AzurePlanProductId] as string;

            // retrieve the skuId from the application state
            var skuId = state[FeatureSamplesApplication.AzurePlanSkuId] as string;

            var availabilities = partnerOperations.Customers.ById(customerId).Products.ById(productId).Skus.ById(skuId).Availabilities.ByTargetSegment("commercial").ByReservationScope("AzurePlan").Get();

            // display the availabilities
            Console.Out.WriteLine("availabilities count: " + availabilities.TotalCount);

            foreach (var availability in availabilities.Items)
            {
                Console.Out.WriteLine("Id: {0}", availability.Id);
                Console.Out.WriteLine("Default currency: {0}", availability.DefaultCurrency?.Code);

                Console.Out.WriteLine();
            }
        }
    }
}
