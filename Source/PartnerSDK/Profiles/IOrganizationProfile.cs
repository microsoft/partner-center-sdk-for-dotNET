// -----------------------------------------------------------------------
// <copyright file="IOrganizationProfile.cs" company="Microsoft Corporation">
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
    /// Encapsulates the behavior of an organization profile.
    /// </summary>
    public interface IOrganizationProfile : IPartnerComponent, IEntityGetOperations<OrganizationProfile>, IEntityUpdateOperations<OrganizationProfile>
    {
        /// <summary>
        /// Retrieves the partner's organization profile.
        /// </summary>
        /// <returns>The organization profile.</returns>
        new OrganizationProfile Get();

        /// <summary>
        /// Asynchronously retrieves the partner's organization profile.
        /// </summary>
        /// <returns>The organization profile.</returns>
        new Task<OrganizationProfile> GetAsync();

        /// <summary>
        /// Updates the partner's organization profile.
        /// </summary>
        /// <param name="organizationProfile">The organization profile.</param>
        /// <returns>The updated organization profile.</returns>
        new OrganizationProfile Update(OrganizationProfile organizationProfile);

        /// <summary>
        /// Asynchronously updates the partner's organization profile.
        /// </summary>
        /// <param name="organizationProfile">The organization profile.</param>
        /// <returns>The updated organization profile.</returns>
        new Task<OrganizationProfile> UpdateAsync(OrganizationProfile organizationProfile);
    }
}
