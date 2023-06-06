// -----------------------------------------------------------------------
// <copyright file="DeviceBatchCreationRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.DevicesDeployment
{
    using System;
    using System.Collections.Generic;
  
    /// <summary>
    /// Represents a devices batch creation model.
    /// </summary>
    public sealed class DeviceBatchCreationRequest : ResourceBase
    {
        /// <summary>
        /// Gets or sets the devices batch unique identifier.
        /// </summary>
        /// <value>
        /// Devices batch ID. 
        /// </value>
        public string BatchId { get; set; }

        /// <summary>
        /// Gets or sets the list of devices to be uploaded. 
        /// </summary>
        /// <value>
        /// List of devices to be uploaded.
        /// </value>
        public IEnumerable<Device> Devices { get; set; }
    }
}
