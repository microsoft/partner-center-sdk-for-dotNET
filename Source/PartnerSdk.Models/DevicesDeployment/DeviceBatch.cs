// -----------------------------------------------------------------------
// <copyright file="DeviceBatch.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.DevicesDeployment
{
    using System;
  
    /// <summary>
    /// Represents a devices batch associated with a customer.
    /// </summary>
    public sealed class DeviceBatch : StandardResourceLinks
    {
        /// <summary>
        /// Gets or sets the devices batch unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the tenant who created the batch.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date the batch was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the count of devices in the batch.
        /// </summary>
        public int DevicesCount { get; set; }

        /// <summary>
        /// Gets or sets the link to the devices under the batch.
        /// </summary>
        /// <value>
        /// Link to devices.
        /// </value>
        public Link DevicesLink { get; set; }
    }
}
