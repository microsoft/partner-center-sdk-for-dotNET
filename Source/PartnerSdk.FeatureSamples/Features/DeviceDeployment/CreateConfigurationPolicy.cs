// <copyright file="CreateConfigurationPolicy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.DeviceDeployment
{
    using System;
    using System.Collections.Generic;
    using Models.Customers;
    using Models.DevicesDeployment;

    /// <summary>
    /// Showcases creation of configuration policy.
    /// </summary>
    internal class CreateConfigurationPolicy : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Create a configuration policy";
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

            ConfigurationPolicy configPolicy = new ConfigurationPolicy()
            {
                Name = "Sample policy 1",
                Description = "testing create policy through SDK",
                PolicySettings = new List<PolicySettingsType>() { PolicySettingsType.OobeUserNotLocalAdmin, PolicySettingsType.RemoveOemPreinstalls }
            };

            Console.WriteLine("Creating a new policy with the following values: ");
            Console.Out.WriteLine("Name: {0}", configPolicy.Name);
            Console.Out.WriteLine("Description: {0}", configPolicy.Description);
            Console.Out.WriteLine("Settings: ");
            foreach (var settings in configPolicy.PolicySettings)
            {
                Console.Out.WriteLine("{0}", settings);
            }

            Console.WriteLine();
            ConfigurationPolicy createdConfigPolicy = partnerOperations.Customers.ById(customerId).ConfigurationPolicies.Create(configPolicy);

            Console.WriteLine("Policy created details: ");
            Console.Out.WriteLine("Name: {0}", createdConfigPolicy.Name);
            Console.Out.WriteLine("Description: {0}", createdConfigPolicy.Description);
            Console.Out.WriteLine("Category: {0}", createdConfigPolicy.Category);
            Console.Out.WriteLine("Settings: ");
            Console.Out.WriteLine("Id: {0} ", createdConfigPolicy.Id);
            state[FeatureSamplesApplication.CustomerConfigurationPolicyIdForDelete] = createdConfigPolicy.Id;
            foreach (var settings in createdConfigPolicy.PolicySettings)
            {
                Console.Out.WriteLine("{0}", settings);
            }

            Console.WriteLine();
        }
    }
}