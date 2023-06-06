// -----------------------------------------------------------------------
// <copyright file="UsageRecordByMeterCollectionOperations.cs" company="Microsoft Corporation">
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
    /// Implements operations related to a single subscription's meter usage records.
    /// </summary>
    internal class UsageRecordByMeterCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IUsageRecordByMeterCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsageRecordByMeterCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription id.</param>
        public UsageRecordByMeterCollectionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
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
        /// Retrieves the subscription's meter usage records.
        /// </summary>
        /// <returns>Collection of subscription's meter usage records.</returns>
        public ResourceCollection<MeterUsageRecord> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<MeterUsageRecord>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the subscription's meter usage records.
        /// </summary>
        /// <returns>Collection of subscription's meter usage records.</returns>
        public async Task<ResourceCollection<MeterUsageRecord>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<MeterUsageRecord, ResourceCollection<MeterUsageRecord>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionUsageRecordsByMeter.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
