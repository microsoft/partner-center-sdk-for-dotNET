// -----------------------------------------------------------------------
// <copyright file="IEntitlementCollectionByEntitlementType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Entitlements
{
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Entitlements;

    /// <summary>
    /// Holds operations that can be performed on entitlements (by type) associated to the customer based on the logged in partner.
    /// </summary>
    public interface IEntitlementCollectionByEntitlementType : IEntireEntityCollectionRetrievalOperations<Entitlement, ResourceCollection<Entitlement>>
    {
        /// <summary>
        /// Retrieves entitlement collection with the given entitlement type.
        /// </summary>
        /// <param name="showExpiry">A flag to indicate if the expiry date is required to be returned along with the entitlement (if applicable).</param>
        /// <returns>The collection of entitlements corresponding to a specific entitlement type for the customer.</returns>
        ResourceCollection<Entitlement> Get(bool showExpiry);

        /// <summary>
        /// Asynchronously retrieves entitlement collection with the given entitlement type.
        /// </summary>
        /// <param name="showExpiry">A flag to indicate if the expiry date is required to be returned along with the entitlement (if applicable).</param>
        /// <returns>The collection of entitlements corresponding to a specific entitlement type for the customer.</returns>
        Task<ResourceCollection<Entitlement>> GetAsync(bool showExpiry);
    }
}
