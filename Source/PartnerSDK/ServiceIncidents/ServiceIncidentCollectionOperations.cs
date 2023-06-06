// -----------------------------------------------------------------------
// <copyright file="ServiceIncidentCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ServiceIncidents
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Query;
    using Models.ServiceIncidents;
    using Network;
    using Newtonsoft.Json;

    /// <summary>
    /// Service incident collection operations implementation class.
    /// </summary>
    internal class ServiceIncidentCollectionOperations : BasePartnerComponent, IServiceIncidentCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceIncidentCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public ServiceIncidentCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Gets the list of service incidents.
        /// </summary>
        /// <returns>List of active service incidents. </returns>
        public ResourceCollection<ServiceIncidents> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<ServiceIncidents>>(() => this.GetAsync());
        }

        /// <summary>
        /// Gets the list of service incidents.
        /// </summary>
        /// <param name="serviceIncidentsQuery">The query with search parameters.</param>
        /// <returns>List of active service incidents. </returns>
        public ResourceCollection<ServiceIncidents> Query(IQuery serviceIncidentsQuery)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<ServiceIncidents>>(() => this.QueryAsync(serviceIncidentsQuery));
        }

        /// <summary>
        /// Asynchronously gets the service incidents collection.
        /// </summary>
        /// <returns>List of service incidents. </returns>
        public async Task<ResourceCollection<ServiceIncidents>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ServiceIncidents, ResourceCollection<ServiceIncidents>>(
             this.Partner,
             string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetServiceIncidents.Path, this.Context));
            
            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Asynchronously gets the service incidents collection.
        /// </summary>
        /// <param name="serviceIncidentsQuery">The query with search parameters.</param>
        /// <returns>List of service incidents. </returns>
        public async Task<ResourceCollection<ServiceIncidents>> QueryAsync(IQuery serviceIncidentsQuery = null)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ServiceIncidents, ResourceCollection<ServiceIncidents>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetServiceIncidents.Path, this.Context));

            if (serviceIncidentsQuery.Filter != null)
            {
                // add the filter to the request if specified
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Filter, JsonConvert.SerializeObject(serviceIncidentsQuery.Filter)));
            }

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
