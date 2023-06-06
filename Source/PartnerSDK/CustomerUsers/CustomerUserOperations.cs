// -----------------------------------------------------------------------
// <copyright file="CustomerUserOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerUsers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.Users;
    using Network;
    using Utilities;

    /// <summary>
    /// Customer user operations class.
    /// </summary>
    internal class CustomerUserOperations : BasePartnerComponent<Tuple<string, string>>, ICustomerUser
    {
        /// <summary>
        /// A lazy reference to the customer user directory role collection operations.
        /// </summary>
        private readonly Lazy<ICustomerUserRoleCollection> customerUserDirectoryRoleCollectionOperations;

        /// <summary>
        /// A lazy reference to the customer user license collection operations.
        /// </summary>
        private readonly Lazy<ICustomerUserLicenseCollection> customerUserLicenseCollectionOperations;

        /// <summary>
        /// A lazy reference to the customer user license update operations.
        /// </summary>
        private readonly Lazy<ICustomerUserLicenseUpdates> customerUserLicenseUpdateOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUserOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="userId">The user Id.</param>
        public CustomerUserOperations(IPartner rootPartnerOperations, string customerId, string userId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, userId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("userId must be set.");
            }

            this.customerUserDirectoryRoleCollectionOperations = new Lazy<ICustomerUserRoleCollection>(() => new CustomerUserRoleCollectionOperations(this.Partner, customerId, userId));
            this.customerUserLicenseCollectionOperations = new Lazy<ICustomerUserLicenseCollection>(() => new CustomerUserLicenseCollectionOperations(this.Partner, customerId, userId));
            this.customerUserLicenseUpdateOperations = new Lazy<ICustomerUserLicenseUpdates>(() => new CustomerUserLicenseUpdateOperations(this.Partner, customerId, userId));
        }

        /// <summary>
        /// Gets the current user's directory roles collection operation.
        /// </summary>
        public ICustomerUserRoleCollection DirectoryRoles
        {
            get
            {
                return this.customerUserDirectoryRoleCollectionOperations.Value;
            }
        }

        /// <summary>
        /// Gets the current user's license collection operation.
        /// </summary>
        public ICustomerUserLicenseCollection Licenses
        {
            get
            {
                return this.customerUserLicenseCollectionOperations.Value;
            }
        }

        /// <summary>
        /// Gets the current user's license updates operation.
        /// </summary>
        public ICustomerUserLicenseUpdates LicenseUpdates
        {
            get
            {
                return this.customerUserLicenseUpdateOperations.Value;
            }
        }

        /// <summary>
        /// Retrieves the customer user.
        /// </summary>
        /// <returns>The customer user.</returns>
        public CustomerUser Get()
        {
            return PartnerService.Instance.SynchronousExecute<CustomerUser>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the customer user.
        /// </summary>
        /// <returns>The customer order.</returns>
        public async Task<CustomerUser> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<CustomerUser, CustomerUser>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerUserDetails.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        public void Delete()
        {
            // To Do -  Task 6385643 is raised for unit test cases of remove and will be fixed by 25 Feb.
            PartnerService.Instance.SynchronousExecute(() => this.DeleteAsync());
        }

        /// <summary>
        /// Asynchronously deletes a user.
        /// </summary>
        /// <returns>Returns task.</returns>
        public async Task DeleteAsync()
        {
            // To Do -  Task 6385643 is raised for unit test cases of remove and will be fixed by 25 Feb.
            var partnerApiServiceProxy = new PartnerServiceProxy<CustomerUser, CustomerUser>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.DeleteCustomerUser.Path, this.Context.Item1, this.Context.Item2));

            await partnerApiServiceProxy.DeleteAsync();
        }

        /// <summary>
        /// Updates the customer user.
        /// </summary>
        /// <param name="entity">Customer user entity.</param>
        /// <returns>The updated user.</returns>
        public CustomerUser Patch(CustomerUser entity)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.PatchAsync(entity));
        }

        /// <summary>
        /// Asynchronously updates the customer user.
        /// </summary>
        /// <param name="entity">Customer user entity.</param>
        /// <returns>The updated user.</returns>
        public async Task<CustomerUser> PatchAsync(CustomerUser entity)
        {
            ParameterValidator.Required(entity, "User entity can't be null.");
            var partnerApiServiceProxy = new PartnerServiceProxy<CustomerUser, CustomerUser>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpdateCustomerUser.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.PatchAsync(entity);
        }
    }
}
