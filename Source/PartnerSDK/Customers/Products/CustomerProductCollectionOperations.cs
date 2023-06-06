// -----------------------------------------------------------------------
// <copyright file="CustomerProductCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using PartnerCenter.Products;
    using Utilities;

    /// <summary>
    /// Product operations by customer id implementation class.
    /// </summary>
    internal class CustomerProductCollectionOperations : BasePartnerComponent, ICustomerProductCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProductCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the products.</param>
        public CustomerProductCollectionOperations(IPartner rootPartnerOperations, string customerId) :
            base(rootPartnerOperations, customerId)
        {
            ParameterValidator.Required(customerId, "customerId has to be set.");
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
            return new CustomerProductOperations(this.Partner, this.Context, productId);
        }

        /// <inheritdoc/>
        public ICustomerProductCollectionByTargetView ByTargetView(string targetView)
        {
            return new CustomerProductCollectionByTargetViewOperations(this.Partner, this.Context, targetView);
        }
    }
}
