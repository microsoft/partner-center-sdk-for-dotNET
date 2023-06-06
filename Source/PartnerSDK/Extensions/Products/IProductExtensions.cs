// -----------------------------------------------------------------------
// <copyright file="IProductExtensions.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Extensions.Products
{
    /// <summary>
    /// Holds extension operations for products.
    /// </summary>
    public interface IProductExtensions : IPartnerComponent
    {
        /// <summary>
        /// Retrieves the extension operations that can be applied on products from a given country.
        /// </summary>
        /// <param name="country">The country name.</param>
        /// <returns>The product extensions operations by country.</returns>
        IProductExtensionsByCountry ByCountry(string country);
    }
}
