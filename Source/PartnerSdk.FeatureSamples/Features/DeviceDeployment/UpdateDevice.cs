// <copyright file="UpdateDevice.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.DeviceDeployment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.DevicesDeployment;

    /// <summary>
    /// Showcases update of devices with configuration policy.
    /// </summary>
    internal class UpdateDevice : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Update a device with configuration policy.";
            }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            string customerId = state[FeatureSamplesApplication.CustomerForDeviceDeployment] as string;
            string deviceBatchId = state[FeatureSamplesApplication.CustomerDeviceBatchId] as string;
            string deviceId = state[FeatureSamplesApplication.CustomerDeviceId] as string;
            string configurationPolicyId = state[FeatureSamplesApplication.CustomerConfigurationPolicyId] as string;

            Device device = new Device
            {
                Id = deviceId,
            };

            DevicePolicyUpdateRequest devicePolicyUpdateRequest = new DevicePolicyUpdateRequest
            {
                Devices = Enumerable.Repeat(device, 1)
            };

            List<KeyValuePair<PolicyCategory, string>> policyToBeAdded = new List<KeyValuePair<PolicyCategory, string>>
            {
                new KeyValuePair<PolicyCategory, string>(PolicyCategory.OOBE, configurationPolicyId)
            };
            device.Policies = policyToBeAdded;

            partnerOperations.Customers.ById(customerId).DevicePolicy.Update(devicePolicyUpdateRequest);

            Console.WriteLine("Updated devices Done");
            Console.WriteLine();
        }
    }
}