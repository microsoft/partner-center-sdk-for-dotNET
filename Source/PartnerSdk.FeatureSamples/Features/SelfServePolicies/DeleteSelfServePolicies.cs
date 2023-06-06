// -----------------------------------------------------------------------
// <copyright file="DeleteSelfServePolicies.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.SelfServePolicies;
    using Microsoft.Store.PartnerCenter.SelfServePolicies;

    /// <summary>
    /// Test delete self serve policies scenario
    /// </summary>
    internal class DeleteSelfServePolicies : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Delete Self Serve Policies"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var defaultCustomerId = ConfigurationManager.AppSettings["defaultCustomerId"];

            // get the self serve polies
            ResourceCollection<SelfServePolicy> selfServePolicies = partnerOperations.SelfServePolicies.Get(defaultCustomerId);

            // If policies do not exist, we need to create one
            if (!selfServePolicies.Items.Any())
            {
                var defaultPartnerTenantId = ConfigurationManager.AppSettings["rIAccountPartnerTenantId"];
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

                // Create polisy
                SelfServePolicy createdSelfServePolicy = partnerOperations.SelfServePolicies.Create(selfServePolicy);
            }

            selfServePolicies = partnerOperations.SelfServePolicies.Get(defaultCustomerId);
            Console.Out.WriteLine("Existing Customer policy count before delete: {0}", selfServePolicies.Items.Count());
            partnerOperations.SelfServePolicies.ById(selfServePolicies.Items.FirstOrDefault().Id).Delete();

            selfServePolicies = partnerOperations.SelfServePolicies.Get(defaultCustomerId);
            Console.Out.WriteLine("Customer policy count after delete: {0}", selfServePolicies.Items.Count());
        }
    }
}
