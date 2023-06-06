// -----------------------------------------------------------------------
// <copyright file="SubscriptionMonthlyUsageRecordCollectionOperations.cs" company="Microsoft Corporation">
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
    /// Implements operations related to a single customer's subscription usage records.
    /// </summary>
    internal class SubscriptionMonthlyUsageRecordCollectionOperations : BasePartnerComponent, ISubscriptionMonthlyUsageRecordCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionMonthlyUsageRecordCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public SubscriptionMonthlyUsageRecordCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId should be set.");
            }
        }

        /// <summary>
        /// Retrieves the subscription usage records.
        /// </summary>
        /// <returns>Collection of subscription usage records.</returns>
        public ResourceCollection<SubscriptionMonthlyUsageRecord> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<SubscriptionMonthlyUsageRecord>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the subscription usage records.
        /// </summary>
        /// <returns>Collection of subscription usage records.</returns>
        public async Task<ResourceCollection<SubscriptionMonthlyUsageRecord>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<SubscriptionMonthlyUsageRecord, ResourceCollection<SubscriptionMonthlyUsageRecord>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionUsageRecords.Path, this.Context));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
