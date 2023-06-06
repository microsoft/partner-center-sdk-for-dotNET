// <copyright file="ISubscriptionMonthlyUsageRecordCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Usage;

    /// <summary>
    /// Defines the behavior for a customer's subscription usage records.
    /// </summary>
    public interface ISubscriptionMonthlyUsageRecordCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<SubscriptionMonthlyUsageRecord, ResourceCollection<SubscriptionMonthlyUsageRecord>>
    {
        /// <summary>
        /// Retrieves the customer's subscription usage records.
        /// </summary>
        /// <returns>The customer's subscription usage records.</returns>
        new ResourceCollection<SubscriptionMonthlyUsageRecord> Get();

        /// <summary>
        /// Asynchronously retrieves the customer's subscription usage records.
        /// </summary>
        /// <returns>The customer's subscription usage records.</returns>
        new Task<ResourceCollection<SubscriptionMonthlyUsageRecord>> GetAsync();
    }
}
