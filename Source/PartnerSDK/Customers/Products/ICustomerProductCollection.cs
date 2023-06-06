// -----------------------------------------------------------------------
// <copyright file="ICustomerProductCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using GenericOperations;
    using PartnerCenter.Products;

    /// <summary>
    /// Holds operations that can be performed on products that apply to a given customer.
    /// </summary>
    public interface ICustomerProductCollection : IPartnerComponent, IEntitySelector<IProduct>
    {
        /// <summary>
        /// Retrieves the operations tied with a specific product for a given customer.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>The product by customer id operations.</returns>
        new IProduct this[string productId] { get; }

        /// <summary>
        /// Retrieves the operations tied with a specific product for a given customer.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>The product by customer id operations.</returns>
        new IProduct ById(string productId);

        /// <summary>
        /// Retrieves the operations that can be applied on products in a given catalog view and that apply to a given customer.
        /// </summary>
        /// <param name="targetView">The product target view.</param>
        /// <returns>The catalog view operations by customer id and by target view.</returns>
        ICustomerProductCollectionByTargetView ByTargetView(string targetView);
    }
}