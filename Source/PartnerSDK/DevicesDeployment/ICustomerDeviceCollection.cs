// -----------------------------------------------------------------------
// <copyright file="ICustomerDeviceCollection.cs" company="Microsoft Corporation">
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
    public interface ICustomerDeviceCollection : IPartnerComponent
    {
        /// <summary>
        /// Updates the devices with configuration policies.
        /// </summary>
        /// <param name="devicePolicyUpdateRequest">The device policy update request with devices to be updated.</param>
        /// <returns>The location of the status to track the update.</returns>
        string Update(DevicePolicyUpdateRequest devicePolicyUpdateRequest);
    }
}
