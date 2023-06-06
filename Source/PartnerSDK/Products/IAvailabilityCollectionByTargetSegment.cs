// -----------------------------------------------------------------------
// <copyright file="IAvailabilityCollectionByTargetSegment.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Products;

    /// <summary>
    /// Holds operations that can be performed on availabilities for a specific target segment.
    /// </summary>
    public interface IAvailabilityCollectionByTargetSegment : IPartnerComponent<Tuple<string, string, string, string>>, IEntireEntityCollectionRetrievalOperations<Availability, ResourceCollection<Availability>>
    {
        /// <summary>
        /// Retrieves all the availabilities for the provided sku on a specific target segment.
        /// </summary>
        /// <returns>The availability for the provided sku on a specific target segment.</returns>
        new ResourceCollection<Availability> Get();

        /// <summary>
        /// Asynchronously retrieves all the availabilities for the provided sku on a specific target segment.
        /// </summary>
        /// <returns>The availabilities for the provided sku on a specific target segment.</returns>
        new Task<ResourceCollection<Availability>> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on products that belong to a given target segment, and reservation scope.
        /// </summary>
        /// <param name="reservationScope">The reservation scope filter.</param>
        /// <returns>The availability collection operations by target segment by reservation scope.</returns>
        IAvailabilityCollectionByTargetSegmentByReservationScopeOperations ByReservationScope(string reservationScope);
    }
}