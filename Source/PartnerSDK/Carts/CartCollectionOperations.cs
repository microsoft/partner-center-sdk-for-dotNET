// -----------------------------------------------------------------------
// <copyright file="CartCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Carts
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Carts;
    using Network;
    using Utilities;

    /// <summary>
    /// Cart collection operations implementation class.
    /// </summary>
    internal class CartCollectionOperations : BasePartnerComponent, ICartCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public CartCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Obtains a specific cart behavior.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <returns>The cart operations.</returns>
        public ICart this[string cartId]
        {
            get
            {
                return this.ById(cartId);
            }
        }

        /// <summary>
        /// Obtains a specific cart behavior.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <returns>The cart operations.</returns>
        public ICart ById(string cartId)
        {
            return new CartOperations(this.Partner, this.Context, cartId);
        }

        /// <summary>
        /// Creates a new cart for the customer.
        /// </summary>
        /// <param name="newCart"> A cart item to be created.</param>
        /// <returns>A cart object </returns>
        public Cart Create(Cart newCart)
        {
            return PartnerService.Instance.SynchronousExecute<Cart>(() => this.CreateAsync(newCart));
        }

        /// <summary>
        /// Asynchronously creates a new cart for the customer.
        /// </summary>
        /// <param name="newCart"> A cart item to be created.</param>
        /// <returns>A cart object </returns>
        public async Task<Cart> CreateAsync(Cart newCart)
        {
            ParameterValidator.Required(newCart, "newCart can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<Cart, Cart>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CreateCart.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(newCart);
        }
    }
}
