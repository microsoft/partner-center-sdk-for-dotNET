// -----------------------------------------------------------------------
// <copyright file="IExtensions.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Extensions
{
    using Products;

    /// <summary>
    /// Holds operations that extend another set of operations.
    /// </summary>
    public interface IExtensions : IPartnerComponent
    {
        /// <summary>
        /// Retrieves the product extension operations.
        /// </summary>
        IProductExtensions Product { get; }
    }
}