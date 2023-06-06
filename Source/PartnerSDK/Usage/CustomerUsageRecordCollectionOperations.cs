// -----------------------------------------------------------------------
// <copyright file="CustomerUsageRecordCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System.Threading.Tasks;
    using Models;
    using Models.Usage;
    using Network;

    /// <summary>
    /// Implements operations related to a partner's customers usage record.
    /// </summary>
    internal class CustomerUsageRecordCollectionOperations : BasePartnerComponent, ICustomerUsageRecordCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUsageRecordCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public CustomerUsageRecordCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves the collection of customer monthly usage records for a partner.
        /// </summary>
        /// <returns>The collection of customer monthly usage records.</returns>
        public ResourceCollection<CustomerMonthlyUsageRecord> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<CustomerMonthlyUsageRecord>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the collection of customer monthly usage records for a partner.
        /// </summary>
        /// <returns>The collection of customer monthly usage records.</returns>
        public async Task<ResourceCollection<CustomerMonthlyUsageRecord>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<CustomerMonthlyUsageRecord, ResourceCollection<CustomerMonthlyUsageRecord>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetCustomerUsageRecords.Path);

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
