// -----------------------------------------------------------------------
// <copyright file="ISelfServePoliciesCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.SelfServePolicies
{
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.SelfServePolicies;

    /// <summary>
    /// This interface represents the operations that can be done on a self serve policy.
    /// </summary>
    public interface ISelfServePoliciesCollection : IPartnerComponent, IEntityCreateOperations<SelfServePolicy>
    {
        /// <summary>
        /// Gets a single self serve policy's operations.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The customer user operations.</returns>
        ISelfServePolicy ById(string userId);

        /// <summary>
        /// Retrieves all the self serve policies by entity id.
        /// </summary>
        /// <param name="entityId">The id of the entity.</param>
        /// <returns>The self serve policies.</returns>
        Task<ResourceCollection<SelfServePolicy>> GetAsync(string entityId);

        /// <summary>
        /// Retrieves all the self serve policies by entity id.
        /// </summary>
        /// <param name="entityId">The id of the entity.</param>
        /// <returns>The self serve policies.</returns>
        ResourceCollection<SelfServePolicy> Get(string entityId);
    }
}
