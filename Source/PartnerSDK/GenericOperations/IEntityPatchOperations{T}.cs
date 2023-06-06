// -----------------------------------------------------------------------
// <copyright file="IEntityPatchOperations{T}.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.GenericOperations
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// Groups operations for patching a single entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IEntityPatchOperations<T> where T : ResourceBase
    {
        /// <summary>
        /// Patches an entity.
        /// </summary>
        /// <param name="entity">The entity information.</param>
        /// <returns>The updated entity.</returns>
        T Patch(T entity);

        /// <summary>
        /// Asynchronously patches an entity.
        /// </summary>
        /// <param name="entity">The entity information.</param>
        /// <returns>The updated entity.</returns>
        Task<T> PatchAsync(T entity);
    }
}
