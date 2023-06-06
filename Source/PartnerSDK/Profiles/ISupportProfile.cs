// -----------------------------------------------------------------------
// <copyright file="ISupportProfile.cs" company="Microsoft Corporation">
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
    /// Encapsulates behavior of a support profile.
    /// </summary>
    public interface ISupportProfile : IPartnerComponent, IEntityGetOperations<SupportProfile>, IEntityUpdateOperations<SupportProfile>
    {
        /// <summary>
        /// Retrieves the support profile. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <returns>The support profile.</returns>
        new SupportProfile Get();

        /// <summary>
        /// Asynchronously retrieves the support profile. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <returns>The support profile.</returns>
        new Task<SupportProfile> GetAsync();

        /// <summary>
        /// Updates the support profile. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <param name="supportProfile">The support profile.</param>
        /// <returns>The updated support profile.</returns>
        new SupportProfile Update(SupportProfile supportProfile);

        /// <summary>
        /// Asynchronously updates the support profile. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <param name="supportProfile">The support profile.</param>
        /// <returns>The updated support profile.</returns>
        new Task<SupportProfile> UpdateAsync(SupportProfile supportProfile);
    }
}