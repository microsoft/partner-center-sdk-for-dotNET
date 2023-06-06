// -----------------------------------------------------------------------
// <copyright file="DeviceCollectionOperations.cs" company="Microsoft Corporation">
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
    /// Implements operations that apply to device collections.  
    /// </summary>
    internal class DeviceCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IDeviceCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        /// <param name="deviceBatchId">The devices batch Id.</param>
        public DeviceCollectionOperations(IPartner rootPartnerOperations, string customerId, string deviceBatchId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, deviceBatchId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException("customerId");
            }

            if (string.IsNullOrWhiteSpace(deviceBatchId))
            {
                throw new ArgumentNullException("deviceBatchId");
            }
        }

        /// <summary>
        /// Retrieves a customer's device operations.
        /// </summary>
        /// <param name="deviceId">The device id.</param>
        /// <returns>The customer's device operations.</returns>
        public IDevice this[string deviceId]
        {
            get
            {
                return this.ById(deviceId);
            }
        }

        /// <summary>
        /// Retrieves a customer's device operations.
        /// </summary>
        /// <param name="deviceId">The device id.</param>
        /// <returns>The customer's device operations.</returns>
        public IDevice ById(string deviceId)
        {
            return new DeviceOperations(this.Partner, this.Context.Item1, this.Context.Item2, deviceId);
        }

        /// <summary>
        /// Retrieves devices associated to the customer.
        /// </summary>
        /// <returns>A collection of devices.</returns>
        public ResourceCollection<Device> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Device>>(this.GetAsync);
        }

        /// <summary>
        /// Retrieves devices associated to the customer asynchronously.
        /// </summary>
        /// <returns>A collection of devices.</returns>
        public async Task<ResourceCollection<Device>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Device, ResourceCollection<Device>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetDevices.Path, this.Context.Item1, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Adds devices to existing devices batch.
        /// </summary>
        /// <param name="newDevices">The new devices to be created.</param>
        /// <returns>The location which indicates the URL of the API to query for status of the create request.</returns>
        public string Create(IEnumerable<Device> newDevices)
        {
            return PartnerService.Instance.SynchronousExecute<string>(() => this.CreateAsync(newDevices));
        }

        /// <summary>
        /// Asynchronously adds devices to existing devices batch.
        /// </summary>
        /// <param name="newDevices">The new devices to be created.</param>
        /// <returns>The location which indicates the URL of the API to query for status of the create request.</returns>
        public async Task<string> CreateAsync(IEnumerable<Device> newDevices)
        {
            if (newDevices == null)
            {
                throw new ArgumentNullException(nameof(newDevices));
            }

            var partnerServiceProxy = new PartnerServiceProxy<IEnumerable<Device>, System.Net.Http.HttpResponseMessage>(
              this.Partner,
              string.Format(PartnerService.Instance.Configuration.Apis.AddDevicestoDeviceBatch.Path, this.Context.Item1, this.Context.Item2));

            var response = await partnerServiceProxy.PostAsync(newDevices);

            return response.Headers.Location != null ? response.Headers.Location.ToString() : string.Empty;
        }
    }
}
