// -----------------------------------------------------------------------
// <copyright file="DirectoryRoleCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerDirectoryRoles
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.JsonConverters;
    using Models.Roles;
    using Network;    
    
    /// <summary>
    /// Directory role collection operations class.
    /// </summary>
    internal class DirectoryRoleCollectionOperations : BasePartnerComponent, IDirectoryRoleCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryRoleCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public DirectoryRoleCollectionOperations(IPartner rootPartnerOperations, string customerId) : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Gets a directory role operations object.
        /// </summary>
        /// <param name="roleId">The directory role id.</param>
        /// <returns>The directory role operations object.</returns>
        public IDirectoryRole this[string roleId]
        {
            get
            {
                return this.ById(roleId);
            }
        }

        /// <summary>
        /// Gets a directory role operations object.
        /// </summary>
        /// <param name="roleId">The directory role id.</param>
        /// <returns>The directory role operations object.</returns>
        public IDirectoryRole ById(string roleId)
        {
            return new DirectoryRoleOperations(this.Partner, this.Context, roleId);
        }

        /// <summary>
        /// Retrieves all customer directory roles.
        /// </summary>
        /// <returns>All the customer directory roles.</returns>
        public ResourceCollection<DirectoryRole> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<DirectoryRole>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves all customer directory roles.
        /// </summary>
        /// <returns>All the customer directory roles.</returns>
        public async Task<ResourceCollection<DirectoryRole>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<DirectoryRole, ResourceCollection<DirectoryRole>>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerDirectoryRoles.Path, this.Context), jsonConverter: new ResourceCollectionConverter<DirectoryRole>());

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
