// -----------------------------------------------------------------------
// <copyright file="IEntityCreateOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.GenericOperations
{
    using System.Threading.Tasks;

    /// <summary>
    /// Groups operations for creating a single entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IEntityCreateOperations<T> : IEntityCreateOperations<T, T>
    {
    }

    /// <summary>
    /// Groups operations for creating a single entity.
    /// </summary>
    /// <typeparam name="T">The entity type request.</typeparam>
    /// <typeparam name="T1">The entity type response.</typeparam>
    public interface IEntityCreateOperations<in T, T1>
    {
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="newEntity">The new entity information.</param>
        /// <returns>The entity information that was just created.</returns>
        T1 Create(T newEntity);

        /// <summary>
        /// Asynchronously creates a new entity.
        /// </summary>
        /// <param name="newEntity">The new entity information.</param>
        /// <returns>The entity information that was just created.</returns>
        Task<T1> CreateAsync(T newEntity);
    }
}
