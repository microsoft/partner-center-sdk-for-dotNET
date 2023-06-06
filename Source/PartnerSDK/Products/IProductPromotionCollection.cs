// -----------------------------------------------------------------------
// <copyright file="IProductPromotionCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    /// <summary>
    /// Holds operations that can be performed on product promotions.
    /// </summary>
    public interface IProductPromotionCollection : IPartnerComponent
    {
        /// <summary>
        /// Retrieves the operations that can be applied on product promotions from a given country.
        /// </summary>
        /// <param name="country">The country name.</param>
        /// <returns>The product promotion collection operations by country.</returns>
        IProductPromotionCollectionByCountry ByCountry(string country);
    }
}