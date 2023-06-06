// -----------------------------------------------------------------------
// <copyright file="IDevice.cs" company="Microsoft Corporation">
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
    /// Represents the operations that can be done on the partner's device.
    /// </summary>
    public interface IDevice : IPartnerComponent<Tuple<string, string, string>>, IEntityPatchOperations<Device>, IEntityDeleteOperations<Device>
    {
        /// <summary>
        /// Updates a device associated to the customer with a configuration policy.
        /// </summary>
        /// <param name="updateDevice">Device to be updated.</param>
        /// <returns>Device that is to be updated.</returns>
        new Device Patch(Device updateDevice);

        /// <summary>
        /// Asynchronously updates a device with a configuration policy.
        /// </summary>
        /// <param name="updateDevice">Device to be updated. </param>
        /// <returns>The updated device.</returns>
        new Task<Device> PatchAsync(Device updateDevice);

        /// <summary>
        /// Deletes a device.
        /// </summary>
        new void Delete();

        /// <summary>
        /// Asynchronously deletes a device.
        /// </summary>
        /// <returns>The task that is returned once the operation is completed.</returns>
        new Task DeleteAsync();
    }
}
