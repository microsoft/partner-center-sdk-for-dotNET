// -----------------------------------------------------------------------
// <copyright file="IProductCollectionByCountry.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using GenericOperations;

    /// <summary>
    /// Holds operations that can be performed on products from a given country.
    /// </summary>
    public interface IProductCollectionByCountry : IPartnerComponent, IEntitySelector<IProduct>
    {
        /// <summary>
        /// Retrieves the operations tied with a specific product.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>The product operations.</returns>
        new IProduct this[string productId] { get; }

        /// <summary>
        /// Retrieves the operations tied with a specific product.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>The product operations.</returns>
        new IProduct ById(string productId);

        /// <summary>
        /// Retrieves the operations that can be applied on products that belong to a given country and catalog view.
        /// </summary>
        /// <param name="targetView">The product target view.</param>
        /// <returns>The product collection operations by country and by target view.</returns>
        IProductCollectionByCountryByTargetView ByTargetView(string targetView);
    }
}