// -----------------------------------------------------------------------
// <copyright file="IServiceRequestCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ServiceRequests
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Query;
    using Models.ServiceRequests;

    /// <summary>
    /// Represents the behavior of service requests.
    /// </summary>
    public interface IServiceRequestCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<ServiceRequest, ResourceCollection<ServiceRequest>>, IEntitySelector<IServiceRequest>
    {
        /// <summary>
        /// Gets a single service request behavior.
        /// </summary>
        /// <param name="serviceRequestId">The service request ID.</param>
        /// <returns>The service request operations.</returns>
        new IServiceRequest this[string serviceRequestId] { get; }

        /// <summary>
        /// Gets a single service request behavior.
        /// </summary>
        /// <param name="serviceRequestId">The service request ID.</param>
        /// <returns>The service request operations.</returns>
        new IServiceRequest ById(string serviceRequestId);

        /// <summary>
        /// Retrieves all service requests.
        /// </summary>
        /// <returns>The service requests.</returns>
        new ResourceCollection<ServiceRequest> Get();

        /// <summary>
        /// Asynchronously retrieves all service requests.
        /// </summary>
        /// <returns>The service requests.</returns>
        new Task<ResourceCollection<ServiceRequest>> GetAsync();

        /// <summary>
        /// Queries service requests.
        /// - Count queries are not supported by this operation.
        /// - You can set the page size or filter or do both at the same time.
        /// - Sort is not supported. Default sorting is on status field.
        /// </summary>
        /// <param name="serviceRequestsQuery">A query to apply onto service requests. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The requested service requests.</returns>
        ResourceCollection<ServiceRequest> Query(IQuery serviceRequestsQuery);

        /// <summary>
        /// Asynchronously queries service requests.
        /// - Count queries are not supported by this operation.
        /// - You can set the page size or filter or do both at the same time.
        /// - Sort is not supported. Default sorting is on status field.
        /// </summary>
        /// <param name="serviceRequestsQuery">A query to apply onto service requests. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The requested service requests.</returns>
        Task<ResourceCollection<ServiceRequest>> QueryAsync(IQuery serviceRequestsQuery);
    }
}
