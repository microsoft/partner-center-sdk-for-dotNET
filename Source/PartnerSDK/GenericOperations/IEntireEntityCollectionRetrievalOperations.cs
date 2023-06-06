// -----------------------------------------------------------------------
// <copyright file="IEntireEntityCollectionRetrievalOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.GenericOperations
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// A generic interface which represents operations for getting an entire collection of entities.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <typeparam name="TResourceCollection">The entity collection type.</typeparam>
    public interface IEntireEntityCollectionRetrievalOperations<T, TResourceCollection>
        where TResourceCollection : ResourceCollection<T>
    {
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>The entities.</returns>
        TResourceCollection Get();

        /// <summary>
        /// Asynchronously retrieves all entities.
        /// </summary>
        /// <returns>The entities.</returns>
        Task<TResourceCollection> GetAsync();
    }
}
