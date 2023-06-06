// -----------------------------------------------------------------------
// <copyright file="SupportProfileOperations.cs" company="Microsoft Corporation">
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
    /// The support profile operations implementation.
    /// </summary>
    internal class SupportProfileOperations : BasePartnerComponent, ISupportProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupportProfileOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public SupportProfileOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves the support profile.
        /// </summary>
        /// <returns>The support profile.</returns>
        public SupportProfile Get()
        {
            return PartnerService.Instance.SynchronousExecute<SupportProfile>(() => this.GetAsync());
        }
        
        /// <summary>
        /// Asynchronously Retrieves the support profile.
        /// </summary>
        /// <returns>The support profile.</returns>
        public async Task<SupportProfile> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<SupportProfile, SupportProfile>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetSupportProfile.Path);
            
            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Updates the support profile.
        /// </summary>
        /// <param name="updatePayload">Payload of the update request</param>
        /// <returns>Updated support profile</returns>
        public SupportProfile Update(SupportProfile updatePayload)
        {
            return PartnerService.Instance.SynchronousExecute<SupportProfile>(() => this.UpdateAsync(updatePayload));
        }

        /// <summary>
        /// Asynchronously updates the support Profile.
        /// </summary>
        /// <param name="updatePayload">Payload of the update request</param>
        /// <returns>Updated support profile</returns>
        public async Task<SupportProfile> UpdateAsync(SupportProfile updatePayload)
        {
            var partnerServiceProxy = new PartnerServiceProxy<SupportProfile, SupportProfile>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.UpdateSupportProfile.Path));

            return await partnerServiceProxy.PutAsync(updatePayload);
        }
    }
}
