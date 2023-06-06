// -----------------------------------------------------------------------
// <copyright file="ISkuCollectionByTargetSegment.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on skus from a specific target segment.
    /// </summary>
    public interface ISkuCollectionByTargetSegment : IPartnerComponent<Tuple<string, string, string>>,  IEntireEntityCollectionRetrievalOperations<Sku, ResourceCollection<Sku>>
    {
        /// <summary>
        /// Retrieves all the skus for the provided product and target segment.
        /// </summary>
        /// <returns>The skus for the provided product and target segment.</returns>
        new ResourceCollection<Sku> Get();

        /// <summary>
        /// Asynchronously retrieves all the skus for the provided product and target segment.
        /// </summary>
        /// <returns>The skus for the provided product and target segment.</returns>
        new Task<ResourceCollection<Sku>> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on sku id's filtered by a specific reservation scope.
        /// </summary>
        /// <param name="reservationScope">The reservation scope filter.</param>
        /// <returns>The individual sku operations sorted by reservation scope.</returns>
        ISkuCollectionByTargetSegmentByReservationScope ByReservationScope(string reservationScope);
    }
}
