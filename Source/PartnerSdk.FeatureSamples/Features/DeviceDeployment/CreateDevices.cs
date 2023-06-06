// <copyright file="CreateDevices.cs" company="Microsoft Corporation">
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
    /// Showcases creation of collection of devices under an existing devices batch.
    /// </summary>
    internal class CreateDevices : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Create a collection of device";
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
                HardwareHash = "DummyHash1234",
                ProductKey = "00329-00000-0003-AA606",
                SerialNumber = "2R9-ZNP67"
            });

            devicesToBeUploaded.Add(new Device
            {
                HardwareHash = "DummyHash12345",
                ProductKey = "00329-00000-0003-AA606",
                SerialNumber = "2R9-ZNP67"
            });

            string trackingLocation = partnerOperations.Customers.ById(customerId).DeviceBatches.ById(deviceBatchId).Devices.Create(devicesToBeUploaded);

            Console.WriteLine("Successfully uploaded device details " + trackingLocation);
        }
    }
}