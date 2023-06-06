// -----------------------------------------------------------------------
// <copyright file="UpdateCart.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Cart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Carts;

    /// <summary>
    /// Increasing quantity of cart to check update cart 
    /// </summary>
    internal class UpdateCart : IUnitOfWork
    {
        /// <summary>
        /// Gets unit of work title
        /// </summary>
        public string Title
        {
            get
            {
                return "Update Cart";
            }
        }

        /// <summary>
        /// Testing the update cart operation
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations</param>
        /// <param name="state">cartID string</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerForOrderSvc] as string;
            var selectedCart = state[FeatureSamplesApplication.CartsKey] as Cart;
            Console.Out.WriteLine("Id: {0} ", selectedCart.Id);
            Console.Out.WriteLine("Quantity: {0}", selectedCart.LineItems.ToArray()[3].Quantity);
            Console.Out.WriteLine();
            selectedCart.LineItems.ToArray()[3].Quantity++;
            var cart = partnerOperations.Customers.ById(selectedCustomerId).Carts.ById(selectedCart.Id).Put(selectedCart);
            Console.Out.WriteLine("Id: {0} ", cart.Id);
            Console.Out.WriteLine("Quantity: {0}", cart.LineItems.ToArray()[3].Quantity);
            Console.Out.WriteLine();
            state[FeatureSamplesApplication.CartsKey] = cart;
        }
    }
}
