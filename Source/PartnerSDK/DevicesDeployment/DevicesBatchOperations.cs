// -----------------------------------------------------------------------
// <copyright file="DevicesBatchOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.DevicesDeployment;
    using Network;

    /// <summary>
    /// Represents the operations that apply to devices batch of the customer.  
    /// </summary>
    internal class DevicesBatchOperations : BasePartnerComponent<Tuple<string, string>>, IDevicesBatch
    {
        /// <summary>
        /// The customer device collection operations.
        /// </summary>
        private readonly Lazy<IDeviceCollection> devices;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesBatchOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="deviceBatchId">The devices batch Id.</param>
        public DevicesBatchOperations(IPartner rootPartnerOperations, string customerId, string deviceBatchId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, deviceBatchId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(deviceBatchId))
            {
                throw new ArgumentNullException(nameof(deviceBatchId));
            }

            this.devices = new Lazy<IDeviceCollection>(() => new DeviceCollectionOperations(this.Partner, this.Context.Item1, this.Context.Item2));
        }

        /// <summary>
        /// Gets the device collection operations for the customer.
        /// </summary>
        /// <returns>The device collection operations.</returns>
        public IDeviceCollection Devices
        {
            get
            {
                return this.devices.Value;
            }
        }
    }
}
