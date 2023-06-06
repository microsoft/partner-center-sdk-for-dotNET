// -----------------------------------------------------------------------
// <copyright file="ServiceIncidentCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.ServiceIncidents
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Query;
    using Models.ServiceIncidents;

    /// <summary>
    /// Showcases service incidents operations.
    /// </summary>
    internal class ServiceIncidentCollectionOperations : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Service Incidents"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // Query service incidents based on their active status - resolved or not. resolved = false fetches all the active incidents for all subscribed services. 
            const string SearchTerm = "false";
            ResourceCollection<ServiceIncidents> serviceIncidents = partnerOperations.ServiceIncidents.Query(QueryFactory.Instance.BuildIndexedQuery(1, 0, new SimpleFieldFilter(ServiceIncidentSearchField.Resolved.ToString(), FieldFilterOperation.Equals, SearchTerm)));

            foreach (var serviceIncident in serviceIncidents.Items)
            {
                Console.Out.WriteLine("Workload: {0}", serviceIncident.Workload);
                Console.Out.WriteLine("Status: {0}", serviceIncident.Status);
                Console.WriteLine();
            }
        }
    }
}
