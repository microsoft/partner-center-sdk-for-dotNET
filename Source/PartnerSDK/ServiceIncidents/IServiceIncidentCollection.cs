// -----------------------------------------------------------------------
// <copyright file="IServiceIncidentCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
namespace Microsoft.Store.PartnerCenter.ServiceIncidents
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Query;
    using Models.ServiceIncidents;

    /// <summary>
    /// Defines the operations available on service incidents.
    /// </summary>
    public interface IServiceIncidentCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<ServiceIncidents, ResourceCollection<ServiceIncidents>>
    {
        /// <summary>
        /// Retrieves all service incidents.
        /// </summary>
        /// <returns>The service incidents.</returns>
        new ResourceCollection<ServiceIncidents> Get();

        /// <summary>
        /// Asynchronously retrieves all service incidents.
        /// </summary>
        /// <returns>The service incidents.</returns>
        new Task<ResourceCollection<ServiceIncidents>> GetAsync();

        /// <summary>
        /// Queries service incidents.
        /// </summary>
        /// <param name="serviceIncidentsQuery">A query to retrieve service incidents based on the active status.
        /// The <see cref="Microsoft.Store.PartnerCenter.Models.Query.QueryFactory"/> can be used to build queries.
        /// Service incident queries support simple queries. You can filter service incidents using their active status.
        /// <see cref="Microsoft.Store.PartnerCenter.Models.ServiceIncidents.ServiceIncidentSearchField"/> lists
        /// the supported search fields. You can use the <see cref="Microsoft.Store.PartnerCenter.Models.Query.FieldFilterOperation" /> enumeration to specify filtering operations.
        /// Supported filtering operations are: equals.
        /// </param>
        /// <returns> The list of service incidents that match the query.</returns>
        ResourceCollection<ServiceIncidents> Query(IQuery serviceIncidentsQuery);

        /// <summary>
        /// Asynchronously queries service incidents.
        /// </summary>
        /// <param name="serviceIncidentsQuery">A query to retrieve service incidents based on the active status.
        /// The <see cref="Microsoft.Store.PartnerCenter.Models.Query.QueryFactory"/> can be used to build queries.
        /// Service incident queries support simple queries. You can filter service incidents using their active status.
        /// <see cref="Microsoft.Store.PartnerCenter.Models.ServiceIncidents.ServiceIncidentSearchField"/> lists
        /// the supported search fields. You can use the <see cref="Microsoft.Store.PartnerCenter.Models.Query.FieldFilterOperation" /> enumeration to specify filtering operations.
        /// Supported filtering operations are: equals.
        /// </param>
        /// <returns> The list of service incidents that match the query.</returns>
        Task<ResourceCollection<ServiceIncidents>> QueryAsync(IQuery serviceIncidentsQuery);
    }
}
