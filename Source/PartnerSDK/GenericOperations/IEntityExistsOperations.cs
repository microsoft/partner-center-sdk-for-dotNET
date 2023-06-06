// -----------------------------------------------------------------------
// <copyright file="IEntityExistsOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.GenericOperations
{
    using System.Threading.Tasks;

    /// <summary>
    /// Groups operations for checking if a single entity exists or not.
    /// </summary>
    public interface IEntityExistsOperations
    {
        /// <summary>
        /// Checks if an entity exists or not.
        /// </summary>
        /// <returns>A value indicating whether the entity exists or not.</returns>
        bool Exists();

        /// <summary>
        /// Asynchronously checks if an entity exists or not.
        /// </summary>
        /// <returns>A value indicating whether the entity exists or not.</returns>
        Task<bool> ExistsAsync();
    }
}
