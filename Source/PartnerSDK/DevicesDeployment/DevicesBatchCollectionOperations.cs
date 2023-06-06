// -----------------------------------------------------------------------
// <copyright file="DevicesBatchCollectionOperations.cs" company="Microsoft Corporation">
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
    /// Implements operations that apply to devices batch collection.  
    /// </summary>
    internal class DevicesBatchCollectionOperations : BasePartnerComponent, IDevicesBatchCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesBatchCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public DevicesBatchCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException("customerId");
            }
        }

        /// <summary>
        /// Retrieves a specific customer's devices batch behavior.
        /// </summary>
        /// <param name="deviceBatchId">The devices batch id.</param>
        /// <returns>The customer devices batch operations.</returns>
        public IDevicesBatch this[string deviceBatchId]
        {
            get
            {
                return this.ById(deviceBatchId);
            }
        }

        /// <summary>
        /// Retrieves a specific customer devices batch.
        /// </summary>
        /// <param name="deviceBatchId">The devices batch id.</param>
        /// <returns>The customer devices batch behavior. </returns>
        public IDevicesBatch ById(string deviceBatchId)
        {
            return new DevicesBatchOperations(this.Partner, this.Context, deviceBatchId);
        }

        /// <summary>
        /// Creates a new devices batch along with the devices.
        /// </summary>
        /// <param name="newDeviceBatch">The new devices batch.</param>
        /// <returns>The location to track the status of the create.</returns>
        public string Create(DeviceBatchCreationRequest newDeviceBatch)
        {
            return PartnerService.Instance.SynchronousExecute<string>(() => this.CreateAsync(newDeviceBatch));
        }

        /// <summary>
        /// Asynchronously creates a new devices batch along with the devices.
        /// </summary>
        /// <param name="newDeviceBatch">The new devices batch information.</param>
        /// <returns>The location to track the status of the create.</returns>
        public async Task<string> CreateAsync(DeviceBatchCreationRequest newDeviceBatch)
        {
            if (newDeviceBatch == null)
            {
                throw new ArgumentNullException(nameof(newDeviceBatch));
            }

            var partnerServiceProxy = new PartnerServiceProxy<DeviceBatchCreationRequest, System.Net.Http.HttpResponseMessage>(
              this.Partner,
              string.Format(PartnerService.Instance.Configuration.Apis.CreateDeviceBatch.Path, this.Context));

            var response = await partnerServiceProxy.PostAsync(newDeviceBatch);

            return response.Headers.Location != null ? response.Headers.Location.ToString() : string.Empty;
        }

        /// <summary>
        /// Retrieves devices batches associated to the customer.
        /// </summary>
        /// <returns>A collection of devices batches. </returns>
        public ResourceCollection<DeviceBatch> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<DeviceBatch>>(this.GetAsync);
        }

        /// <summary>
        /// Retrieves devices batches associated to the customer asynchronously.
        /// </summary>
        /// <returns>A collection of devices batches. </returns>
        public async Task<ResourceCollection<DeviceBatch>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<DeviceBatch, ResourceCollection<DeviceBatch>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetDeviceBatches.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
