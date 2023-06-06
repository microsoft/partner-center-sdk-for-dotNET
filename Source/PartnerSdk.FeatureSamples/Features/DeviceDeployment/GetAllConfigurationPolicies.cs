// <copyright file="GetAllConfigurationPolicies.cs" company="Microsoft Corporation">
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
    /// Showcases the retrieval of all the configuration policies.
    /// </summary>
    internal class GetAllConfigurationPolicies : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Get all configuration policies. ";
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

            ResourceCollection<ConfigurationPolicy> configurationPolicies =
                partnerOperations.Customers.ById(customerId)
                    .ConfigurationPolicies.Get();

            Console.WriteLine("Retrieved configuration policies");
        
            foreach (var configurationPolicy in configurationPolicies.Items)
            {
                Console.Out.WriteLine("Id: {0}", configurationPolicy.Id);
                Console.Out.WriteLine("Name: {0}", configurationPolicy.Name);
                Console.Out.WriteLine("Description: {0}", configurationPolicy.Description);
                Console.Out.WriteLine("Creation Date: {0}", configurationPolicy.CreatedDate);
                Console.Out.WriteLine("Category: {0}", configurationPolicy.Category);
                Console.WriteLine();
            }
        }
    }
}
