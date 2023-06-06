// -----------------------------------------------------------------------
// <copyright file="CartOperations.cs" company="Microsoft Corporation">
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
    /// Cart operations implementation class.
    /// </summary>
    internal class CartOperations : BasePartnerComponent<Tuple<string, string>>, ICart
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id</param>
        /// <param name="cartId">The cart Id</param>
        public CartOperations(IPartner rootPartnerOperations, string customerId, string cartId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, cartId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(cartId))
            {
                throw new ArgumentException("cartId must be set.");
            }
        }

        /// <summary>
        /// Checkouts the cart.
        /// </summary>
        /// <param name="customerUserUpn">The customer user UPN for license assignment.</param>
        /// <returns>The cart checkout result.</returns>
        public CartCheckoutResult Checkout(string customerUserUpn = null)
        {
            return PartnerService.Instance.SynchronousExecute<CartCheckoutResult>(() => this.CheckoutAsync(customerUserUpn: customerUserUpn));
        }

        /// <summary>
        /// Asynchronously Checkouts the cart.
        /// </summary>
        /// <param name="customerUserUpn">The customer user UPN for license assignment.</param>
        /// <returns>The cart checkout result.</returns>
        public async Task<CartCheckoutResult> CheckoutAsync(string customerUserUpn = null)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<string, CartCheckoutResult>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.PlaceOrder.Path, this.Context.Item1, this.Context.Item2));

            if (customerUserUpn != null)
            {
                partnerApiServiceProxy.CustomerUserUpn = customerUserUpn;
            }

            return await partnerApiServiceProxy.PostAsync("success");
        }

        /// <summary>
        /// Updates an existing cart.
        /// </summary>
        /// <param name="cart">The cart to update.</param>
        /// <returns>The updated cart object.</returns>
        public Cart Put(Cart cart)
        {
            return PartnerService.Instance.SynchronousExecute<Cart>(() => this.PutAsync(cart));
        }

        /// <summary>
        /// Asynchronously update an existing cart
        /// </summary>
        /// <param name="cart">The cart to update.</param>
        /// <returns>The updated cart object.</returns>
        public async Task<Cart> PutAsync(Cart cart)
        {
            ParameterValidator.Required(cart, "Cart can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<Cart, Cart>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpdateCart.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.PutAsync(cart);
        }

        /// <summary>
        /// Retrieves a customer cart.
        /// </summary>
        /// <returns>Required cart object</returns>
        public Cart Get()
        {
            return PartnerService.Instance.SynchronousExecute<Cart>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves a customer cart.
        /// </summary>
        /// <returns>Required cart object</returns>
        public async Task<Cart> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Cart, Cart>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCart.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
