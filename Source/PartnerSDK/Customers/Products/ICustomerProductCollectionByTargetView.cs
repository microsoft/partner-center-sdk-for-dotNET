﻿// -----------------------------------------------------------------------
// <copyright file="ICustomerProductCollectionByTargetView.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Products;

    /// <summary>
    /// Holds operations that can be performed on products in a given catalog view that apply to a given customer.
    /// </summary>
    public interface ICustomerProductCollectionByTargetView :
        IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<Product, ResourceCollection<Product>>
    {
        /// <summary>
        /// Retrieves all the products in a given catalog view that apply to a given customer.
        /// </summary>
        /// <returns>The products in a given catalog view that apply to a given customer.</returns>
        new ResourceCollection<Product> Get();

        /// <summary>
        /// Asynchronously retrieves all the products in a given catalog view that apply to a given customer.
        /// </summary>
        /// <returns>The products in a given catalog view that apply to a given customer.</returns>
        new Task<ResourceCollection<Product>> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on products in a given catalog view and that apply to a given customer, filtered by target segment.
        /// </summary>
        /// <param name="targetSegment">The product segment filter.</param>
        /// <returns>The product collection operations by customer, by target view and by target segment.</returns>
        ICustomerProductCollectionByTargetViewByTargetSegment ByTargetSegment(string targetSegment);

        /// <summary>
        /// Retrieves the operations that can be applied on products in a given catalog view and that apply to a given customer, filtered by reservation scope.
        /// </summary>
        /// <param name="reservationScope">The product segment filter.</param>
        /// <returns>The product collection operations by customer, by target view and by reservation scope.</returns>
        ICustomerProductCollectionByTargetViewByReservationScope ByReservationScope(string reservationScope);
    }
}
