// -----------------------------------------------------------------------
// <copyright file="IProductByReservationScope.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Products;

    /// <summary>
    /// Holds operations that can be performed on a single product filtered by reservation scope
    /// </summary>
    public interface IProductByReservationScope : IPartnerComponent<Tuple<string, string, string>>, IEntityGetOperations<Product>
    {
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
    }
}
