// <copyright file="GetBatchUploadStatus.cs" company="Microsoft Corporation">
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
    /// Showcases retrieval of batch upload status.
    /// </summary>
    internal class GetBatchUploadStatus : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Get Batch Upload Status.";
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
            string trackingId = state[FeatureSamplesApplication.TrackingIdForDeviceDeployment] as string;

            var deviceBatchUploadStatus = partnerOperations.Customers.ById(customerId).BatchUploadStatus.ById(trackingId).Get();         

            Console.WriteLine("Status of the task is {0} with each the device status as follows", deviceBatchUploadStatus.Status);
            
            foreach (var deviceStatus in deviceBatchUploadStatus.DevicesStatus)
            {
                Console.Out.WriteLine("Serial Number: {0}", deviceStatus.SerialNumber);
                Console.Out.WriteLine("Status: {0}", deviceStatus.Status);
                Console.Out.WriteLine("Error : {0}", deviceStatus.ErrorDescription);
                Console.WriteLine();
            }
        }
    }
}
