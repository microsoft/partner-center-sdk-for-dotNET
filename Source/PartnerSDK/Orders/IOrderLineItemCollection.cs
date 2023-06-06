// -----------------------------------------------------------------------
// <copyright file="IOrderLineItemCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using GenericOperations;

    /// <summary>
    /// Encapsulates customer order line item behavior.
    /// </summary>
    public interface IOrderLineItemCollection : IEntitySelector<IOrderLineItem>
    {
        /// <summary>
        /// Gets a specific order line item behavior.
        /// </summary>
        /// <param name="lineItemNumber">The line item number.</param>
        /// <returns>The order operations.</returns>
        new IOrderLineItem this[string lineItemNumber] { get; }

        /// <summary>
        /// Obtains a specific order line item behavior.
        /// </summary>
        /// <param name="lineItemNumber">The line item number.</param>
        /// <returns>The order operations.</returns>
        new IOrderLineItem ById(string lineItemNumber);
    }
}