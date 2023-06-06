// -----------------------------------------------------------------------
// <copyright file="ProductExtensionsOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Extensions.Products
{
    /// <summary>
    /// Extensions operations implementation.
    /// </summary>
    internal class ProductExtensionsOperations : BasePartnerComponent, IProductExtensions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductExtensionsOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public ProductExtensionsOperations(IPartner rootPartnerOperations) :
            base(rootPartnerOperations)
        {
        }

        /// <inheritdoc/>
        public IProductExtensionsByCountry ByCountry(string country)
        {
            return new ProductExtensionsByCountryOperations(this.Partner, country);
        }
    }
}
