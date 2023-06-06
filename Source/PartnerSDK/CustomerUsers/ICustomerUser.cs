// -----------------------------------------------------------------------
// <copyright file="ICustomerUser.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerUsers
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Microsoft.Store.PartnerCenter.Models.Users;

    /// <summary>
    /// Encapsulates a customer user behavior.
    /// </summary>
    public interface ICustomerUser : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<CustomerUser>, IEntityDeleteOperations<CustomerUser>, IEntityPatchOperations<CustomerUser>
    {
        /// <summary>
        /// Gets the current user's directory role collection operations.
        /// </summary>
        ICustomerUserRoleCollection DirectoryRoles { get; }

        /// <summary>
        /// Gets the current user's licenses collection operations.
        /// </summary>
        ICustomerUserLicenseCollection Licenses { get; }

        /// <summary>
        /// Gets the current user's license updates operations.
        /// </summary>
        ICustomerUserLicenseUpdates LicenseUpdates { get; }

        /// <summary>
        /// Retrieves the customer user.
        /// </summary>
        /// <returns>The customer user.</returns>
        new CustomerUser Get();

        /// <summary>
        /// Asynchronously retrieves the customer user.
        /// </summary>
        /// <returns>The customer order.</returns>
        new Task<CustomerUser> GetAsync();

        /// <summary>
        /// Deletes a user.
        /// </summary>
        new void Delete();

        /// <summary>
        /// Asynchronously deletes a user.
        /// </summary>
        /// <returns>Returns task.</returns>
        new Task DeleteAsync();

        /// <summary>
        /// Updates the customer user.
        /// </summary>
        /// <param name="entity">Customer user entity.</param>
        /// <returns>The updated user.</returns>
        new CustomerUser Patch(CustomerUser entity);

        /// <summary>
        /// Asynchronously updates the customer user.
        /// </summary>
        /// <param name="entity">Customer user entity.</param>
        /// <returns>The updated user.</returns>
        new Task<CustomerUser> PatchAsync(CustomerUser entity);
    }
}
