// <copyright file="ISubscriptionDailyUsageRecordCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Usage;

    /// <summary>
    /// Defines the behavior for a subscription's daily usage records.
    /// </summary>
    public interface ISubscriptionDailyUsageRecordCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<SubscriptionDailyUsageRecord, ResourceCollection<SubscriptionDailyUsageRecord>>
    {
        /// <summary>
        /// Retrieves the subscription's daily usage records.
        /// </summary>
        /// <returns>The subscription's daily usage records.</returns>
        new ResourceCollection<SubscriptionDailyUsageRecord> Get();

        /// <summary>
        /// Asynchronously retrieves the subscription's daily usage records.
        /// </summary>
        /// <returns>The subscription's daily usage records.</returns>
        new Task<ResourceCollection<SubscriptionDailyUsageRecord>> GetAsync();
    }
}
