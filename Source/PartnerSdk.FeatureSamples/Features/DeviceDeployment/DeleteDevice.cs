// <copyright file="DeleteDevice.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.DeviceDeployment
{
    using System;
    using System.Collections.Generic;
    using Models.Customers;
  
    /// <summary>
    /// Showcases deletion of device.
    /// </summary>
    internal class DeleteDevice : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Delete a device";
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
         
            partnerOperations.Customers.ById(customerId).DeviceBatches.ById(deviceBatchId).Devices.ById(deviceId).Delete();

            Console.WriteLine("Successfully deleted device details: ");
            Console.Out.WriteLine("ID: {0}", deviceId);
            Console.WriteLine();
        }
    }
}