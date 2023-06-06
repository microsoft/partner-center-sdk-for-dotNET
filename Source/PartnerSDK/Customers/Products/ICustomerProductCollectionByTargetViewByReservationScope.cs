// -----------------------------------------------------------------------
// <copyright file="ICustomerProductCollectionByTargetViewByReservationScope.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on products in a given product collection and that apply to a given customer, filtered by reservation scope.
    /// </summary>
    public interface ICustomerProductCollectionByTargetViewByReservationScope :
        IPartnerComponent<Tuple<string, string, string>>, IEntireEntityCollectionRetrievalOperations<Product, ResourceCollection<Product>>
    {
        /// <summary>
        /// Retrieves all the products in a given product collection and that apply to a given customer, filtered by reservation scope.
        /// </summary>
        /// <returns>The products in a given product collection and that apply to a given customer, filtered by reservation scope.</returns>
        new ResourceCollection<Product> Get();

        /// <summary>
        /// Asynchronously retrieves all the products in a given product collection and that apply to a given customer, filtered by reservation scope.
        /// </summary>
        /// <returns>The products in a given product collection and that apply to a given customer, filtered by reservation scope.</returns>
        new Task<ResourceCollection<Product>> GetAsync();
    }
}
