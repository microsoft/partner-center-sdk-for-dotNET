// <copyright file="PartnerLicensesUsageInsights.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Analytics
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Holds partner licenses usage insights operations.
    /// </summary>
    internal class PartnerLicensesUsageInsights : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Partner Licenses Usage Insights"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // get the partner's licenses usage insights collection.
            var insightsCollection = partnerOperations.Analytics.Licenses.Usage.Get();

            foreach (var insights in insightsCollection.Items)
            {
                Console.WriteLine("----------------------------------------");
                Console.Out.WriteLine("Service Name: {0}", insights.ServiceName);
                Console.Out.WriteLine("Channel Name: {0}", insights.Channel);
                Console.Out.WriteLine("Workload Name: {0}", insights.WorkloadName);
                Console.Out.WriteLine("Usage percent: {0}", insights.ProratedLicensesUsagePercent);                
            }
        }
    }
}
