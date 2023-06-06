// -----------------------------------------------------------------------
// <copyright file="ICustomerUserRoleCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerUsers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Models.Roles;

    /// <summary>
    /// Represents the behavior of the customers user's directory roles.
    /// </summary>
    public interface ICustomerUserRoleCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<DirectoryRole, ResourceCollection<DirectoryRole>>
    {
        /// <summary>
        /// Retrieves the customer user's directory roles.
        /// </summary>
        /// <returns>The customer user's directory roles.</returns>
        new ResourceCollection<DirectoryRole> Get();

        /// <summary>
        /// Asynchronously retrieves the customer user's directory roles.
        /// </summary>
        /// <returns>The customer user's directory roles.</returns>
        new Task<ResourceCollection<DirectoryRole>> GetAsync();
    }
}
