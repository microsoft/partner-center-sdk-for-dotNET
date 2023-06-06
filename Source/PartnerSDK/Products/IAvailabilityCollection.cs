// -----------------------------------------------------------------------
// <copyright file="IAvailabilityCollection.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on availabilities.
    /// </summary>
    public interface IAvailabilityCollection : IPartnerComponent<Tuple<string, string, string>>, IEntireEntityCollectionRetrievalOperations<Availability, ResourceCollection<Availability>>, IEntitySelector<IAvailability>
    {
        /// <summary>
        /// Retrieves the operations tied with a specific availability.
        /// </summary>
        /// <param name="availabilityId">The availability id.</param>
        /// <returns>The availability operations.</returns>
        new IAvailability this[string availabilityId] { get; }

        /// <summary>
        /// Retrieves the operations tied with a specific availability.
        /// </summary>
        /// <param name="availabilityId">The availability id.</param>
        /// <returns>The availability operations.</returns>
        new IAvailability ById(string availabilityId);

        /// <summary>
        /// Retrieves all the availabilities for the provided sku.
        /// </summary>
        /// <returns>The availability for the provided sku.</returns>
        new ResourceCollection<Availability> Get();

        /// <summary>
        /// Asynchronously retrieves all the availabilities for the provided sku.
        /// </summary>
        /// <returns>The availabilities for the provided sku.</returns>
        new Task<ResourceCollection<Availability>> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on availabilities filtered by a specific target segment.
        /// </summary>
        /// <param name="targetSegment">The availability segment filter.</param>
        /// <returns>The availability collection operations by target segment.</returns>
        IAvailabilityCollectionByTargetSegment ByTargetSegment(string targetSegment);

        /// <summary>
        /// Retrieves the operations that can be applied on availabilities filtered by a specific reservation scope.
        /// </summary>
        /// <param name="reservationScope">The availability reservation scope filter.</param>
        /// <returns>The availability collection operations by reservation scope.</returns>
        IAvailabilityCollectionByReservationScopeOperations ByReservationScope(string reservationScope);
    }
}
