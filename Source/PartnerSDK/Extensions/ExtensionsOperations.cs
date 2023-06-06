// -----------------------------------------------------------------------
// <copyright file="ExtensionsOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Extensions
{
    using System;
    using Products;

    /// <summary>
    /// Extensions operations implementation.
    /// </summary>
    internal class ExtensionsOperations : BasePartnerComponent, IExtensions
    {
        /// <summary>
        /// The product extensions operations.
        /// </summary>
        private readonly Lazy<IProductExtensions> product;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionsOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public ExtensionsOperations(IPartner rootPartnerOperations) :
            base(rootPartnerOperations)
        {
            this.product = new Lazy<IProductExtensions>(() => new ProductExtensionsOperations(this.Partner));
        }

        /// <inheritdoc/>
        public IProductExtensions Product
        {
            get
            {
                return this.product.Value;
            }
        }
    }
}
