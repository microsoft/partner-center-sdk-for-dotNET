// -----------------------------------------------------------------------
// <copyright file="OrderLineItemOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System;

    /// <summary>
    /// Order line item operations implementation class.
    /// </summary>
    internal class OrderLineItemOperations : BasePartnerComponent<Tuple<string, string, string>>, IOrderLineItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderLineItemOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="orderId">The order Id.</param>
        /// <param name="lineItemNumber">The line item number</param>
        public OrderLineItemOperations(IPartner rootPartnerOperations, string customerId, string orderId, string lineItemNumber)
            : base(rootPartnerOperations, new Tuple<string, string, string>(customerId, orderId, lineItemNumber))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("orderId must be set.");
            }

            if (string.IsNullOrWhiteSpace(lineItemNumber))
            {
                throw new ArgumentException("lineItemNumber must be set.");
            }
        }

        /// <summary>
        /// Gets an Order line item activation link operations.
        /// </summary>
        public IOrderLineItemActivationLink ActivationLink
        {
            get { return new OrderLineItemActivationLinkOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3); }
        }
    }
}
