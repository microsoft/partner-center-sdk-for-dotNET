// -----------------------------------------------------------------------
// <copyright file="IUsageRecordByResourceCollection.cs" company="Microsoft Corporation">
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
    /// Defines the behavior for a subscription's resource usage records.
    /// </summary>
    public interface IUsageRecordByResourceCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<ResourceUsageRecord, ResourceCollection<ResourceUsageRecord>>
    {
        /// <summary>
        /// Retrieves the subscription's resource usage records.
        /// </summary>
        /// <returns>The subscription's resource usage records.</returns>
        new ResourceCollection<ResourceUsageRecord> Get();

        /// <summary>
        /// Asynchronously retrieves the subscription's resource usage records.
        /// </summary>
        /// <returns>The subscription's resource usage records.</returns>
        new Task<ResourceCollection<ResourceUsageRecord>> GetAsync();
    }
}
