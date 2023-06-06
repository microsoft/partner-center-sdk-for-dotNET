// -----------------------------------------------------------------------
// <copyright file="UsageRecordByResourceCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Usage;
    using Network;

    /// <summary>
    /// Implements operations related to a single subscription's resource usage records.
    /// </summary>
    internal class UsageRecordByResourceCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IUsageRecordByResourceCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsageRecordByResourceCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription id.</param>
        public UsageRecordByResourceCollectionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, subscriptionId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId should be set.");
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("subscriptionId should be set.");
            }
        }

        /// <summary>
        /// Retrieves the subscription's resource usage records.
        /// </summary>
        /// <returns>Collection of subscription's resource usage records.</returns>
        public ResourceCollection<ResourceUsageRecord> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<ResourceUsageRecord>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the subscription's resource usage records.
        /// </summary>
        /// <returns>Collection of subscription's resource usage records.</returns>
        public async Task<ResourceCollection<ResourceUsageRecord>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ResourceUsageRecord, ResourceCollection<ResourceUsageRecord>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionUsageRecordsByResource.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
