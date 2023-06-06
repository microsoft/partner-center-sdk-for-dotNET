// -----------------------------------------------------------------------
// <copyright file="IDomain.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Domains
{
    using System.Threading.Tasks;
    using GenericOperations;

    /// <summary>
    /// Represents the behavior of a domain.
    /// </summary>
    public interface IDomain : IPartnerComponent, IEntityExistsOperations
    {
        /// <summary>
        /// Checks if the domain is available or not.
        /// </summary>
        /// <returns>True if the domain exists, false otherwise.</returns>
        new bool Exists();

        /// <summary>
        /// Asynchronously checks if the domain is available or not.
        /// </summary>
        /// <returns>True if the domain exists, false otherwise.</returns>
        new Task<bool> ExistsAsync();
    }
}