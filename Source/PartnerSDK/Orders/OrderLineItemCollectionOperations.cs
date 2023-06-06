// -----------------------------------------------------------------------
// <copyright file="OrderLineItemCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System;

    /// <summary>
    /// Order line item collection operations implementation class.
    /// </summary>
    internal class OrderLineItemCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IOrderLineItemCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderLineItemCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        /// <param name="orderId">The order Id</param>
        public OrderLineItemCollectionOperations(IPartner rootPartnerOperations, string customerId, string orderId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, orderId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("orderId must be set.");
            }
        }

        /// <summary>
        /// Obtains the order line item operations.
        /// </summary>
        /// <param name="lineItemNumber">The order line item number.</param>
        /// <returns>The order line item operations.</returns>
        public IOrderLineItem this[string lineItemNumber]
        {
            get
            {
                return this.ById(lineItemNumber);
            }
        }

        /// <summary>
        /// Obtains a specific line item operations.
        /// </summary>
        /// <param name="lineItemNumber">The order line item number.</param>
        /// <returns>The order line item operations.</returns>
        public IOrderLineItem ById(string lineItemNumber)
        {
            return new OrderLineItemOperations(this.Partner, this.Context.Item1, this.Context.Item2, lineItemNumber);
        }
    }
}