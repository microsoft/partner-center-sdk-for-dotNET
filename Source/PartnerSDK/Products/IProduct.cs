// -----------------------------------------------------------------------
// <copyright file="IProduct.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Microsoft.Store.PartnerCenter.Customers.Products;
    using Models.Products;

    /// <summary>
    /// Holds operations that can be performed on a single product.
    /// </summary>
    public interface IProduct : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<Product>
    {
        /// <summary>
        /// Retrieves the skus for the product.
        /// </summary>
        ISkuCollection Skus { get; }

        /// <summary>
        /// Retrieves the product information.
        /// </summary>
        /// <returns>The product information.</returns>
        new Product Get();

        /// <summary>
        /// Asynchronously retrieves the product information.
        /// </summary>
        /// <returns>The product information.</returns>
        new Task<Product> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on product id's filtered by a specific reservation scope.
        /// </summary>
        /// <param name="reservationScope">The reservation scope filter.</param>
        /// <returns>The individual product operations sorted by reservation scope.</returns>
        IProductByReservationScope ByReservationScope(string reservationScope);

        /// <summary>
        /// Retrieves the operations that can be applied on a customer's product id's filtered by a specific reservation scope.
        /// </summary>
        /// <param name="reservationScope">The reservation scope filter.</param>
        /// <returns>The individual product operations sorted by reservation scope.</returns>
        ICustomerProductByReservationScope ByCustomerReservationScope(string reservationScope);
    }
}
