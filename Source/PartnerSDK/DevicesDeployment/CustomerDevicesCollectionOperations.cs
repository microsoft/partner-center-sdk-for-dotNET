// -----------------------------------------------------------------------
// <copyright file="CustomerDevicesCollectionOperations.cs" company="Microsoft Corporation">
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
    internal class CustomerDevicesCollectionOperations : BasePartnerComponent, ICustomerDeviceCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDevicesCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public CustomerDevicesCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException(nameof(customerId));
            }
        }

        /// <summary>
        /// Updates devices with configuration policies.
        /// </summary>
        /// <param name="devicePolicyUpdateRequest">The device policy update request with devices to be updated.</param>
        /// <returns>The location which indicates the URL of the API to query for the status of update request.</returns>
        public string Update(DevicePolicyUpdateRequest devicePolicyUpdateRequest)
        {
            return PartnerService.Instance.SynchronousExecute<string>(() => this.UpdateAsync(devicePolicyUpdateRequest));
        }

        /// <summary>
        /// Asynchronously update devices with configuration policies.
        /// </summary>
        /// <param name="devicePolicyUpdateRequest">The device policy update request with devices to be updated.</param>
        /// <returns>The location which indicates the URL of the API to query for the status of update request.</returns>
        public async Task<string> UpdateAsync(DevicePolicyUpdateRequest devicePolicyUpdateRequest)
        {
            if (devicePolicyUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(devicePolicyUpdateRequest));
            }

            var partnerServiceProxy = new PartnerServiceProxy<DevicePolicyUpdateRequest, System.Net.Http.HttpResponseMessage>(
              this.Partner,
              string.Format(PartnerService.Instance.Configuration.Apis.UpdateDevicesWithPolicies.Path, this.Context));

            var response = await partnerServiceProxy.PatchAsync(devicePolicyUpdateRequest);

            return response.Headers.Location != null ? response.Headers.Location.ToString() : string.Empty;
        }
    }
}
