// -----------------------------------------------------------------------
// <copyright file="ProductCollectionByCountryOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using Utilities;

    /// <summary>
    /// Product operations by country implementation class.
    /// </summary>
    internal class ProductCollectionByCountryOperations : BasePartnerComponent, IProductCollectionByCountry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCollectionByCountryOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="country">The country on which to base the products.</param>
        public ProductCollectionByCountryOperations(IPartner rootPartnerOperations, string country) :
            base(rootPartnerOperations, country)
        {
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <inheritdoc/>
        public IProduct this[string productId]
        {
            get
            {
                return this.ById(productId);
            }
        }

        /// <inheritdoc/>
        public IProduct ById(string productId)
        {
            return new ProductOperations(this.Partner, productId, this.Context);
        }

        /// <inheritdoc/>
        public IProductCollectionByCountryByTargetView ByTargetView(string targetView)
        {
            return new ProductCollectionByCountryByTargetViewOperations(this.Partner, targetView, this.Context);
        }
    }
}
