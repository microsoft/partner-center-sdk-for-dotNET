// -----------------------------------------------------------------------
// <copyright file="IManagedServiceCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ManagedServices
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.ManagedServices;

    /// <summary>
    /// Holds the customer's managed services operations.
    /// </summary>
    public interface IManagedServiceCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<ManagedService, ResourceCollection<ManagedService>>
    {
        /// <summary>
        /// Retrieves all managed services. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <returns>The managed services.</returns>
        new ResourceCollection<ManagedService> Get();

        /// <summary>
        /// Asynchronously retrieves all managed services. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <returns>The managed services.</returns>
        new Task<ResourceCollection<ManagedService>> GetAsync();
    }
}
