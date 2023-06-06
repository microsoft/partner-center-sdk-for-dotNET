// -----------------------------------------------------------------------
// <copyright file="ICustomerProductCollectionByTargetViewByTargetSegment.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on products in a given catalog view and that apply to a given customer, filtered by target segment.
    /// </summary>
    public interface ICustomerProductCollectionByTargetViewByTargetSegment :
        IPartnerComponent<Tuple<string, string, string>>, IEntireEntityCollectionRetrievalOperations<Product, ResourceCollection<Product>>
    {
        /// <summary>
        /// Retrieves all the products in a given catalog view and that apply to a given customer, filtered by target segment.
        /// </summary>
        /// <returns>The products in a given catalog view and that apply to a given customer, filtered by target segment.</returns>
        new ResourceCollection<Product> Get();

        /// <summary>
        /// Asynchronously retrieves all the products in a given catalog view and that apply to a given customer, filtered by target segment.
        /// </summary>
        /// <returns>The products in a given catalog view and that apply to a given customer, filtered by target segment.</returns>
        new Task<ResourceCollection<Product>> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on customer products filtered by a specific reservation scope.
        /// </summary>
        /// <param name="reservationScope">The customer products reservation scope filter.</param>
        /// <returns>The customer products collection operations by reservation scope.</returns>
        ICustomerProductCollectionByTargetViewByTargetSegmentByReservationScope ByReservationScope(string reservationScope);
    }
}
