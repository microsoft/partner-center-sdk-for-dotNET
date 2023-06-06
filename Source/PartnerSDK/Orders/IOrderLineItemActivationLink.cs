// -----------------------------------------------------------------------
// <copyright file="IOrderLineItemActivationLink.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Orders;

    /// <summary>
    /// Encapsulates customer order line item activation link behavior.
    /// </summary>
    public interface IOrderLineItemActivationLink
    {
        /// <summary>
        /// Retrieves the order line item activation link.
        /// </summary>
        /// <returns>The order line item activation link.</returns>
        ResourceCollection<OrderLineItemActivationLink> Get();

        /// <summary>
        /// Asynchronously retrieves the order line item activation link.
        /// </summary>
        /// <returns>The order line item activation link.</returns>
        Task<ResourceCollection<OrderLineItemActivationLink>> GetAsync();
    }
}
