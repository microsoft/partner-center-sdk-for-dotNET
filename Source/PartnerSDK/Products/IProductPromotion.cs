// -----------------------------------------------------------------------
// <copyright file="IProductPromotion.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models.Products;

    /// <summary>
    /// Holds operations that can be performed on a single product promotion.
    /// </summary>
    public interface IProductPromotion : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<ProductPromotion>
    {
        /// <summary>
        /// Retrieves the product promotion information.
        /// </summary>
        /// <returns>The product promotion information.</returns>
        new ProductPromotion Get();

        /// <summary>
        /// Asynchronously retrieves the product promotion information.
        /// </summary>
        /// <returns>The product promotion information.</returns>
        new Task<ProductPromotion> GetAsync();
    }
}
