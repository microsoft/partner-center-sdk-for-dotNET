// -----------------------------------------------------------------------
// <copyright file="IOrderLineItem.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    /// <summary>
    /// Encapsulates a customer order line item behavior.
    /// </summary>
    public interface IOrderLineItem
    {
        /// <summary>
        /// Encapsulates a customer order line item activation link.
        /// </summary>
        /// <returns>The activation link.</returns>
        IOrderLineItemActivationLink ActivationLink { get; }
    }
}