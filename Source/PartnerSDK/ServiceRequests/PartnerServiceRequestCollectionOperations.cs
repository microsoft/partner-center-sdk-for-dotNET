// -----------------------------------------------------------------------
// <copyright file="PartnerServiceRequestCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ServiceRequests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.JsonConverters;
    using Models.Query;
    using Models.ServiceRequests;
    using Network;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>
    /// The partner's service requests operations and implementation.
    /// </summary>
    internal class PartnerServiceRequestCollectionOperations : BasePartnerComponent, IPartnerServiceRequestCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerServiceRequestCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public PartnerServiceRequestCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Gets a Service Request specified by Id.
        /// </summary>
        /// <param name="serviceRequestId">Incident Id.</param>
        /// <returns>Service Request Operations.</returns>
        public IServiceRequest this[string serviceRequestId]
        {
            get
            {
                return this.ById(serviceRequestId);
            }
        }

        /// <summary>
        /// Retrieves a Service Request specified by Id.
        /// </summary>
        /// <param name="serviceRequestId">Incident Id.</param>
        /// <returns>Service Request Operations.</returns>
        public IServiceRequest ById(string serviceRequestId)
        {
            return new PartnerServiceRequestOperations(this.Partner, serviceRequestId);
        }

        /// <summary>
        /// Queries for service requests associated with the partner.
        /// </summary>
        /// <param name="serviceRequestsQuery">The query with search parameters.</param>
        /// <returns>A collection of service requests matching the criteria.</returns>
        public ResourceCollection<ServiceRequest> Query(IQuery serviceRequestsQuery)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.QueryAsync(serviceRequestsQuery));
        }

        /// <summary>
        /// Queries asynchronously for service requests associated with the partner
        /// </summary>
        /// <param name="serviceRequestsQuery">The query with search parameters.</param>
        /// <returns>A collection of service requests matching the criteria.</returns>
        public async Task<ResourceCollection<ServiceRequest>> QueryAsync(IQuery serviceRequestsQuery)
        {
            ParameterValidator.Required(serviceRequestsQuery, "serviceRequestsQuery can't be null");

            if (serviceRequestsQuery.Type != QueryType.Indexed && serviceRequestsQuery.Type != QueryType.Simple)
            {
                throw new ArgumentException("Specified query type is not supported.");
            }

            var partnerServiceProxy = new PartnerServiceProxy<ServiceRequest, ResourceCollection<ServiceRequest>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Path,
                jsonConverter: new ResourceCollectionConverter<ServiceRequest>());

            if (serviceRequestsQuery.Type == QueryType.Indexed)
            {
                // get all service requests in case a -ve page size was specified
                serviceRequestsQuery.PageSize = Math.Max(serviceRequestsQuery.PageSize, 0);

                partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Size, serviceRequestsQuery.PageSize.ToString()));

                partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Offset, serviceRequestsQuery.Index.ToString()));
            }

            if (serviceRequestsQuery.Filter != null)
            {
                // add the filter to the request if specified
                partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Filter, JsonConvert.SerializeObject(serviceRequestsQuery.Filter)));
            }
            
            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Retrieves Service Requests associated to the partner.
        /// </summary>
        /// <returns>A collection of service requests.</returns>
        public ResourceCollection<ServiceRequest> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<ServiceRequest>>(this.GetAsync);
        }

        /// <summary>
        /// Retrieves Service Requests associated to the partner asynchronously.
        /// </summary>
        /// <returns>A collection of service requests.</returns>
        public async Task<ResourceCollection<ServiceRequest>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ServiceRequest, ResourceCollection<ServiceRequest>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetAllServiceRequestsPartner.Path);

            return await partnerServiceProxy.GetAsync();
        }
    }
}
