// <copyright file="CreateDeviceBatch.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.DeviceDeployment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Models.Customers;
    using Models.DevicesDeployment;

    /// <summary>
    /// Showcases creation of devices batch with devices.
    /// </summary>
    internal class CreateDeviceBatch : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Create a devices batch with devices";
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
            List<Device> devicesToBeUploaded = new List<Device>();

            devicesToBeUploaded.Add(new Device
            {
                HardwareHash = "DummyHash123",
                ProductKey = "00329-00000-0003-AA606",
                SerialNumber = "1R9-ZNP67"
            });

            DeviceBatchCreationRequest newDeviceBatch = new DeviceBatchCreationRequest();
            newDeviceBatch.BatchId = deviceBatchId;
            newDeviceBatch.Devices = devicesToBeUploaded;

            partnerOperations.Customers.ById(customerId).DeviceBatches.Create(newDeviceBatch);

            Console.WriteLine("Successfully uploaded device details");
        }
    }
}