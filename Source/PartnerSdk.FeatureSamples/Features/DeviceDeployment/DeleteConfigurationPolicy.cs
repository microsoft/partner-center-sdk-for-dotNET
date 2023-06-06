// <copyright file="DeleteConfigurationPolicy.cs" company="Microsoft Corporation">
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
    /// Showcases delete configuration policy functionality.
    /// </summary>
    internal class DeleteConfigurationPolicy : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Delete a given configuration policy";
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
            string policyId = state[FeatureSamplesApplication.CustomerConfigurationPolicyIdForDelete] as string;

            Console.Out.WriteLine("Policy to be deleted: {0} ", policyId);

            partnerOperations.Customers.ById(customerId).ConfigurationPolicies.ById(policyId).Delete();

            Console.Out.WriteLine("Successfully deleted policy: {0} ", policyId);
            Console.WriteLine();
        }
    }
}
