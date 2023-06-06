// -----------------------------------------------------------------------
// <copyright file="IEntityPostOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.GenericOperations
{
    using System.Threading.Tasks;

    /// <summary>
    /// Groups operations for posting a single entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IEntityPostOperations<T> : IEntityPostOperations<T, T>
    {
    }

    /// <summary>
    /// Groups operations for posting a single entity.
    /// </summary>
    /// <typeparam name="T">The entity type request.</typeparam>
    /// <typeparam name="T1">The entity type response.</typeparam>
    public interface IEntityPostOperations<in T, T1>
    {
        /// <summary>
        /// Posts a new entity.
        /// </summary>
        /// <param name="newEntity">The new entity information.</param>
        /// <returns>The entity information from the post.</returns>
        T1 Post(T newEntity);

        /// <summary>
        /// Asynchronously posts a new entity.
        /// </summary>
        /// <param name="newEntity">The new entity information.</param>
        /// <returns>The entity information from the post.</returns>
        Task<T1> PostAsync(T newEntity);
    }
}