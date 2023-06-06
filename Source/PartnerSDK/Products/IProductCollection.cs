// -----------------------------------------------------------------------
// <copyright file="IProductCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using GenericOperations;

    /// <summary>
    /// Holds operations that can be performed on products.
    /// </summary>
    public interface IProductCollection : IPartnerComponent
    {
        /// <summary>
        /// Retrieves the operations that can be applied on products from a given country.
        /// </summary>
        /// <param name="country">The country name.</param>
        /// <returns>The product collection operations by country.</returns>
        IProductCollectionByCountry ByCountry(string country);
    }
}