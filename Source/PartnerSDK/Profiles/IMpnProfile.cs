// -----------------------------------------------------------------------
// <copyright file="IMpnProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Profiles
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Partners;

    /// <summary>
    /// Encapsulates behavior of a partner's MPN profile.
    /// </summary>
    public interface IMpnProfile : IPartnerComponent, IEntityGetOperations<MpnProfile>
    {
        /// <summary>
        /// Retrieves the logged in partner's MPN profile.
        /// </summary>
        /// <returns>The partner's MPN profile.</returns>
        new MpnProfile Get();

        /// <summary>
        /// Asynchronously retrieves the logged in partner's MPN profile.
        /// </summary>
        /// <returns>The partner's MPN profile.</returns>
        new Task<MpnProfile> GetAsync();

        /// <summary>
        /// Retrieves a partner's MPN profile by MPN Id.
        /// </summary>
        /// <param name="mpnId">The MPN id.</param>
        /// <returns>The partner's MPN profile.</returns>
        MpnProfile Get(string mpnId);

        /// <summary>
        /// Asynchronously retrieves a partner's MPN profile by MPN Id.
        /// </summary>
        /// <param name="mpnId">The MPN id.</param>
        /// <returns>The partner's MPN profile.</returns>
        Task<MpnProfile> GetAsync(string mpnId);
    }
}
