// -----------------------------------------------------------------------
// <copyright file="Device.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.DevicesDeployment
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a device associated with a customer.
    /// </summary>
    public sealed class Device : ResourceBase
    {
        /// <summary>
        /// Gets or sets the device unique identifier.
        /// </summary>
        /// <value>
        /// Device ID. 
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the serial number associated with a device.
        /// </summary>
        /// <value>
        /// Serial number.
        /// </value>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the product key uniquely associated with a device.
        /// </summary>
        /// <value>
        /// Product key.
        /// </value>
        public string ProductKey { get; set; }

        /// <summary>
        /// Gets or sets the hardware hash associated with a device.
        /// </summary>
        /// <value>
        /// Hardware hash.
        /// </value>
        public string HardwareHash { get; set; }

        /// <summary>
        /// Gets or sets the device model name associated with the device.
        /// </summary>
        /// <value>
        /// Device model name.
        /// </value>
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets the OEM manufacturer name.
        /// </summary>
        /// <value>
        /// OEM Manufacturer name.
        /// </value>
        public string OemManufacturerName { get; set; }

        /// <summary>
        /// Gets or sets the list of policies assigned to a device.
        /// </summary>
        /// <value>
        /// Key value pair list of policy IDs.
        /// </value>
        public List<KeyValuePair<PolicyCategory, string>> Policies { get; set; }

        /// <summary>
        /// Gets or sets the UTC date the device was uploaded.
        /// </summary>
        /// <value>
        /// UTC Date.
        /// </value>
        public DateTime UploadedDate { get; set; }

        /// <summary>
        /// Gets or sets the list of HTTP methods allowed on a device as GET, PATCH, DELETE.
        /// </summary>
        /// <value>
        /// List of strings.
        /// </value>
        public IEnumerable<string> AllowedOperations { get; set; }
    }
}
