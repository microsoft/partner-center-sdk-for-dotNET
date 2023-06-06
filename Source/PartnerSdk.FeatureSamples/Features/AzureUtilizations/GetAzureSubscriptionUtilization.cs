// <copyright file="GetAzureSubscriptionUtilization.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.AzureUtilization
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Utilizations;

    /// <summary>
    /// Displays all azureUtilizationRecords for a certain customer.
    /// </summary>
    internal class GetAzureSubscriptionUtilization : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Azure Utilizations"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer and subscription from the application state
            // Note: Azure Utilization data is only available in Production environment. The below customer and subscription information need to be changed to a test production user and respective App.config values to be configured. Reading these values from console.
            Console.WriteLine("Enter Customer Id (Tip or production accounts only):");
            string selectedCustomerId = Console.ReadLine();

            Console.WriteLine("Enter Subscription Id (Tip or production accounts only):");
            string selectedSubscriptionId = Console.ReadLine();

            // get the azureUtilizationRecords for the customer and subscription
            ResourceCollection<AzureUtilizationRecord> azureUtilizationRecords = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(selectedSubscriptionId).Utilization.Azure.Query(
                DateTimeOffset.Now.AddYears(-1),
                DateTimeOffset.Now,
                size: 1000);

            // display the azureUtilizationRecords
            Console.Out.WriteLine("Azure Utilization Records count: " + azureUtilizationRecords.TotalCount);

            IList<AzureUtilizationRecord> listAzureUtilizationRecords = new List<AzureUtilizationRecord>(azureUtilizationRecords.Items);

            foreach (var azureUtilizationRecord in listAzureUtilizationRecords)
            {
                if (azureUtilizationRecord.InstanceData.AdditionalInfo != null)
                {
                    Console.Out.WriteLine("Resource.Id: {0}", azureUtilizationRecord.Resource.Id);
                    Console.Out.WriteLine("Resource.Name: {0}", azureUtilizationRecord.Resource.Name);
                    Console.Out.WriteLine("Resource.Unit: {0}", azureUtilizationRecord.Unit);
                    Console.Out.WriteLine("Resource.UsageStartTime: {0}", azureUtilizationRecord.UsageStartTime);
                    Console.Out.WriteLine("Resource.UsageEndTime: {0}", azureUtilizationRecord.UsageEndTime);
                    Console.Out.WriteLine("Resource.InstanceData.AdditionalInfo: {0}", azureUtilizationRecord.InstanceData.AdditionalInfo);
                    Console.Out.WriteLine("Resource.InstanceData.Tags: {0}", azureUtilizationRecord.InstanceData.Tags);
                    Console.Out.WriteLine();
                }
            }
        }
    }
}
