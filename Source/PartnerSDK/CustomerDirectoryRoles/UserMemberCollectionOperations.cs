// -----------------------------------------------------------------------
// <copyright file="UserMemberCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerDirectoryRoles
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.JsonConverters;
    using Models.Query;
    using Models.Roles;
    using Network;
    using Utilities;

    /// <summary>
    /// User member collection operations class.
    /// </summary>
    internal class UserMemberCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IUserMemberCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMemberCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The partner operations instance.</param>
        /// <param name="customerId">Customer id.</param>
        /// <param name="roleId">The directory role Id.</param>
        public UserMemberCollectionOperations(IPartner rootPartnerOperations, string customerId, string roleId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, roleId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(roleId))
            {
                throw new ArgumentException("roleId must be set.");
            }
        }

        /// <summary>
        /// Get a single user member operations object.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The user member operations instance.</returns>
        public IUserMember this[string userId]
        {
            get
            {
                return this.ById(userId);
            }
        }

        /// <summary>
        /// Get a single user member operations object.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The user member operations instance.</returns>
        public IUserMember ById(string userId)
        {
            return new UserMemberOperations(this.Partner, this.Context.Item1, this.Context.Item2, userId);
        }

        /// <summary>
        /// Adds customer user to a directory role.
        /// </summary>
        /// <param name="newEntity">UserMember to add.</param>
        /// <returns>The customer directory role user member.</returns>
        public UserMember Create(UserMember newEntity)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.CreateAsync(newEntity));
        }

        /// <summary>
        /// Asynchronously adds customer user to a directory role.
        /// </summary>
        /// <param name="newEntity">UserMember to add.</param>
        /// <returns>The customer directory role user member.</returns>
        public async Task<UserMember> CreateAsync(UserMember newEntity)
        {
            ParameterValidator.Required(newEntity, "entity can't be null");

            var partnerServiceProxy = new PartnerServiceProxy<UserMember, UserMember>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.AddUserToCustomerDirectoryRole.Path, this.Context.Item1, this.Context.Item2), jsonConverter: new ResourceCollectionConverter<Models.Roles.UserMember>());
            return await partnerServiceProxy.PostAsync(newEntity);
        }

        /// <summary>
        /// Gets all the user members of a customer directory role.
        /// </summary>
        /// <returns>The directory role user memberships.</returns>
        public SeekBasedResourceCollection<UserMember> Get()
        {
            return PartnerService.Instance.SynchronousExecute<SeekBasedResourceCollection<Models.Roles.UserMember>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets all the user members of a customer directory role.
        /// </summary>
        /// <returns>The directory role user members.</returns>
        public async Task<SeekBasedResourceCollection<UserMember>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<DirectoryRole, SeekBasedResourceCollection<Models.Roles.UserMember>>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerDirectoryRoleUserMembers.Path, this.Context.Item1, this.Context.Item2), jsonConverter: new ResourceCollectionConverter<Models.Roles.UserMember>());
            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Retrieves the user members of a customer directory role.
        /// </summary>
        /// <param name="query">A query to apply onto user member collection.</param>
        /// <returns>The directory role user memberships.</returns>
        public SeekBasedResourceCollection<UserMember> Query(IQuery query)
        {
            return PartnerService.Instance.SynchronousExecute<SeekBasedResourceCollection<Models.Roles.UserMember>>(() => this.QueryAsync(query));
        }

        /// <summary>
        /// Asynchronously retrieves the user members of a customer directory role.
        /// </summary>
        /// <param name="query">A query to apply onto user member collection.</param>
        /// <returns>The directory role user members.</returns>
        public async Task<SeekBasedResourceCollection<Models.Roles.UserMember>> QueryAsync(IQuery query)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<DirectoryRole, SeekBasedResourceCollection<Models.Roles.UserMember>>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerDirectoryRoleUserMembers.Path, this.Context.Item1, this.Context.Item2), jsonConverter: new ResourceCollectionConverter<Models.Roles.UserMember>());

            if (query != null)
            {
                if (query.Type == QueryType.Count)
                {
                    throw new ArgumentException("query can't be a count query.");
                }

                if (query.Type == QueryType.Seek)
                {
                    // if this is a seek query, add the seek operation and the continuation token to the request.
                    ParameterValidator.Required(query.Token, "query.Token is required.");

                    partnerApiServiceProxy.AdditionalRequestHeaders.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetCustomerUsers.AdditionalHeaders.ContinuationToken, query.Token.ToString()));

                    partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetCustomerUsers.Parameters.SeekOperation, query.SeekOperation.ToString()));
                }
                else
                {
                    if (query.Type == QueryType.Indexed)
                    {
                        partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                            PartnerService.Instance.Configuration.Apis.GetCustomerUsers.Parameters.Size, query.PageSize.ToString()));
                    }
                    else
                    {
                        partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                            PartnerService.Instance.Configuration.Apis.GetCustomerUsers.Parameters.Size, "0"));
                    }
                }
            }

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}