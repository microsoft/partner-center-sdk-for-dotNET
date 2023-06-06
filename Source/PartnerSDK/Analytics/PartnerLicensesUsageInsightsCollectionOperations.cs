// -----------------------------------------------------------------------
// <copyright file="PartnerLicensesUsageInsightsCollectionOperations.cs" company="Microsoft Corporation">
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
    /// Implements the operations on partner licenses usage insights collection.
    /// </summary>
    internal class PartnerLicensesUsageInsightsCollectionOperations : BasePartnerComponent, IPartnerLicensesUsageInsightsCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerLicensesUsageInsightsCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public PartnerLicensesUsageInsightsCollectionOperations(IPartner rootPartnerOperations) : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves the collection of partner's licenses' usage insights.
        /// </summary>
        /// <returns>Collection of licenses usage insights</returns>
        public ResourceCollection<PartnerLicensesUsageInsights> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<PartnerLicensesUsageInsights>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the collection of partner's licenses' usage insights.
        /// </summary>
        /// <returns>Collection of licenses usage insights</returns>
        public async Task<ResourceCollection<PartnerLicensesUsageInsights>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<PartnerLicensesUsageInsights, ResourceCollection<PartnerLicensesUsageInsights>>(
            this.Partner,
            PartnerService.Instance.Configuration.Apis.PartnerLicensesUsageInsights.Path,
            jsonConverter: new ResourceCollectionConverter<PartnerLicensesUsageInsights>());

            return await partnerServiceProxy.GetAsync();
        }
    }
}
