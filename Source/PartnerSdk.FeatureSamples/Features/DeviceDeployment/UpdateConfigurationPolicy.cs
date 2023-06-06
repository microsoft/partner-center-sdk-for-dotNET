// <copyright file="UpdateConfigurationPolicy.cs" company="Microsoft Corporation">
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
    /// Showcases update configuration policy functionality.
    /// </summary>
    internal class UpdateConfigurationPolicy : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Update a given configuration policy";
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
            string policyId = state[FeatureSamplesApplication.CustomerConfigurationPolicyId] as string;

            ConfigurationPolicy configPolicyToBeUpdated = new ConfigurationPolicy()
            {
                Name = "profile2",
                Id = policyId,
                PolicySettings = new List<PolicySettingsType>() { PolicySettingsType.OobeUserNotLocalAdmin, PolicySettingsType.RemoveOemPreinstalls }
            };

            Console.Out.WriteLine("Policy to be updated: ");
            Console.Out.WriteLine("Id: {0}", configPolicyToBeUpdated.Id);
            Console.Out.WriteLine("Name: {0}", configPolicyToBeUpdated.Name);
            Console.Out.WriteLine("Policy settings: ");

            foreach (var setting in configPolicyToBeUpdated.PolicySettings)
            {
                Console.Out.WriteLine("{0}", setting);
            }

            Console.WriteLine();

            ConfigurationPolicy updatedConfigurationPolicy = partnerOperations.Customers.ById(customerId).ConfigurationPolicies.ById(policyId).Patch(configPolicyToBeUpdated);

            Console.Out.WriteLine("Updated policy: ");
            Console.Out.WriteLine("Id: {0}", updatedConfigurationPolicy.Id);
            Console.Out.WriteLine("Name: {0}", updatedConfigurationPolicy.Name);
            foreach (var setting in updatedConfigurationPolicy.PolicySettings)
            {
                Console.Out.WriteLine("{0}", setting);
            }

            Console.WriteLine();
        }
    }
}
