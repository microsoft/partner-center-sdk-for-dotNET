// -----------------------------------------------------------------------
// <copyright file="ProductPromotionCollectionByCountryOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// Product promotion operations by country implementation class.
    /// </summary>
    internal class ProductPromotionCollectionByCountryOperations : BasePartnerComponent, IProductPromotionCollectionByCountry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductPromotionCollectionByCountryOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="country">The country on which to base the product promotions.</param>
        public ProductPromotionCollectionByCountryOperations(IPartner rootPartnerOperations, string country)
            : base(rootPartnerOperations, country)
        {
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <inheritdoc/>
        public IProductPromotion this[string productPromotionId]
        {
            get
            {
                return this.ById(productPromotionId);
            }
        }

        /// <inheritdoc/>
        public IProductPromotion ById(string productPromotionId)
        {
            return new ProductPromotionOperations(this.Partner, productPromotionId, this.Context);
        }

        /// <inheritdoc/>
        public IProductPromotionCollectionByCountryBySegment BySegment(string segment)
        {
            return new ProductPromotionCollectionByCountryBySegmentOperations(this.Partner, segment, this.Context);
        }
    }
}
