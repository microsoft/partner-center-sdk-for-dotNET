// -----------------------------------------------------------------------
// <copyright file="CustomerLicensesDeploymentInsightsCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Analytics
{
    using System.Threading.Tasks;
    using Models;
    using Models.Analytics;
    using Models.JsonConverters;
    using Network;

    /// <summary>
    /// Implements the operations on customer licenses deployment insights collection.
    /// </summary>
    internal class CustomerLicensesDeploymentInsightsCollectionOperations : BasePartnerComponent, ICustomerLicensesDeploymentInsightsCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerLicensesDeploymentInsightsCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id of the customer</param>
        public CustomerLicensesDeploymentInsightsCollectionOperations(IPartner rootPartnerOperations, string customerId) 
            : base(rootPartnerOperations, customerId)
        {
        }

        /// <summary>
        /// Retrieves the collection of customer's licenses' deployment insights.
        /// </summary>
        /// <returns>Collection of customer licenses deployment insights</returns>
        public ResourceCollection<CustomerLicensesDeploymentInsights> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<CustomerLicensesDeploymentInsights>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the collection of customer's licenses' deployment insights.
        /// </summary>
        /// <returns>Collection of customer licenses deployment insights</returns>
        public async Task<ResourceCollection<CustomerLicensesDeploymentInsights>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<CustomerLicensesDeploymentInsights, ResourceCollection<CustomerLicensesDeploymentInsights>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.CustomerLicensesDeploymentInsights.Path, this.Context),
                jsonConverter: new ResourceCollectionConverter<CustomerLicensesDeploymentInsights>());

            return await partnerServiceProxy.GetAsync();
        }
    }
}
