// -----------------------------------------------------------------------
// <copyright file="IServiceRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ServiceRequests
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.ServiceRequests;

    /// <summary>
    /// Groups operations that can be performed on a single service request.
    /// </summary>
    public interface IServiceRequest : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<ServiceRequest>, IEntityPatchOperations<ServiceRequest>
    {
        /// <summary>
        /// Retrieves the service request.
        /// </summary>
        /// <returns>The service request information.</returns>
        new ServiceRequest Get();

        /// <summary>
        /// Asynchronously retrieves the service request.
        /// </summary>
        /// <returns>The service request information.</returns>
        new Task<ServiceRequest> GetAsync();

        /// <summary>
        /// Patches a service request.
        /// </summary>
        /// <param name="serviceRequest">The service request that has the properties to be patched set.</param>
        /// <returns>The updated service request.</returns>
        new ServiceRequest Patch(ServiceRequest serviceRequest);

        /// <summary>
        /// Asynchronously patches a service request.
        /// </summary>
        /// <param name="serviceRequest">The service request that has the properties to be patched set.</param>
        /// <returns>The updated service request.</returns>
        new Task<ServiceRequest> PatchAsync(ServiceRequest serviceRequest);
    }
}
