// <copyright file="GetDevices.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.DeviceDeployment
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Customers;
    using Models.DevicesDeployment;

    /// <summary>
    /// Showcases the retrieval of device collection.
    /// </summary>
    internal class GetDevices : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Get All devices";
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

            ResourceCollection<Device> devices = partnerOperations.Customers.ById(customerId).DeviceBatches.ById(deviceBatchId).Devices.Get();

            Console.WriteLine("Retrieved devices");

            foreach (var device in devices.Items)
            {
                Console.Out.WriteLine("Id: {0}", device.Id);
                Console.Out.WriteLine("productKey: {0}", device.ProductKey);
                Console.Out.WriteLine("Serial number: {0}", device.SerialNumber);
                Console.Out.WriteLine("Model Name: {0}", device.ModelName);
                Console.Out.WriteLine("Manufacturer: {0}", device.OemManufacturerName);
                Console.Out.WriteLine("uploaded date: {0}", device.UploadedDate);
                Console.WriteLine();
            }
        }
    }
}
