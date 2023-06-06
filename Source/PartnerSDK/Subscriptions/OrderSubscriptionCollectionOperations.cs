// -----------------------------------------------------------------------
// <copyright file="OrderSubscriptionCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Subscriptions;
    using Network;

    /// <summary>
    /// Implements getting customer subscriptions for a given order.
    /// </summary>
    internal class OrderSubscriptionCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<Subscription, ResourceCollection<Subscription>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderSubscriptionCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="orderId">The order Id.</param>
        public OrderSubscriptionCollectionOperations(IPartner rootPartnerOperations, string customerId, string orderId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, orderId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("orderId must be set.");
            }
        }

        /// <summary>
        /// Gets the subscriptions for the given order.
        /// </summary>
        /// <returns>The order subscriptions.</returns>
        public ResourceCollection<Subscription> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Subscription>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the subscriptions for the given order.
        /// </summary>
        /// <returns>The order subscriptions.</returns>
        public async Task<ResourceCollection<Subscription>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Subscription, ResourceCollection<Subscription>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerSubscriptionsByOrder.Path, this.Context.Item1));

            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetCustomerSubscriptionsByOrder.Parameters.OrderId,
                this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
