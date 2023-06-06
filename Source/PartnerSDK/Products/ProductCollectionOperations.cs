// -----------------------------------------------------------------------
// <copyright file="ProductCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using Utilities;

    /// <summary>
    /// Product collection operations implementation.
    /// </summary>
    internal class ProductCollectionOperations : BasePartnerComponent, IProductCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public ProductCollectionOperations(IPartner rootPartnerOperations) :
            base(rootPartnerOperations)
        {
        }

        /// <inheritdoc/>
        public IProductCollectionByCountry ByCountry(string country)
        {
            return new ProductCollectionByCountryOperations(this.Partner, country);
        }
    }
}
