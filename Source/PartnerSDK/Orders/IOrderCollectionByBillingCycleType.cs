// -----------------------------------------------------------------------
// <copyright file="IOrderCollectionByBillingCycleType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Offers;
    using Models.Orders;

    /// <summary>
    /// Holds operations that can be performed on orders given a billing cycle type.
    /// </summary>
    public interface IOrderCollectionByBillingCycleType : 
        IPartnerComponent<Tuple<string, BillingCycleType>>, IEntireEntityCollectionRetrievalOperations<Order, ResourceCollection<Order>>
    {
        /// <summary>
        /// Retrieves all customer orders.
        /// </summary>
        /// <returns>The customer orders.</returns>
        new ResourceCollection<Order> Get();

        /// <summary>
        /// Asynchronously retrieves all customer orders.
        /// </summary>
        /// <returns>The customer orders.</returns>
        new Task<ResourceCollection<Order>> GetAsync();

        /// <summary>
        /// Retrieves all customer orders.
        /// </summary>
        /// <param name="includePrice">Whether to include pricing details in the order information.</param>
        /// <returns>The customer orders including pricing details (based on access permissions) when requested..</returns>
        ResourceCollection<Order> Get(bool includePrice);

        /// <summary>
        /// Asynchronously retrieves all customer orders.
        /// </summary>
        /// <param name="includePrice">Whether to include pricing details in the order information.</param>
        /// <returns>The customer orders including pricing details (based on access permissions) when requested..</returns>
        Task<ResourceCollection<Order>> GetAsync(bool includePrice);
    }
}
