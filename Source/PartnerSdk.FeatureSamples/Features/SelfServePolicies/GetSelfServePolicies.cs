// -----------------------------------------------------------------------
// <copyright file="GetSelfServePolicies.cs" company="Microsoft Corporation">
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
    /// Test get self serve policies scenario
    /// </summary>
    internal class GetSelfServePolicies : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Self Serve Policies"; }
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

            if (selfServePolicies.Items.Any())
            {
                // currently there can be only one self serve policy per customer.
                var policy = selfServePolicies.Items.FirstOrDefault();

                Console.Out.WriteLine($"PolicyID: {policy.Id}");
                Console.Out.WriteLine($"ETag: {policy.Attributes.Etag}");
                Console.Out.WriteLine($"SelfServeEntity: entityType: {policy.SelfServeEntity.SelfServeEntityType} customerTenantID: {policy.SelfServeEntity.TenantID}");
                Console.Out.WriteLine($"Grantor: grantType: {policy.Grantor.GrantorType} partnerTenantiD: {policy.Grantor.TenantID}");

                // currently there can be only one permission per customer.
                var permission = policy.Permissions.FirstOrDefault();
                Console.Out.WriteLine($"Permission: action: {permission.Action} resource: {permission.Resource}");
            }
        }
    }
}
