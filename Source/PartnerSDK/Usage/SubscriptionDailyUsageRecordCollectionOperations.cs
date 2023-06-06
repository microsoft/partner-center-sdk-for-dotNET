// -----------------------------------------------------------------------
// <copyright file="SubscriptionDailyUsageRecordCollectionOperations.cs" company="Microsoft Corporation">
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
    /// Implements operations related to a single subscription daily usage records.
    /// </summary>
    internal class SubscriptionDailyUsageRecordCollectionOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionDailyUsageRecordCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionDailyUsageRecordCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription id.</param>
        public SubscriptionDailyUsageRecordCollectionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
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
        /// Retrieves the subscription daily usage records.
        /// </summary>
        /// <returns>Collection of subscription daily usage records.</returns>
        public ResourceCollection<SubscriptionDailyUsageRecord> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<SubscriptionDailyUsageRecord>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the subscription daily usage records.
        /// </summary>
        /// <returns>Collection of subscription daily usage records.</returns>
        public async Task<ResourceCollection<SubscriptionDailyUsageRecord>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<SubscriptionDailyUsageRecord, ResourceCollection<SubscriptionDailyUsageRecord>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionDailyUsageRecords.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
