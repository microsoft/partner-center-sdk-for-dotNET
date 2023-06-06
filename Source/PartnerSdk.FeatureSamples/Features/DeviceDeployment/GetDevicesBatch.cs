// <copyright file="GetDevicesBatch.cs" company="Microsoft Corporation">
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
    /// Showcases retrieval of collection of devices batches.
    /// </summary>
    internal class GetDevicesBatch : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Get All devices batches.";
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
      
            ResourceCollection<DeviceBatch> deviceCollections = partnerOperations.Customers.ById(customerId).DeviceBatches.Get();
         
            Console.WriteLine("Retrieved devices batches");

            foreach (var deviceCollection in deviceCollections.Items)
            {
                Console.Out.WriteLine("Id: {0}", deviceCollection.Id);
                Console.Out.WriteLine("Count of devices: {0}", deviceCollection.DevicesCount);
                Console.WriteLine();
            }
        }
    }
}
