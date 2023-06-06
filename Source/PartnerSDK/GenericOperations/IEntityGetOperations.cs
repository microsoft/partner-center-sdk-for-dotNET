// -----------------------------------------------------------------------
// <copyright file="IEntityGetOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.GenericOperations
{
    using System.Threading.Tasks;
    
    /// <summary>
    /// Groups operations for getting a single entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IEntityGetOperations<T>
    {
        /// <summary>
        /// Retrieves an entity.
        /// </summary>
        /// <returns>The entity.</returns>
        T Get();

        /// <summary>
        /// Asynchronously retrieves an entity.
        /// </summary>
        /// <returns>The entity.</returns>
        Task<T> GetAsync();
    }
}
