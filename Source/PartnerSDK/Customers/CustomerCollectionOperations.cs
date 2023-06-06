// -----------------------------------------------------------------------
// <copyright file="CustomerCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Models;
    using Models.Customers;
    using Models.JsonConverters;
    using Models.Query;
    using Network;
    using Newtonsoft.Json;
    using RelationshipRequests;
    using Usage;
    using Utilities;

    /// <summary>
    /// The partner customers implementation.
    /// </summary>
    internal class CustomerCollectionOperations : BasePartnerComponent, ICustomerCollection
    {
        /// <summary>
        /// A lazy reference to the current partner's customer usage records operations.
        /// </summary>
        private Lazy<ICustomerUsageRecordCollection> usageRecords;

        /// <summary>
        /// A lazy reference to a customer relationship request operations..
        /// </summary>
        private Lazy<ICustomerRelationshipRequest> relationshipRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public CustomerCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
            this.usageRecords = new Lazy<ICustomerUsageRecordCollection>(() => new CustomerUsageRecordCollectionOperations(this.Partner));
            this.relationshipRequest = new Lazy<ICustomerRelationshipRequest>(() => new CustomerRelationshipRequestOperations(this.Partner));
        }

        /// <summary>
        /// Obtains the customer usage records behavior for the partner.
        /// </summary>
        /// <returns>The requested customers usage record.</returns>
        public ICustomerUsageRecordCollection UsageRecords
        {
            get { return this.usageRecords.Value; }
        }

        /// <summary>
        /// Gets the relationship request behavior used to relate customers into the partner.
        /// </summary>
        public ICustomerRelationshipRequest RelationshipRequest
        {
            get { return this.relationshipRequest.Value; }
        }

        /// <summary>
        /// Gets a single customer operations.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <returns>The customer operations.</returns>
        public ICustomer this[string customerId]
        {
            get
            {
                return this.ById(customerId);
            }
        }

        /// <summary>
        /// Gets a single customer operations.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <returns>The customer operations.</returns>
        public ICustomer ById(string customerId)
        {
            return new CustomerOperations(this.Partner, customerId);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="newCustomer">The new customer information.</param>
        /// <returns>The customer information that was just created.</returns>
        public Customer Create(Customer newCustomer)
        {
            return PartnerService.Instance.SynchronousExecute<Customer>(() => this.CreateAsync(newCustomer));
        }

        /// <summary>
        /// Asynchronously creates a new customer.
        /// </summary>
        /// <param name="newCustomer">The new customer information.</param>
        /// <returns>The customer information that was just created.</returns>
        public async Task<Customer> CreateAsync(Customer newCustomer)
        {
            ParameterValidator.Required(newCustomer, "newCustomer can't be null");

            var partnerServiceProxy = new PartnerServiceProxy<Customer, Customer>(
               this.Partner,
               PartnerService.Instance.Configuration.Apis.CreateCustomer.Path,
               jsonConverter: new ResourceCollectionConverter<Customer>());

            return await partnerServiceProxy.PostAsync(newCustomer);
        }

        /// <summary>
        /// Retrieves all customers associated to the partner.
        /// </summary>
        /// <returns>All customers.</returns>
        public SeekBasedResourceCollection<Customer> Get()
        {
            return PartnerService.Instance.SynchronousExecute<SeekBasedResourceCollection<Customer>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves all customers associated to the partner.
        /// </summary>
        /// <returns>All customers.</returns>
        public async Task<SeekBasedResourceCollection<Customer>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Customer, SeekBasedResourceCollection<Customer>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetCustomers.Path,
                jsonConverter: new ResourceCollectionConverter<Customer>());

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Queries customers associated to the partner.
        /// - Count queries are not supported by this operation.
        /// - You can set the page size or filter or do both at the same time.
        /// - Sort is not supported.
        /// - You can navigate to other pages by specifying a seek query with the seek operation and the continuation
        ///   token sent by the previous operation.
        /// </summary>
        /// <param name="customersQuery">A query to apply onto customers. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The requested customers.</returns>
        public SeekBasedResourceCollection<Customer> Query(IQuery customersQuery)
        {
            return PartnerService.Instance.SynchronousExecute<SeekBasedResourceCollection<Customer>>(() => this.QueryAsync(customersQuery));
        }

        /// <summary>
        /// Asynchronously queries customers associated to the partner.
        /// - Count queries are not supported by this operation.
        /// - You can set the page size or filter or do both at the same time.
        /// - Sort is not supported.
        /// - You can navigate to other pages by specifying a seek query with the seek operation and the continuation
        ///   token sent by the previous operation.
        /// </summary>
        /// <param name="customersQuery">A query to apply onto customers. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The requested customers.</returns>
        public async Task<SeekBasedResourceCollection<Customer>> QueryAsync(IQuery customersQuery)
        {
            ParameterValidator.Required(customersQuery, "customersQuery can't be null");

            if (customersQuery.Type == QueryType.Count)
            {
                throw new ArgumentException("customersQuery can't be a count query.");
            }

            var partnerServiceProxy = new PartnerServiceProxy<Customer, SeekBasedResourceCollection<Customer>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetCustomers.Path,
                jsonConverter: new ResourceCollectionConverter<Customer>());

            if (customersQuery.Type == QueryType.Seek)
            {
                // if this is a seek query, add the seek operation and the continuation token to the request
                ParameterValidator.Required(customersQuery.Token, "customersQuery.Token is required.");

                partnerServiceProxy.AdditionalRequestHeaders.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetCustomers.AdditionalHeaders.ContinuationToken, customersQuery.Token.ToString()));

                partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetCustomers.Parameters.SeekOperation, customersQuery.SeekOperation.ToString()));
            }
            else
            {
                if (customersQuery.Type == QueryType.Indexed)
                {
                    customersQuery.PageSize = Math.Max(customersQuery.PageSize, 0);

                    partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetCustomers.Parameters.Size, customersQuery.PageSize.ToString()));
                }
                else
                {
                    partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetCustomers.Parameters.Size, "0"));
                }

                if (customersQuery.Filter != null)
                {
                    // add the filter to the request if specified
                    partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetCustomers.Parameters.Filter, WebUtility.UrlEncode(JsonConvert.SerializeObject(customersQuery.Filter))));
                }
            }

            return await partnerServiceProxy.GetAsync();
        }
    }
}