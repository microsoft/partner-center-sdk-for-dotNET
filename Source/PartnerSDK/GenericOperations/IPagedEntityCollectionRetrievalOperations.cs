// -----------------------------------------------------------------------
// <copyright file="IPagedEntityCollectionRetrievalOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// A generic interface which represents paged entity retrieval operations.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <typeparam name="TResourceCollection">The entity collection type.</typeparam>
    public interface IPagedEntityCollectionRetrievalOperations<T, TResourceCollection>
        where T : ResourceBase
        where TResourceCollection : ResourceCollection<T>
    {
        /// <summary>
        /// Retrieves a subset of entities.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of entities to return.</param>
        /// <returns>The requested entities subset.</returns>
        TResourceCollection Get(int offset, int size);

        /// <summary>
        /// Asynchronously retrieves a subset of entities.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of entities to return.</param>
        /// <returns>The requested entities subset.</returns>
        Task<TResourceCollection> GetAsync(int offset, int size);
    }
}
