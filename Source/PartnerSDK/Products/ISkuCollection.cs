// -----------------------------------------------------------------------
// <copyright file="ISkuCollection.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on skus.
    /// </summary>
    public interface ISkuCollection : IPartnerComponent<Tuple<string, string>>, IEntitySelector<ISku>, IEntireEntityCollectionRetrievalOperations<Sku, ResourceCollection<Sku>>
    {
        /// <summary>
        /// Retrieves the operations tied with a specific sku.
        /// </summary>
        /// <param name="skuId">The sku id.</param>
        /// <returns>The sku operations.</returns>
        new ISku this[string skuId] { get; }

        /// <summary>
        /// Retrieves the operations tied with a specific sku.
        /// </summary>
        /// <param name="skuId">The sku id.</param>
        /// <returns>The sku operations.</returns>
        new ISku ById(string skuId);

        /// <summary>
        /// Retrieves all the skus for the provided product.
        /// </summary>
        /// <returns>The skus for the provided product.</returns>
        new ResourceCollection<Sku> Get();

        /// <summary>
        /// Asynchronously retrieves all the skus for the provided product.
        /// </summary>
        /// <returns>The skus for the provided product.</returns>
        new Task<ResourceCollection<Sku>> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on skus that belong to a segment.
        /// </summary>
        /// <param name="targetSegment">The sku segment filter.</param>
        /// <returns>The sku collection operations by target segment.</returns>
        ISkuCollectionByTargetSegment ByTargetSegment(string targetSegment);

        /// <summary>
        /// Retrieves the operations that can be applied on sku id's filtered by a specific reservation scope.
        /// </summary>
        /// <param name="reservationScope">The reservation scope filter.</param>
        /// <returns>The individual sku operations sorted by reservation scope.</returns>
        ISkuCollectionByReservationScope ByReservationScope(string reservationScope);
    }
}
