// -----------------------------------------------------------------------
// <copyright file="CreateAndUpdateSelfServePolicies.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.SelfServePolicies
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.SelfServePolicies;
    using Microsoft.Store.PartnerCenter.SelfServePolicies;

    /// <summary>
    /// Test create self serve policies scenario
    /// </summary>
    internal class CreateAndUpdateSelfServePolicies : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Create and Update Self Serve Policies"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var defaultCustomerId = ConfigurationManager.AppSettings["defaultCustomerId"];
            var defaultPartnerTenantId = ConfigurationManager.AppSettings["rIAccountPartnerTenantId"];

            // get the self serve polies
            ResourceCollection<SelfServePolicy> selfServePolicies = partnerOperations.SelfServePolicies.Get(defaultCustomerId);

            // delete any existing policies
            if (selfServePolicies.Items.Any())
            {
                partnerOperations.SelfServePolicies.ById(selfServePolicies.Items.FirstOrDefault().Id).Delete();
            }

            var selfServePolicy = new SelfServePolicy
            {
                SelfServeEntity = new SelfServeEntity
                {
                    SelfServeEntityType = "customer",
                    TenantID = defaultCustomerId,
                },
                Grantor = new Grantor
                {
                    GrantorType = "billToPartner",
                    TenantID = defaultPartnerTenantId,
                },
                Permissions = new Permission[]
                {
                    new Permission
                    {
                    Action = "Purchase",
                    Resource = "AzureReservedInstances",
                    },
                },
            };

            // creates the self serve policy
            SelfServePolicy policy = partnerOperations.SelfServePolicies.Create(selfServePolicy);

            // perform an update operation
            policy.Permissions = policy.Permissions.Concat(new Permission[] { new Permission { Resource = "AzureSavingsPlan", Action = "Purchase" } }).ToArray();

            SelfServePolicy updatedPolicy = partnerOperations.SelfServePolicies.ById(policy.Id).Put(policy);

            Console.WriteLine(this.DisplaySelfServePolicy(updatedPolicy));
        }

        /// <summary>
        /// Displays the granted self serve policies for a given customer as a string.
        /// </summary>
        /// <param name="policy">The instance of <see cref="SelfServePolicy"/> for a given customer.</param>
        /// <returns>A string representing the granted self serve policies for a given customer.</returns>
        private string DisplaySelfServePolicy(SelfServePolicy policy)
        {
            var sb = new StringBuilder("\n");

            sb.AppendLine($"Self Serve Permissions for customer: {policy.SelfServeEntity.TenantID}");

            foreach (var perm in policy.Permissions)
            {
                sb.AppendLine($"Resource: {perm.Resource}");
                sb.AppendLine($"Action: {perm.Action}");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
