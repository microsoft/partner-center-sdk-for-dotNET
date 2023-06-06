// -----------------------------------------------------------------------
// <copyright file="OrderLineItemActivationLink.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    /// <summary>
    /// Class that represents Order Line Item activation link.
    /// </summary>
    public class OrderLineItemActivationLink
    {
        /// <summary>
        /// Gets or sets the line item number.
        /// </summary>
        /// <value>
        /// The line item number.
        /// </value>
        public int LineItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the activation link.
        /// </summary>
        /// <value>
        /// The activation link.
        /// </value>
        public Link Link { get; set; }
    }
}
