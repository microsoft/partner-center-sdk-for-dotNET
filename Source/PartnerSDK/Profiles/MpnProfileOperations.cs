// -----------------------------------------------------------------------
// <copyright file="MpnProfileOperations.cs" company="Microsoft Corporation">
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
    using Utilities;

    /// <summary>
    /// Class which contains operations for Microsoft Partner Network Profile
    /// </summary>
    internal class MpnProfileOperations : BasePartnerComponent, IMpnProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MpnProfileOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public MpnProfileOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves the logged in reseller's MpnProfile.
        /// </summary>
        /// <returns>The Mpn profile.</returns>
        public MpnProfile Get()
        {
            return PartnerService.Instance.SynchronousExecute<MpnProfile>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the logged in reseller's MpnProfile.
        /// </summary>
        /// <returns>The Mpn profile.</returns>
        public async Task<MpnProfile> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<MpnProfile, MpnProfile>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetMpnProfile.Path);

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Retrieves a MpnProfile by MPN Id.
        /// </summary>
        /// <param name="mpnId">The MPN Id.</param>
        /// <returns>The Mpn profile.</returns>
        public MpnProfile Get(string mpnId)
        {
            return PartnerService.Instance.SynchronousExecute<MpnProfile>(() => this.GetAsync(mpnId));
        }

        /// <summary>
        /// Asynchronously retrieves a MpnProfile by MPN Id.
        /// </summary>
        /// <param name="mpnId">The MPN id.</param>
        /// <returns>The Mpn profile</returns>
        public async Task<MpnProfile> GetAsync(string mpnId)
        {
            ParameterValidator.Required(mpnId, "The MPN Id is a required parameter.");

            var partnerServiceProxy = new PartnerServiceProxy<MpnProfile, MpnProfile>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetMpnProfile.Path);

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetMpnProfile.Parameters.MpnId, mpnId));

            return await partnerServiceProxy.GetAsync();
        }        
    }
}