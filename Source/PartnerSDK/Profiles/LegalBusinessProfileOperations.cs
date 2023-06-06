// -----------------------------------------------------------------------
// <copyright file="LegalBusinessProfileOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Profiles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Partners;
    using Network;

    /// <summary>
    /// The legal business profile operations implementation.
    /// </summary>
    internal class LegalBusinessProfileOperations : BasePartnerComponent, ILegalBusinessProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LegalBusinessProfileOperations"/> class. 
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public LegalBusinessProfileOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves the legal business profile.
        /// </summary>
        /// <param name="vettingVersion">(Optional) The vetting version. The default value is set to Current.</param>
        /// <returns>The legal business profile.</returns>
        public LegalBusinessProfile Get(VettingVersion vettingVersion = VettingVersion.Current)
        {
            return PartnerService.Instance.SynchronousExecute<LegalBusinessProfile>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the legal business profile.
        /// </summary>
        /// <param name="vettingVersion">(Optional) The vetting version. The default value is set to Current.</param>
        /// <returns>The legal business profile.</returns>
        public async Task<LegalBusinessProfile> GetAsync(VettingVersion vettingVersion = VettingVersion.Current)
        {
            var partnerServiceProxy = new PartnerServiceProxy<LegalBusinessProfile, LegalBusinessProfile>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetLegalBusinessProfile.Path);

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetLegalBusinessProfile.Parameters.VettingVersion, vettingVersion.ToString()));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Updates the Legal Business Profile.
        /// </summary>
        /// <param name="legalBusinessProfile">Payload of the update request.</param>
        /// <returns>Updated Legal Business Profile.</returns>
        public LegalBusinessProfile Update(LegalBusinessProfile legalBusinessProfile)
        {
            return PartnerService.Instance.SynchronousExecute<LegalBusinessProfile>(() => this.UpdateAsync(legalBusinessProfile));
        }

        /// <summary>
        /// Asynchronously updates the Legal Business Profile.
        /// </summary>
        /// <param name="legalBusinessProfile">Payload of the update request.</param>
        /// <returns>Updated Legal Business Profile.</returns>
        public async Task<LegalBusinessProfile> UpdateAsync(LegalBusinessProfile legalBusinessProfile)
        {
            var partnerServiceProxy = new PartnerServiceProxy<LegalBusinessProfile, LegalBusinessProfile>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetLegalBusinessProfile.Path));

            return await partnerServiceProxy.PutAsync(legalBusinessProfile);
        }
    }
}
