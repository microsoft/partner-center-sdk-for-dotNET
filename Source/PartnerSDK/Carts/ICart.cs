// -----------------------------------------------------------------------
// <copyright file="ICart.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Carts
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Carts;

    /// <summary>
    /// Encapsulates a customer cart behavior.
    /// </summary>
    public interface ICart : IPartnerComponent<Tuple<string, string>>, IEntityPutOperations<Cart>, IEntityGetOperations<Cart>
    {
        /// <summary>
        /// Checkouts the cart.
        /// </summary>
        /// <returns>The cart checkout result.</returns>
        /// <param name="customerUserUpn">The customer user UPN for license assignment.</param>
        CartCheckoutResult Checkout(string customerUserUpn = null);

        /// <summary>
        /// Asynchronously Checkouts the cart.
        /// </summary>
        /// <returns>The cart checkout result.</returns>
        /// <param name="customerUserUpn">The customer user UPN for license assignment.</param>
        Task<CartCheckoutResult> CheckoutAsync(string customerUserUpn = null);
    }
}
