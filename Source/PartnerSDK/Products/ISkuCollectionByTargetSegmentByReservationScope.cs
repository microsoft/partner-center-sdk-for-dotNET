// -----------------------------------------------------------------------
// <copyright file="ISkuCollectionByTargetSegmentByReservationScope.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on skus from a specific target segment and reservation scope.
    /// </summary>
    public interface ISkuCollectionByTargetSegmentByReservationScope : IPartnerComponent<Tuple<string, string, string, string>>, IEntireEntityCollectionRetrievalOperations<Sku, ResourceCollection<Sku>>
    {
        /// <summary>
        /// Retrieves all the skus for the provided product, target segment, and reservation scope.
        /// </summary>
        /// <returns>The skus for the provided product, target segment, and reservation scope.</returns>
        new ResourceCollection<Sku> Get();

        /// <summary>
        /// Asynchronously retrieves all the skus for the provided product, target segment, and reservation scope.
        /// </summary>
        /// <returns>The skus for the provided product, target segment, and reservation scope.</returns>
        new Task<ResourceCollection<Sku>> GetAsync();
    }
}
