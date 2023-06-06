﻿// -----------------------------------------------------------------------
// <copyright file="IProductCollectionByCountryByCollectionIdByTargetSegment.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on products that belong to a given country, a product collection and a specific target segment.
    /// </summary>
    public interface IProductCollectionByCountryByCollectionIdByTargetSegment :
        IPartnerComponent<Tuple<string, string, string>>, IEntireEntityCollectionRetrievalOperations<Product, ResourceCollection<Product>>
    {
        /// <summary>
        /// Retrieves all the products in the given country, product collection and target segment.
        /// </summary>
        /// <returns>The products in the given country, product collection and target segment.</returns>
        new ResourceCollection<Product> Get();

        /// <summary>
        /// Asynchronously retrieves all the products in the given country, product collection and target segment.
        /// </summary>
        /// <returns>The products in the given country, product collection and target segment.</returns>
        new Task<ResourceCollection<Product>> GetAsync();
    }
}
