// -----------------------------------------------------------------------
// <copyright file="IUserMember.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerDirectoryRoles
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Roles;

    /// <summary>
    /// Represents the behavior of a user member.
    /// </summary>
    public interface IUserMember : IPartnerComponent<Tuple<string, string, string>>, IEntityDeleteOperations<UserMember>
    {
        /// <summary>
        /// Remove directory user member from directory role.
        /// </summary>
        new void Delete();

        /// <summary>
        /// Asynchronously remove directory user member from directory role.
        /// </summary>
        /// <returns>A task when the operation is finished.</returns>
        new Task DeleteAsync();
    }
}
