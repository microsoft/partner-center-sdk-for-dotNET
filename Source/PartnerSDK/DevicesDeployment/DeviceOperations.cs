// -----------------------------------------------------------------------
// <copyright file="DeviceOperations.cs" company="Microsoft Corporation">
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
    /// Implements operations that apply to device.  
    /// </summary>
    internal class DeviceOperations : BasePartnerComponent<Tuple<string, string, string>>, IDevice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        /// <param name="deviceBatchId">The devices batch Id.</param>
        /// <param name="deviceId">The device Id.</param>
        public DeviceOperations(IPartner rootPartnerOperations, string customerId, string deviceBatchId, string deviceId)
            : base(rootPartnerOperations, new Tuple<string, string, string>(customerId, deviceBatchId, deviceId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException("customerId");
            }

            if (string.IsNullOrWhiteSpace(deviceBatchId))
            {
                throw new ArgumentNullException("deviceBatchId");
            }

            if (string.IsNullOrWhiteSpace(deviceId))
            {
                throw new ArgumentNullException("deviceId");
            }
        }

        /// <summary>
        /// Updates a device associated to the customer with a configuration policy.
        /// </summary>
        /// <param name="updateDevice">The device that is to be updated.</param>
        /// <returns>Device to be updated.</returns>
        public Device Patch(Device updateDevice)
        {
            return PartnerService.Instance.SynchronousExecute<Device>(() => this.PatchAsync(updateDevice));
        }

        /// <summary>
        /// Updates a device with configuration policy asynchronously.
        /// </summary>
        /// <param name="updateDevice">Payload of the update request.</param>
        /// <returns>Updated device.</returns>
        public async Task<Device> PatchAsync(Device updateDevice)
        {
            var partnerServiceProxy = new PartnerServiceProxy<Device, Device>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.UpdateDevice.Path, this.Context.Item1, this.Context.Item2, this.Context.Item3));

            return await partnerServiceProxy.PutAsync(updateDevice);
        }

        /// <summary>
        /// Deletes a device associated to the customer.
        /// </summary>
        public void Delete()
        {
            PartnerService.Instance.SynchronousExecute(this.DeleteAsync);
        }

        /// <summary>
        /// Deletes device associated to the customer asynchronously.
        /// </summary>
        /// <returns>An operation that completes when delete is complete.</returns>
        public async Task DeleteAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Device, Device>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.DeleteDevice.Path, this.Context.Item1, this.Context.Item2, this.Context.Item3));

            await partnerServiceProxy.DeleteAsync();
        }
    }
}
