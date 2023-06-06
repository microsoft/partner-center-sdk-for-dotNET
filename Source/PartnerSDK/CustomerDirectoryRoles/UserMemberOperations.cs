// -----------------------------------------------------------------------
// <copyright file="UserMemberOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerDirectoryRoles
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Roles;
    using Network;

    /// <summary>
    /// User member operations operations class.
    /// </summary>
    internal class UserMemberOperations : BasePartnerComponent<Tuple<string, string, string>>, IUserMember
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMemberOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The partner operations instance.</param>
        /// <param name="customerId">Customer id.</param>
        /// <param name="roleId">The directory role Id.</param>
        /// <param name="userId">The user Id.</param>
        public UserMemberOperations(IPartner rootPartnerOperations, string customerId, string roleId, string userId)
            : base(rootPartnerOperations, new Tuple<string, string, string>(customerId, roleId, userId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(roleId))
            {
                throw new ArgumentException("roleId must be set.");
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("userId must be set.");
            }
        }

        /// <summary>
        /// Remove directory user member from directory role.
        /// </summary>
        public void Delete()
        {
            PartnerService.Instance.SynchronousExecute(() => this.DeleteAsync());
        }

        /// <summary>
        /// Asynchronously remove directory user member from directory role.
        /// </summary>
        /// <returns>A task when the operation is finished.</returns>
        public async Task DeleteAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<DirectoryRole, DirectoryRole>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.RemoveCustomerUserMemberFromDirectoryRole.Path, this.Context.Item1, this.Context.Item2, this.Context.Item3));

            await partnerApiServiceProxy.DeleteAsync();
        }
    }
}