// -----------------------------------------------------------------------
// <copyright file="ProductPromotionCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    /// <summary>
    /// Product promotion collection operations implementation.
    /// </summary>
    internal class ProductPromotionCollectionOperations : BasePartnerComponent, IProductPromotionCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductPromotionCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public ProductPromotionCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <inheritdoc/>
        public IProductPromotionCollectionByCountry ByCountry(string country)
        {
            return new ProductPromotionCollectionByCountryOperations(this.Partner, country);
        }
    }
}
