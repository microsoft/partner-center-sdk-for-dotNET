// -----------------------------------------------------------------------
// <copyright file="CustomerLicensesUsageInsightsCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
namespace Microsoft.Store.PartnerCenter.Analytics
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.Analytics;
    using Models.JsonConverters;
    using Network;

    /// <summary>
    /// Implements the operations on customer licenses usage insights collection.
    /// </summary>
    internal class CustomerLicensesUsageInsightsCollectionOperations : BasePartnerComponent, ICustomerLicensesUsageInsightsCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerLicensesUsageInsightsCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id of the customer</param>
        public CustomerLicensesUsageInsightsCollectionOperations(IPartner rootPartnerOperations, string customerId) : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException("customerId");
            }
        }

        /// <summary>
        /// Retrieves the collection of customer's licenses' usage insights.
        /// </summary>
        /// <returns>Collection of customer licenses usage insights</returns>
        public ResourceCollection<CustomerLicensesUsageInsights> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<CustomerLicensesUsageInsights>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the collection of customer's licenses' usage insights.
        /// </summary>
        /// <returns>Collection of customer licenses usage insights</returns>
        public async Task<ResourceCollection<CustomerLicensesUsageInsights>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<CustomerLicensesUsageInsights, ResourceCollection<CustomerLicensesUsageInsights>>(
            this.Partner,
            string.Format(PartnerService.Instance.Configuration.Apis.CusotmerLicensesUsageInsights.Path, this.Context),
            jsonConverter: new ResourceCollectionConverter<CustomerLicensesUsageInsights>());

            return await partnerServiceProxy.GetAsync();
        }
    }
}
