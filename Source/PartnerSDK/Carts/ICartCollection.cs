// -----------------------------------------------------------------------
// <copyright file="ICartCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Carts
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Carts;

    /// <summary>
    /// Encapsulates a customer cart behavior.
    /// </summary>
    public interface ICartCollection : IPartnerComponent, IEntityCreateOperations<Cart>, IEntitySelector<ICart>
    {
        /// <summary>
        /// Gets a specific cart behavior.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <returns>The cart operations.</returns>
        new ICart this[string cartId] { get; }

        /// <summary>
        /// Obtains a specific cart behavior.
        /// </summary>
        /// <param name="cartId"> The cart id. </param>
        /// <returns>New ICart</returns>
        new ICart ById(string cartId);

        /// <summary>
        /// Creates a new cart for customer
        /// </summary>
        /// <param name="newCart">A cart item to be created.</param>
        /// <returns>The new cart that was just created.</returns>
        new Cart Create(Cart newCart);

        /// <summary>
        /// Asynchronously creates a new cart.
        /// </summary>
        /// <param name="newCart">A cart item to be created.</param>
        /// <returns>The new cart that was just created.</returns>
        new Task<Cart> CreateAsync(Cart newCart);
    }
}
