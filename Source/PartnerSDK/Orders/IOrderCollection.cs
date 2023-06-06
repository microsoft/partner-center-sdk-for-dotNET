// -----------------------------------------------------------------------
// <copyright file="IOrderCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Offers;
    using Models.Orders;

    /// <summary>
    /// Encapsulates customer orders behavior.
    /// </summary>
    public interface IOrderCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<Order, ResourceCollection<Order>>, IEntityCreateOperations<Order>, IEntitySelector<IOrder>
    {
        /// <summary>
        /// Gets a specific order behavior.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <returns>The order operations.</returns>
        new IOrder this[string orderId] { get; }

        /// <summary>
        /// Obtains a specific order behavior.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <returns>The order operations.</returns>
        new IOrder ById(string orderId);

        /// <summary>
        /// Places a new order for the customer.
        /// </summary>
        /// <param name="newOrder">The new order information.</param>
        /// <returns>The order that was just placed.</returns>
        new Order Create(Order newOrder);

        /// <summary>
        /// Asynchronously places a new order for the customer.
        /// </summary>
        /// <param name="newEntity">The new order information.</param>
        /// <returns>The order that was just placed.</returns>
        new Task<Order> CreateAsync(Order newEntity);

        /// <summary>
        /// Places a new order for the customer with a Customer User UPN for license assignment.
        /// </summary>
        /// <param name="newOrder">The new order information.</param>
        /// <param name="customerUserUpn">The customer user UPN for license assignment.</param>
        /// <returns>The order that was just placed.</returns>
        Order Create(Order newOrder, string customerUserUpn);

        /// <summary>
        /// Asynchronously places a new order for the customer with a Customer User UPN for license assignment.
        /// </summary>
        /// <param name="newEntity">The new order information.</param>
        /// <param name="customerUserUpn">The customer user UPN for license assignment.</param>
        /// <returns>The order that was just placed.</returns>
        Task<Order> CreateAsync(Order newEntity, string customerUserUpn);

        /// <summary>
        /// Retrieves all customer orders.
        /// </summary>
        /// <returns>The customer orders.</returns>
        new ResourceCollection<Order> Get();

        /// <summary>
        /// Retrieves all customer orders including pricing details (based on access permissions) when requested..
        /// </summary>
        /// <param name="includePrice">Whether to include pricing details in the order information.</param>
        /// <returns>The customer orders including pricing details (based on access permissions) when requested.</returns>
        ResourceCollection<Order> Get(bool includePrice);

        /// <summary>
        /// Asynchronously retrieves all customer orders.
        /// </summary>
        /// <returns>The customer orders.</returns>
        new Task<ResourceCollection<Order>> GetAsync();

        /// <summary>
        /// Retrieves all customer orders including pricing details (based on access permissions) when requested..
        /// </summary>
        /// <param name="includePrice">Whether to include pricing details in the order information.</param>
        /// <returns>The customer orders including pricing details (based on access permissions) when requested.</returns>
        Task<ResourceCollection<Order>> GetAsync(bool includePrice);

        /// <summary>
        /// Obtains the order collection behavior given a billing cycle type.
        /// </summary>
        /// <param name="billingCycleType">The billing cycle type.</param>
        /// <returns>The order collection by billing cycle type.</returns>
        IOrderCollectionByBillingCycleType ByBillingCycleType(BillingCycleType billingCycleType);
    }
}
