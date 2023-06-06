// -----------------------------------------------------------------------
// <copyright file="DirectoryRoleOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerDirectoryRoles
{
    using System;

    /// <summary>
    /// Directory role operations class.
    /// </summary>
    internal class DirectoryRoleOperations : BasePartnerComponent<Tuple<string, string>>, IDirectoryRole
    {
        /// <summary>
        /// A lazy reference to the current directory role's user members operations.
        /// </summary>
        private readonly Lazy<IUserMemberCollection> directoryRoleUserMemberOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryRoleOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        /// <param name="roleId">The directory role I=id.</param>
        public DirectoryRoleOperations(IPartner rootPartnerOperations, string customerId, string roleId) : base(rootPartnerOperations, new Tuple<string, string>(customerId, roleId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(roleId))
            {
                throw new ArgumentException("roleId must be set.");
            }

            this.directoryRoleUserMemberOperations = new Lazy<IUserMemberCollection>(() => new UserMemberCollectionOperations(this.Partner, customerId, roleId));
        }

        /// <summary>
        /// Gets the current directory role's user member collection operations.
        /// </summary>
        public IUserMemberCollection UserMembers
        {
            get
            {
                return this.directoryRoleUserMemberOperations.Value;
            }
        }
    }
}
