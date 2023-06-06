// -----------------------------------------------------------------------
// <copyright file="ISubscriptionUsageSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Usage;

    /// <summary>
    /// This interface defines the operations available on a customer's subscription usage summary.
    /// </summary>
    public interface ISubscriptionUsageSummary : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<SubscriptionUsageSummary>
    {
        /// <summary>
        /// Retrieves the customer's subscription usage summary.
        /// </summary>
        /// <returns>The customer's subscription usage summary.</returns>
        new SubscriptionUsageSummary Get();

        /// <summary>
        /// Asynchronously retrieves the customer's subscription usage summary.
        /// </summary>
        /// <returns>The customer's subscription usage summary.</returns>
        new Task<SubscriptionUsageSummary> GetAsync();
    }
}
