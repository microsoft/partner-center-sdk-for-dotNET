// -----------------------------------------------------------------------
// <copyright file="CustomerUserRoleCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerUsers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Users;
    using Microsoft.Store.PartnerCenter.Network;
    using Models.Roles;

    /// <summary>
    /// Represents the behavior of the customers user's directory roles.
    /// </summary>
    internal class CustomerUserRoleCollectionOperations : BasePartnerComponent<Tuple<string, string>>, ICustomerUserRoleCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUserRoleCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant id.</param>
        /// <param name="userId">The user id.</param>
        public CustomerUserRoleCollectionOperations(IPartner rootPartnerOperations, string customerId, string userId) : base(rootPartnerOperations, new Tuple<string, string>(customerId, userId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("userId must be set.");
            }
        }

        /// <summary>
        /// Retrieves the customer user's directory roles.
        /// </summary>
        /// <returns>The customer user's directory roles.</returns>
        public ResourceCollection<DirectoryRole> Get()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the customer user's directory roles.
        /// </summary>
        /// <returns>The customer user's directory roles.</returns>
        public async Task<ResourceCollection<DirectoryRole>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<CustomerUser, ResourceCollection<DirectoryRole>>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CustomerUserDirectoryRoles.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
