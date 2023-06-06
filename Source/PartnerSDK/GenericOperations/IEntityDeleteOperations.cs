// -----------------------------------------------------------------------
// <copyright file="IEntityDeleteOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.GenericOperations
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// Groups operations for deleting a single entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IEntityDeleteOperations<T> where T : ResourceBase
    {
        /// <summary>
        /// Deletes an entity.
        /// </summary>
        void Delete();

        /// <summary>
        /// Asynchronously deletes an entity.
        /// </summary>
        /// <returns>A task which completes once the deletion is done.</returns>
        Task DeleteAsync();
    }
}
