// -----------------------------------------------------------------------
// <copyright file="OrganizationProfileOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Profiles
{
    using System.Threading.Tasks;
    using Models.Partners;
    using Network;

    /// <summary>
    /// The organization profile operations implementation.
    /// </summary>
    internal class OrganizationProfileOperations : BasePartnerComponent, IOrganizationProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationProfileOperations"/> class. 
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public OrganizationProfileOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves the organization profile.
        /// </summary>
        /// <returns>The organization profile.</returns>
        public OrganizationProfile Get()
        {
            return PartnerService.Instance.SynchronousExecute<OrganizationProfile>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the organization profile.
        /// </summary>
        /// <returns>The organization profile.</returns>
        public async Task<OrganizationProfile> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<OrganizationProfile, OrganizationProfile>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetOrganizationProfile.Path);
            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Updates the Organization Profile
        /// </summary>
        /// <param name="updatePayload">Payload of the update request</param>
        /// <returns>Updated Organization Profile</returns>
        public OrganizationProfile Update(OrganizationProfile updatePayload)
        {
            return PartnerService.Instance.SynchronousExecute<OrganizationProfile>(() => this.UpdateAsync(updatePayload));
        }

        /// <summary>
        /// Updates the Organization Profile 
        /// </summary>
        /// <param name="updatePayload">Payload of the update request</param>
        /// <returns>Updated Organization Profile</returns>
        public async Task<OrganizationProfile> UpdateAsync(OrganizationProfile updatePayload)
        {
            var partnerServiceProxy = new PartnerServiceProxy<OrganizationProfile, OrganizationProfile>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetOrganizationProfile.Path));

            return await partnerServiceProxy.PutAsync(updatePayload);
        }
    }
}
