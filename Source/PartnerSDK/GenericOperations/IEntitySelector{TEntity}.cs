// -----------------------------------------------------------------------
// <copyright file="IEntitySelector{TEntity}.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.GenericOperations
{
    /// <summary>
    /// Defines operations for selecting an entity out of a collection.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity behavior.</typeparam>
    public interface IEntitySelector<TEntity>
    {
        /// <summary>
        /// Gets the behavior for an entity using the entity's ID.
        /// </summary>
        /// <param name="id">The entity's ID.</param>
        /// <returns>The entity's behavior.</returns>
        TEntity this[string id] { get; }

        /// <summary>
        /// Retrieves the behavior for an entity using the entity's ID.
        /// </summary>
        /// <param name="id">The entity's ID.</param>
        /// <returns>The entity's behavior.</returns>
        TEntity ById(string id);
    }
}
