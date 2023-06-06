// -----------------------------------------------------------------------
// <copyright file="ILegalBusinessProfile.cs" company="Microsoft Corporation">
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
    /// Encapsulates behavior of a partner's legal business profile.
    /// </summary>
    public interface ILegalBusinessProfile : IPartnerComponent, IEntityUpdateOperations<LegalBusinessProfile>
    {
        /// <summary>
        /// Retrieves the legal business profile.
        /// </summary>
        /// <param name="vettingVersion">(Optional) The vetting version. The default value is set to Current.</param>
        /// <returns>The legal business profile.</returns>
        LegalBusinessProfile Get(VettingVersion vettingVersion = VettingVersion.Current);

        /// <summary>
        /// Asynchronously retrieves the legal business profile.
        /// </summary>
        /// <param name="vettingVersion">(Optional) The vetting version. The default value is set to Current.</param>
        /// <returns>The legal business profile.</returns>
        Task<LegalBusinessProfile> GetAsync(VettingVersion vettingVersion = VettingVersion.Current);

        /// <summary>
        /// Updates the legal business profile.
        /// </summary>
        /// <param name="legalBusinessProfile">The legal business profile information.</param>
        /// <returns>The updated legal business profile.</returns>
        new LegalBusinessProfile Update(LegalBusinessProfile legalBusinessProfile);

        /// <summary>
        /// Asynchronously updates the legal business profile.
        /// </summary>
        /// <param name="legalBusinessProfile">The legal business profile information.</param>
        /// <returns>The updated legal business profile.</returns>
        new Task<LegalBusinessProfile> UpdateAsync(LegalBusinessProfile legalBusinessProfile);
    }
}
