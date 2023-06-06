// -----------------------------------------------------------------------
// <copyright file="IProductPromotionCollectionByCountryBySegment.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Products;

    /// <summary>
    /// Holds operations that can be performed on product promotions that belong to a given country and segment.
    /// </summary>
    public interface IProductPromotionCollectionByCountryBySegment :
        IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<ProductPromotion, ResourceCollection<ProductPromotion>>
    {
        /// <summary>
        /// Retrieves all the product promotions in the given country and segment.
        /// </summary>
        /// <returns>The product promotions in the given country and segment.</returns>
        new ResourceCollection<ProductPromotion> Get();

        /// <summary>
        /// Asynchronously retrieves all the product promotions in the given country and segment.
        /// </summary>
        /// <returns>The product promotions in the given country and segment.</returns>
        new Task<ResourceCollection<ProductPromotion>> GetAsync();
    }
}
