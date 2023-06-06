// -----------------------------------------------------------------------
// <copyright file="IProductCollectionByCountryByCollectionId.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on products that belong to a given country and a product collection.
    /// </summary>
    public interface IProductCollectionByCountryByCollectionId :
        IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<Product, ResourceCollection<Product>>
    {
        /// <summary>
        /// Retrieves all the products in the given country and product collection.
        /// </summary>
        /// <returns>The products in the given country and product collection.</returns>
        new ResourceCollection<Product> Get();

        /// <summary>
        /// Asynchronously retrieves all the products in the given country and product collection.
        /// </summary>
        /// <returns>The products in the given country and product collection.</returns>
        new Task<ResourceCollection<Product>> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on products that belong to a given country, product collection and target segment.
        /// </summary>
        /// <param name="targetSegment">The target segment filter.</param>
        /// <returns>The products collection operations by country, by collection id and by target segment.</returns>
        IProductCollectionByCountryByCollectionIdByTargetSegment ByTargetSegment(string targetSegment);
    }
}
