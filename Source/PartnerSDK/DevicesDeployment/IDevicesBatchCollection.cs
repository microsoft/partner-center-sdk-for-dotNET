// -----------------------------------------------------------------------
// <copyright file="IDevicesBatchCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.DevicesDeployment;

    /// <summary>
    /// Represents the operations that can be done on the partner's devices batches.
    /// </summary>
    public interface IDevicesBatchCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<DeviceBatch, ResourceCollection<DeviceBatch>>, IEntitySelector<IDevicesBatch>, IEntityCreateOperations<DeviceBatchCreationRequest, string>
    {
        /// <summary>
        /// Retrieves a specific customer devices batch behavior.
        /// </summary>
        /// <param name="devicebatchId">The devices batch id.</param>
        /// <returns>The devices batch behavior.</returns>
        new IDevicesBatch this[string devicebatchId] { get; }

        /// <summary>
        /// Retrieves a specific customer devices batch behavior.
        /// </summary>
        /// <param name="devicebatchId">The devices batch id.</param>
        /// <returns>The devices batch behavior.</returns>
        new IDevicesBatch ById(string devicebatchId);

        /// <summary>
        /// Retrieves all devices batches.
        /// </summary>
        /// <returns>The devices batches.</returns>
        new ResourceCollection<DeviceBatch> Get();

        /// <summary>
        /// Asynchronously retrieves all devices batches.
        /// </summary>
        /// <returns>The devices batches.</returns>
        new Task<ResourceCollection<DeviceBatch>> GetAsync();
    }
}