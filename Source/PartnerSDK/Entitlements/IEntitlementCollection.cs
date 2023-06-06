// -----------------------------------------------------------------------
// <copyright file="IEntitlementCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Entitlements
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Entitlements;

    /// <summary>
    /// Holds operations that can be performed on entitlements associated to the customer based on the logged in partner.
    /// </summary>
    public interface IEntitlementCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<Entitlement, ResourceCollection<Entitlement>>
    {
        /// <summary>
        /// Retrieves the operations that can be applied on entitlements with a given entitlement type.
        /// </summary>
        /// <param name="entitlementType">The Entitlement Type.</param>
        /// <returns>The entitlement collection operations by entitlement type.</returns>
        IEntitlementCollectionByEntitlementType ByEntitlementType(string entitlementType);

        /// <summary>
        /// Retrieves the entitlements for a customer.
        /// </summary>
        /// <param name="showExpiry">A flag to indicate if the expiry date is required to be returned along with the entitlement (if applicable).</param>
        /// <returns>The collection of entitlements for the customer.</returns>
        ResourceCollection<Entitlement> Get(bool showExpiry);

        /// <summary>
        /// An asynchronous operation to retrieve the entitlements for a customer.
        /// </summary>
        /// <param name="showExpiry">A flag to indicate if the expiry date is required to be returned along with the entitlement (if applicable).</param>
        /// <returns>The collection of entitlements for the customer.</returns>
        Task<ResourceCollection<Entitlement>> GetAsync(bool showExpiry);
    }
}
