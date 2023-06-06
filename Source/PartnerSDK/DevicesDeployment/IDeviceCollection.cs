// -----------------------------------------------------------------------
// <copyright file="IDeviceCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.DevicesDeployment;
 
    /// <summary>
    /// Represents the operations that can be done on the partner's devices.
    /// </summary>
    public interface IDeviceCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<Device, ResourceCollection<Device>>, IEntityCreateOperations<IEnumerable<Device>, string>, IEntitySelector<IDevice>
    {
        /// <summary>
        /// Retrieves a specific customer's device behavior.
        /// </summary>
        /// <param name="deviceId">The device id.</param>
        /// <returns>The customer device behavior.</returns>
        new IDevice this[string deviceId] { get; }

        /// <summary>
        /// Retrieves a specific customer's device behavior.
        /// </summary>
        /// <param name="deviceId">The device id.</param>
        /// <returns>The customer device behavior.</returns>
        new IDevice ById(string deviceId);

        /// <summary>
        /// Retrieves all devices.
        /// </summary>
        /// <returns>The devices.</returns>
        new ResourceCollection<Device> Get();

        /// <summary>
        /// Asynchronously retrieves all devices.
        /// </summary>
        /// <returns>The devices.</returns>
        new Task<ResourceCollection<Device>> GetAsync();

        /// <summary>
        /// Adds devices to existing devices batch.
        /// </summary>
        /// <param name="newDevices">The new devices to be created.</param>
        /// <returns>The location which indicates the URL of the API to query for status of the create request.</returns>
        new string Create(IEnumerable<Device> newDevices);

        /// <summary>
        /// Asynchronously adds devices to existing devices batch.
        /// </summary>
        /// <param name="newDevices">The new devices to be created.</param>
        /// <returns>The location which indicates the URL of the API to query for status of the create request.</returns>
        new Task<string> CreateAsync(IEnumerable<Device> newDevices);
    }
}
