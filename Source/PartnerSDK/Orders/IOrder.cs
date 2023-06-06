// -----------------------------------------------------------------------
// <copyright file="IOrder.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Orders;

    /// <summary>
    /// Encapsulates a customer order behavior.
    /// </summary>
    public interface IOrder : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<Order>, IEntityPatchOperations<Order>
    {
        /// <summary>
        /// Gets the order provisioning status operations.
        /// </summary>
        IOrderProvisioningStatus ProvisioningStatus { get; }

        /// <summary>
        /// Gets the order attachment collection operations.
        /// </summary>
        IOrderAttachmentCollection Attachments { get; }

        /// <summary>
        /// Gets the order line item collection operations.
        /// </summary>
        IOrderLineItemCollection OrderLineItems { get; }

        /// <summary>
        /// Gets order activation link collection operation.
        /// </summary>
        IOrderActivationLinks OrderActivationLinks { get; }

        /// <summary>
        /// Retrieves the order information.
        /// </summary>
        /// <returns>The order information.</returns>
        new Order Get();

        /// <summary>
        /// Retrieves the order information including pricing details (based on access permissions) when requested..
        /// </summary>
        /// <param name="includePrice">Whether to include pricing details in the order information.</param>
        /// <returns>The order information including pricing details (based on access permissions) when requested.</returns>
        Order Get(bool includePrice);

        /// <summary>
        /// Asynchronously retrieves the order information.
        /// </summary>
        /// <returns>The order information.</returns>
        new Task<Order> GetAsync();

        /// <summary>
        /// Asynchronously retrieves the order information including pricing details (based on access permissions) when requested..
        /// </summary>
        /// <param name="includePrice">Whether to include pricing details in the order information.</param>
        /// <returns>The order information including pricing details (based on access permissions) when requested.</returns>
        Task<Order> GetAsync(bool includePrice);

        /// <summary>
        /// Patches the order.
        /// </summary>
        /// <param name="partialOrder">An order that has the properties to be patched set.</param>
        /// <returns>The updated order.</returns>
        new Order Patch(Order partialOrder);

        /// <summary>
        /// Asynchronously patches the order.
        /// </summary>
        /// <param name="partialOrder">An order that has the properties to be patched set.</param>
        /// <returns>The updated order.</returns>
        new Task<Order> PatchAsync(Order partialOrder);
    }
}