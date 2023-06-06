// -----------------------------------------------------------------------
// <copyright file="SubscriptionCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Subscriptions;
    using Network;
    using Usage;

    /// <summary>
    /// The customer subscriptions implementation.
    /// </summary>
    internal class SubscriptionCollectionOperations : BasePartnerComponent, ISubscriptionCollection
    {
        /// <summary>
        /// A lazy reference to the current customer's subscription usage records operations.
        /// </summary>
        private Lazy<ISubscriptionMonthlyUsageRecordCollection> usageRecords;

        /// <summary>
        /// A lazy reference to the current customer's overage operations.
        /// </summary>
        private Lazy<IOverageCollection> overageCollectionOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id to whom the subscriptions belong.</param>
        public SubscriptionCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set");
            }

            this.usageRecords = new Lazy<ISubscriptionMonthlyUsageRecordCollection>(() => new SubscriptionMonthlyUsageRecordCollectionOperations(this.Partner, this.Context));
            this.overageCollectionOperations = new Lazy<IOverageCollection>(() => new OverageCollectionOperations(this.Partner, customerId));
        }

        /// <summary>
        /// Obtains the subscription usage records behavior for the customer.
        /// </summary>
        /// <returns>The customer subscription usage records.</returns>
        public ISubscriptionMonthlyUsageRecordCollection UsageRecords
        {
            get
            {
                return this.usageRecords.Value;
            }
        }

        /// <summary>
        /// Gets the subscription overage for the customer.
        /// </summary>
        /// <returns>The overage.</returns>
        public IOverageCollection Overage
        {
            get
            {
                return this.overageCollectionOperations.Value;
            }
        }

        /// <summary>
        /// Retrieves a specific customer subscription behavior.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <returns>The customer subscription behavior.</returns>
        public ISubscription this[string subscriptionId]
        {
            get
            {
                return this.ById(subscriptionId);
            }
        }

        /// <summary>
        /// Groups customer subscriptions by an order.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <returns>The order subscriptions operations.</returns>
        public IEntireEntityCollectionRetrievalOperations<Subscription, ResourceCollection<Subscription>> ByOrder(string orderId)
        {
            return new OrderSubscriptionCollectionOperations(this.Partner, this.Context, orderId);
        }

        /// <summary>
        /// Groups customer subscriptions by a partner.
        /// </summary>
        /// <param name="partnerId">The partner id.</param>
        /// <returns>The partner subscriptions operations.</returns>
        public IEntityCollectionRetrievalOperations<Subscription, ResourceCollection<Subscription>> ByPartner(string partnerId)
        {
            return new PartnerSubscriptionCollectionOperations(this.Partner, this.Context, partnerId);
        }

        /// <summary>
        /// Retrieves a specific customer subscription behavior.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <returns>The customer subscription.</returns>
        public ISubscription ById(string subscriptionId)
        {
            return new SubscriptionOperations(this.Partner, this.Context, subscriptionId);
        }
        
        /// <summary>
        /// Retrieves all subscriptions.
        /// </summary>
        /// <returns>The subscriptions.</returns>
        public ResourceCollection<Subscription> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Subscription>>(() => this.GetAsync());
        }

        /// <summary>
        /// Retrieves all subscriptions.
        /// </summary>
        /// <returns>The subscriptions.</returns>
        public async Task<ResourceCollection<Subscription>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Subscription, ResourceCollection<Subscription>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerSubscriptions.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
