// -----------------------------------------------------------------------
// <copyright file="PartnerLicensesDeploymentInsightsCollectionOperations.cs" company="Microsoft Corporation">
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
    /// Implements the operations on partner licenses deployment insights collection.
    /// </summary>
    internal class PartnerLicensesDeploymentInsightsCollectionOperations : BasePartnerComponent, IPartnerLicensesDeploymentInsightsCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerLicensesDeploymentInsightsCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public PartnerLicensesDeploymentInsightsCollectionOperations(IPartner rootPartnerOperations) : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves the collection of partner's licenses' deployment insights.
        /// </summary>
        /// <returns>Collection of licenses deployment insights</returns>
        public ResourceCollection<PartnerLicensesDeploymentInsights> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<PartnerLicensesDeploymentInsights>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the collection of partner's licenses' deployment insights.
        /// </summary>
        /// <returns>Collection of licenses deployment insights</returns>
        public async Task<ResourceCollection<PartnerLicensesDeploymentInsights>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<PartnerLicensesDeploymentInsights, ResourceCollection<PartnerLicensesDeploymentInsights>>(
            this.Partner,
            PartnerService.Instance.Configuration.Apis.PartnerLicensesDeploymentInsights.Path,
            jsonConverter: new ResourceCollectionConverter<PartnerLicensesDeploymentInsights>());

            return await partnerServiceProxy.GetAsync();
        }
    }
}
