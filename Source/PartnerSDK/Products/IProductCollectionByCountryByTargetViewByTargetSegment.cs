// -----------------------------------------------------------------------
// <copyright file="IProductCollectionByCountryByTargetViewByTargetSegment.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on products that belong to a given country, a catalog view and a specific target segment.
    /// </summary>
    public interface IProductCollectionByCountryByTargetViewByTargetSegment :
        IPartnerComponent<Tuple<string, string, string>>, IEntireEntityCollectionRetrievalOperations<Product, ResourceCollection<Product>>
    {
        /// <summary>
        /// Retrieves all the products in the given country, catalog view and target segment.
        /// </summary>
        /// <returns>The products in the given country, catalog view and target segment.</returns>
        new ResourceCollection<Product> Get();

        /// <summary>
        /// Asynchronously retrieves all the products in the given country, catalog view and target segment.
        /// </summary>
        /// <returns>The products in the given country, catalog view and target segment.</returns>
        new Task<ResourceCollection<Product>> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on products that belong to a given country, catalog view, target segment, and reservation scope.
        /// </summary>
        /// <param name="reservationScope">The reservation scope filter.</param>
        /// <returns>The product collection operations by country, by target view, target segment, and by reservation scope.</returns>
        IProductCollectionByCountryByTargetViewByTargetSegmentByReservationScope ByReservationScope(string reservationScope);
    }
}
