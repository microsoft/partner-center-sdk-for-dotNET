// -----------------------------------------------------------------------
// <copyright file="IProductPromotionCollectionByCountry.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using Microsoft.Store.PartnerCenter.GenericOperations;

    /// <summary>
    /// Holds operations that can be performed on product promotions from a given country.
    /// </summary>
    public interface IProductPromotionCollectionByCountry : IPartnerComponent, IEntitySelector<IProductPromotion>
    {
        /// <summary>
        /// Retrieves the operations tied with a specific product promotion.
        /// </summary>
        /// <param name="productPromotionId">The product promotion id.</param>
        /// <returns>The product promotion operations.</returns>
        new IProductPromotion this[string productPromotionId] { get; }

        /// <summary>
        /// Retrieves the operations tied with a specific product promotion.
        /// </summary>
        /// <param name="productPromotionId">The product promotion id.</param>
        /// <returns>The product promotion operations.</returns>
        new IProductPromotion ById(string productPromotionId);

        /// <summary>
        /// Retrieves the operations that can be applied on product promotions that belong to a given country and segment.
        /// </summary>
        /// <param name="segment">The product promotion segment.</param>
        /// <returns>The product collection operations by country and by segment.</returns>
        IProductPromotionCollectionByCountryBySegment BySegment(string segment);
    }
}