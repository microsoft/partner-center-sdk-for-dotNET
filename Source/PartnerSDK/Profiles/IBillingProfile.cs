// -----------------------------------------------------------------------
// <copyright file="IBillingProfile.cs" company="Microsoft Corporation">
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
    /// Encapsulates behavior of a billing profile.
    /// </summary>
    public interface IBillingProfile : IPartnerComponent, IEntityGetOperations<BillingProfile>, IEntityUpdateOperations<BillingProfile>
    {
        /// <summary>
        /// Retrieves a partner's billing profile.
        /// </summary>
        /// <returns>The billing profile.</returns>
        new BillingProfile Get();

        /// <summary>
        /// Asynchronously retrieves a partner's billing profile.
        /// </summary>
        /// <returns>The billing profile.</returns>
        new Task<BillingProfile> GetAsync();

        /// <summary>
        /// Updates the partner's billing profile.
        /// </summary>
        /// <param name="billingProfile">The billing profile information.</param>
        /// <returns>The updated billing profile.</returns>
        new BillingProfile Update(BillingProfile billingProfile);

        /// <summary>
        /// Asynchronously updates the partner's billing profile.
        /// </summary>
        /// <param name="billingProfile">The billing profile information.</param>
        /// <returns>The updated billing profile.</returns>
        new Task<BillingProfile> UpdateAsync(BillingProfile billingProfile);
    }
}