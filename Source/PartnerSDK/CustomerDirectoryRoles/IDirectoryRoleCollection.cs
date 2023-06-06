// -----------------------------------------------------------------------
// <copyright file="IDirectoryRoleCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerDirectoryRoles
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Roles;

    /// <summary>
    /// Represents the behavior of directory role collection.
    /// </summary>
    public interface IDirectoryRoleCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<DirectoryRole, ResourceCollection<DirectoryRole>>, IEntitySelector<IDirectoryRole>
    {
        /// <summary>
        /// Gets a directory role behavior.
        /// </summary>
        /// <param name="roleId">The directory role id.</param>
        /// <returns>The directory role operations.</returns>
        new IDirectoryRole this[string roleId] { get; }

        /// <summary>
        /// Gets a directory role behavior.
        /// </summary>
        /// <param name="roleId">The directory role id.</param>
        /// <returns>The directory role operations.</returns>
        new IDirectoryRole ById(string roleId);

        /// <summary>
        /// Retrieves all customer directory roles.
        /// </summary>
        /// <returns>All the customer directory roles.</returns>
        new ResourceCollection<DirectoryRole> Get();

        /// <summary>
        /// Asynchronously retrieves all customer directory roles.
        /// </summary>
        /// <returns>All the customer directory roles.</returns>
        new Task<ResourceCollection<DirectoryRole>> GetAsync();
    }
}
