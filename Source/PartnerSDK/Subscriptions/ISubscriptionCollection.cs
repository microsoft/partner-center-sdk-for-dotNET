// -----------------------------------------------------------------------
// <copyright file="ISubscriptionCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Subscriptions;
    using Usage;

    /// <summary>
    /// Represents the behavior of the customer subscriptions as a whole.
    /// </summary>
    public interface ISubscriptionCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<Subscription, ResourceCollection<Subscription>>, IEntitySelector<ISubscription>
    {
        /// <summary>
        /// Obtains the subscription usage records behavior for the customer.
        /// </summary>
        /// <returns>The customer subscription usage records behavior.</returns>
        ISubscriptionMonthlyUsageRecordCollection UsageRecords { get; }

        /// <summary>
        /// Gets the overage operations.
        /// </summary>
        IOverageCollection Overage { get; }

        /// <summary>
        /// Retrieves a specific customer subscription behavior.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <returns>The customer subscription behavior.</returns>
        new ISubscription this[string subscriptionId] { get; }

        /// <summary>
        /// Retrieves a specific customer subscription behavior.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <returns>The customer subscription behavior.</returns>
        new ISubscription ById(string subscriptionId);

        /// <summary>
        /// Retrieves all subscriptions.
        /// </summary>
        /// <returns>The subscriptions.</returns>
        new ResourceCollection<Subscription> Get();

        /// <summary>
        /// Asynchronously retrieves all subscriptions.
        /// </summary>
        /// <returns>The subscriptions.</returns>
        new Task<ResourceCollection<Subscription>> GetAsync();

        /// <summary>
        /// Groups customer subscriptions by an order.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <returns>The order subscriptions operations.</returns>
        IEntireEntityCollectionRetrievalOperations<Subscription, ResourceCollection<Subscription>> ByOrder(string orderId);
        
        /// <summary>
        /// Groups customer subscriptions by a partner.
        /// </summary>
        /// <param name="partnerId">The partner id.</param>
        /// <returns>The partner subscriptions operations.</returns>
        IEntityCollectionRetrievalOperations<Subscription, ResourceCollection<Subscription>> ByPartner(string partnerId);
    }
}
