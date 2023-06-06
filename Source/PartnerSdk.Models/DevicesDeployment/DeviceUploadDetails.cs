// -----------------------------------------------------------------------
// <copyright file="DeviceUploadDetails.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.DevicesDeployment
{
    using System;

    /// <summary>
    /// Represents the status of batch upload of devices.
    /// </summary>
    public class DeviceUploadDetails : ResourceBase
    {
        /// <summary>
        /// Gets or sets the device Id.
        /// </summary>
        /// <value>
        /// Id of the device.
        /// </value>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        /// <value>
        /// Serial number.
        /// </value>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the product key.
        /// </summary>
        /// <value>
        /// Product key.
        /// </value>
        public string ProductKey { get; set; }

        /// <summary>
        /// Gets or sets the device upload status.
        /// </summary>
        /// <value>
        /// Type of status of the upload.
        /// </value>
        public DeviceUploadStatusType Status { get; set; }

        /// <summary>
        /// Gets or sets the error code upon device upload failure.
        /// </summary>
        /// <value>
        /// Http status error code.
        /// </value>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error description upon device upload failure.
        /// </summary>
        /// <value>
        /// Http error message.
        /// </value>
        public string ErrorDescription { get; set; }
    }
}
