// -----------------------------------------------------------------------
// <copyright file="CustomerUsersCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerUsers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.Users;
    using Network;
    using Newtonsoft.Json;
    using PartnerCenter.Models;
    using PartnerCenter.Models.JsonConverters;
    using PartnerCenter.Models.Query;
    using Utilities;

    /// <summary>
    /// Customer user collection operations class.
    /// </summary>
    internal class CustomerUsersCollectionOperations : BasePartnerComponent, ICustomerUserCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUsersCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public CustomerUsersCollectionOperations(IPartner rootPartnerOperations, string customerId) : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Gets a specific customer user behavior.
        /// </summary>
        /// <param name="customerUserId">The customer tenant id.</param>
        /// <returns>The customer user operations.</returns>
        public ICustomerUser this[string customerUserId]
        {
            get
            {
                return this.ById(customerUserId);
            }
        }

        /// <summary>
        /// Obtains a specific customer user behavior.
        /// </summary>
        /// <param name="customerUserId">The customer tenant id.</param>
        /// <returns>The customer user operations.</returns>
        public ICustomerUser ById(string customerUserId)
        {
            return new CustomerUserOperations(this.Partner, this.Context, customerUserId);
        }

        /// <summary>
        /// Retrieves all the customer users.
        /// </summary>
        /// <returns>All the customer orders.</returns>
        public SeekBasedResourceCollection<CustomerUser> Get()
        {
            return PartnerService.Instance.SynchronousExecute<SeekBasedResourceCollection<CustomerUser>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves all the customer users.
        /// </summary>
        /// <returns>All the customer users.</returns>
        public async Task<SeekBasedResourceCollection<CustomerUser>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<CustomerUser, SeekBasedResourceCollection<CustomerUser>>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerUsers.Path, this.Context), jsonConverter: new ResourceCollectionConverter<CustomerUser>());

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Queries customer users associated to the partner's customers.
        /// - Count queries are not supported by this operation.
        /// - You can set page size, filter and sort option.
        /// - You can navigate to other pages by specifying a seek query with the seek operation and the continuation
        ///   token sent by the previous operation.
        /// </summary>
        /// <param name="customerusersQuery">A query to apply onto customer users collection. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The requested customer users.</returns>
        public SeekBasedResourceCollection<CustomerUser> Query(IQuery customerusersQuery)
        {
            return PartnerService.Instance.SynchronousExecute<SeekBasedResourceCollection<CustomerUser>>(() => this.QueryAsync(customerusersQuery));
        }

        /// <summary>
        /// Queries customer users associated to the partner's customers.
        /// </summary>
        /// <param name="customerusersQuery">A query to apply onto customer users collection.</param>
        /// <returns>Customer user collection.</returns>
        public async Task<SeekBasedResourceCollection<CustomerUser>> QueryAsync(IQuery customerusersQuery)
        {
            ParameterValidator.Required(customerusersQuery, "customerusersQuery can't be null");

            if (customerusersQuery.Type == QueryType.Count)
            {
                throw new ArgumentException("customerusersQuery can't be a count query.");
            }

            var partnerApiServiceProxy = new PartnerServiceProxy<CustomerUser, SeekBasedResourceCollection<CustomerUser>>(
                this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerUsers.Path, this.Context), jsonConverter: new ResourceCollectionConverter<CustomerUser>());

            if (customerusersQuery.Type == QueryType.Seek)
            {
                // if this is a seek query, add the seek operation and the continuation token to the request
                ParameterValidator.Required(customerusersQuery.Token, "customerusersQuery.Token is required.");

                partnerApiServiceProxy.AdditionalRequestHeaders.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetCustomers.AdditionalHeaders.ContinuationToken, customerusersQuery.Token.ToString()));

                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetCustomers.Parameters.SeekOperation, customerusersQuery.SeekOperation.ToString()));
            }
            else
            {
                if (customerusersQuery.Type == QueryType.Indexed)
                {
                    partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetCustomerUsers.Parameters.Size, customerusersQuery.PageSize.ToString()));
                }
                else
                {
                    partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetCustomerUsers.Parameters.Size, "0"));
                }

                if (customerusersQuery.Filter != null)
                {
                    // add the filter to the request if specified
                    partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetCustomerUsers.Parameters.Filter, WebUtility.UrlEncode(JsonConvert.SerializeObject(customerusersQuery.Filter))));
                }

                if (customerusersQuery.Sort != null)
                {
                    // add the sort details to the request if specified
                    partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetCustomerUsers.Parameters.SortField, customerusersQuery.Sort.SortField));

                    partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetCustomerUsers.Parameters.SortDirection, customerusersQuery.Sort.SortDirection.ToString()));
                }
            }

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Create a new user for the customer.
        /// </summary>
        /// <param name="newEntity">The user object containing details for the new user to be created.</param>
        /// <returns>User entity</returns>
        public CustomerUser Create(CustomerUser newEntity)
        {
            return PartnerService.Instance.SynchronousExecute<CustomerUser>(() => this.CreateAsync(newEntity));
        }

        /// <summary>
        /// Create a new user for the customer.
        /// </summary>
        /// <param name="newEntity">The user object containing details for the new user to be created.</param>
        /// <returns>User entity</returns>
        public async Task<CustomerUser> CreateAsync(CustomerUser newEntity)
        {
            ParameterValidator.Required(newEntity, "User entity can't be null.");
            var partnerApiServiceProxy = new PartnerServiceProxy<CustomerUser, CustomerUser>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CreateCustomerUser.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(newEntity);
        }
    }
}
