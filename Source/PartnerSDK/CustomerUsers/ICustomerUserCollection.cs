// -----------------------------------------------------------------------
// <copyright file="ICustomerUserCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerUsers
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Microsoft.Store.PartnerCenter.Models.Users;
    using PartnerCenter.Models;
    using PartnerCenter.Models.Query;

    /// <summary>
    /// Represents the behavior of the customers users.
    /// </summary>
    public interface ICustomerUserCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<CustomerUser, SeekBasedResourceCollection<CustomerUser>>, IEntityCreateOperations<CustomerUser>, IEntitySelector<ICustomerUser>
    {
        /// <summary>
        /// Gets a single customer user operations.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The customer user operations.</returns>
        new ICustomerUser this[string userId] { get; }

        /// <summary>
        /// Gets a single customer user operations.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The customer user operations.</returns>
        new ICustomerUser ById(string userId);

        /// <summary>
        /// Queries users associated to the customer.
        /// - Count queries are not supported by this operation.
        /// - You can set page size, filter and sort option.
        /// - You can navigate to other pages by specifying a seek query with the seek operation and the continuation
        ///   token sent by the previous operation.
        /// </summary>
        /// <param name="customerUsersQuery">A query to apply onto customer users. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The requested customer users.</returns>
        SeekBasedResourceCollection<CustomerUser> Query(IQuery customerUsersQuery);

        /// <summary>
        /// Asynchronously queries users of customer.
        /// - Count queries are not supported by this operation.
        /// - You can set page size, filter and sort option.
        /// - You can navigate to other pages by specifying a seek query with the seek operation and the continuation
        ///   token sent by the previous operation.
        /// </summary>
        /// <param name="customerUsersQuery">A query to apply onto customer users. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The requested customer users.</returns>
        Task<SeekBasedResourceCollection<CustomerUser>> QueryAsync(IQuery customerUsersQuery);

        /// <summary>
        /// Create a new user for the customer.
        /// </summary>
        /// <param name="newEntity">The user object containing details for the new user to be created.</param>
        /// <returns>User entity</returns>
        new CustomerUser Create(CustomerUser newEntity);

        /// <summary>
        /// Create a new user for the customer.
        /// </summary>
        /// <param name="newEntity">The user object containing details for the new user to be created.</param>
        /// <returns>User entity</returns>
        new Task<CustomerUser> CreateAsync(CustomerUser newEntity);

        /// <summary>
        /// Retrieves all the customer users.
        /// </summary>
        /// <returns>All the customer orders.</returns>
        new SeekBasedResourceCollection<CustomerUser> Get();

        /// <summary>
        /// Asynchronously retrieves all the customer users.
        /// </summary>
        /// <returns>All the customer users.</returns>
        new Task<SeekBasedResourceCollection<CustomerUser>> GetAsync();
    }
}
