// -----------------------------------------------------------------------
// <copyright file="PartnerSubscriptionCollectionOperations.cs" company="Microsoft Corporation">
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
    /// Implements customer subscription operations grouped by a Microsoft partner.
    /// </summary>
    internal class PartnerSubscriptionCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IEntityCollectionRetrievalOperations<Subscription, ResourceCollection<Subscription>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerSubscriptionCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="partnerId">The partner Id.</param>
        public PartnerSubscriptionCollectionOperations(IPartner rootPartnerOperations, string customerId, string partnerId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, partnerId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(partnerId))
            {
                throw new ArgumentException("partnerId must be set.");
            }
        }

        /// <summary>
        /// Gets the subscriptions for the given partner.
        /// </summary>
        /// <returns>The partner subscriptions.</returns>
        public ResourceCollection<Subscription> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Subscription>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the subscriptions for the given partner.
        /// </summary>
        /// <returns>The partner subscriptions.</returns>
        public async Task<ResourceCollection<Subscription>> GetAsync()
        {
            return await this.GetAsync(0, int.MaxValue);
        }

        /// <summary>
        /// Gets a segment of the subscriptions for the given partner.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The desired segment size.</param>
        /// <returns>The required subscriptions segment.</returns>
        public ResourceCollection<Subscription> Get(int offset, int size)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Subscription>>(() => this.GetAsync(offset, size));
        }

        /// <summary>
        /// Asynchronously gets a segment of the subscriptions for the given partner.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The desired segment size.</param>
        /// <returns>The required subscriptions segment.</returns>
        public async Task<ResourceCollection<Subscription>> GetAsync(int offset, int size)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Subscription, ResourceCollection<Subscription>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerSubscriptionsByPartner.Path, this.Context.Item1));

            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetCustomerSubscriptionsByPartner.Parameters.PartnerId,
                this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
