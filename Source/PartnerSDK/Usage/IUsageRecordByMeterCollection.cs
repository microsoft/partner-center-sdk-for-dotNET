// -----------------------------------------------------------------------
// <copyright file="IUsageRecordByMeterCollection.cs" company="Microsoft Corporation">
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
    /// Defines the behavior for a subscription's meter usage records.
    /// </summary>
    public interface IUsageRecordByMeterCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<MeterUsageRecord, ResourceCollection<MeterUsageRecord>>
    {
        /// <summary>
        /// Retrieves the subscription's meter usage records.
        /// </summary>
        /// <returns>The subscription's meter usage records.</returns>
        new ResourceCollection<MeterUsageRecord> Get();

        /// <summary>
        /// Asynchronously retrieves the subscription's meter usage records.
        /// </summary>
        /// <returns>The subscription's meter usage records.</returns>
        new Task<ResourceCollection<MeterUsageRecord>> GetAsync();
    }
}
