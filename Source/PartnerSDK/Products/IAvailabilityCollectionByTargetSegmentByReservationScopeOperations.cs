// -----------------------------------------------------------------------
// <copyright file="IAvailabilityCollectionByTargetSegmentByReservationScopeOperations.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on availabilities for a specific reservation scope given a target segment.
    /// </summary>
    public interface IAvailabilityCollectionByTargetSegmentByReservationScopeOperations : IPartnerComponent<Tuple<string, string, string, string, string>>, IEntireEntityCollectionRetrievalOperations<Availability, ResourceCollection<Availability>>
    {
        /// <summary>
        /// Retrieves all the availabilities for the provided sku on a specific reservation scope given a target segment.
        /// </summary>
        /// <returns>The availability for the provided sku on a specific reservation scope given a target segment.</returns>
        new ResourceCollection<Availability> Get();

        /// <summary>
        /// Asynchronously retrieves all the availabilities for the provided sku on a specific reservation scope given a target segment.
        /// </summary>
        /// <returns>The availabilities for the provided sku on a specific reservation scope given a target segment.</returns>
        new Task<ResourceCollection<Availability>> GetAsync();
    }
}