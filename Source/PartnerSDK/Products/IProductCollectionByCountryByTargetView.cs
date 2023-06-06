// -----------------------------------------------------------------------
// <copyright file="IProductCollectionByCountryByTargetView.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on products that belong to a given country and a catalog view.
    /// </summary>
    public interface IProductCollectionByCountryByTargetView :
        IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<Product, ResourceCollection<Product>>
    {
        /// <summary>
        /// Retrieves all the products in the given country and catalog view.
        /// </summary>
        /// <returns>The products in the given country and catalog view.</returns>
        new ResourceCollection<Product> Get();

        /// <summary>
        /// Asynchronously retrieves all the products in the given country and catalog view.
        /// </summary>
        /// <returns>The products in the given country and catalog view.</returns>
        new Task<ResourceCollection<Product>> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on products that belong to a given country, catalog view and target segment.
        /// </summary>
        /// <param name="targetSegment">The target segment filter.</param>
        /// <returns>The product collection operations by country, by target view and by target segment.</returns>
        IProductCollectionByCountryByTargetViewByTargetSegment ByTargetSegment(string targetSegment);

        /// <summary>
        /// Retrieves the operations that can be applied on products that belong to a given country, catalog view and reservation scope.
        /// </summary>
        /// <param name="reservationScope">The reservation scope filter.</param>
        /// <returns>The product collection operations by country, by target view and by reservation scope.</returns>
        IProductCollectionByCountryByTargetViewByReservationScope ByReservationScope(string reservationScope);
    }
}
