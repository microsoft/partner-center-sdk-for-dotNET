// -----------------------------------------------------------------------
// <copyright file="DevicePolicyUpdateRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.DevicesDeployment
{
    using System;
    using System.Collections.Generic;
  
    /// <summary>
    /// Represents the list of devices to be updated with a policy.
    /// </summary>
    public sealed class DevicePolicyUpdateRequest : ResourceBase
    {
        /// <summary>
        /// Gets or sets the list of devices to be updated. 
        /// </summary>
        /// <value>
        /// List of devices to be updated.
        /// </value>
        public IEnumerable<Device> Devices { get; set; }
    }
}
