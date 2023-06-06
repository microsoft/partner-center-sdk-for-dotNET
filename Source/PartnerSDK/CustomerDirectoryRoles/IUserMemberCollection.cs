// -----------------------------------------------------------------------
// <copyright file="IUserMemberCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerDirectoryRoles
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Query;
    using Models.Roles;

    /// <summary>
    /// Represents the behavior of user member collection.
    /// </summary>
    public interface IUserMemberCollection : IPartnerComponent<Tuple<string, string>>, IEntityCreateOperations<UserMember>, IEntireEntityCollectionRetrievalOperations<UserMember, SeekBasedResourceCollection<Models.Roles.UserMember>>, IEntitySelector<IUserMember>
    {
        /// <summary>
        /// Gets a single user member behavior.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The user member operations.</returns>
        new IUserMember this[string userId] { get; }

        /// <summary>
        /// Gets a single user member behavior.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The user member operations.</returns>
        new IUserMember ById(string userId);

        /// <summary>
        /// Adds customer user to a directory role.
        /// </summary>
        /// <param name="newEntity">UserMember to add.</param>
        /// <returns>The customer directory role user member.</returns>
        new UserMember Create(UserMember newEntity);

        /// <summary>
        /// Asynchronously adds customer user to a directory role.
        /// </summary>
        /// <param name="newEntity">UserMember to add.</param>
        /// <returns>The customer directory role user member.</returns>
        new Task<UserMember> CreateAsync(UserMember newEntity);

        /// <summary>
        /// Queries the user members of a customer directory role.
        /// </summary>
        /// <param name="query">A query to apply onto user member collection.</param>
        /// <returns>The directory role user members.</returns>
        SeekBasedResourceCollection<UserMember> Query(IQuery query);

        /// <summary>
        /// Asynchronously queries the user members of a customer directory role.
        /// </summary>
        /// <param name="query">A query to apply onto user member collection.</param>
        /// <returns>The directory role user members.</returns>
        Task<SeekBasedResourceCollection<UserMember>> QueryAsync(IQuery query);

        /// <summary>
        /// Gets all the user members of a customer directory role.
        /// </summary>
        /// <returns>The directory role user memberships.</returns>
        new SeekBasedResourceCollection<UserMember> Get();

        /// <summary>
        /// Asynchronously gets all the user members of a customer directory role.
        /// </summary>
        /// <returns>The directory role user members.</returns>
        new Task<SeekBasedResourceCollection<UserMember>> GetAsync();
    }
}