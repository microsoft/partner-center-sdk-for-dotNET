// <copyright file="IResourceUsageRecordCollection.cs" company="Microsoft Corporation">
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
    public interface IResourceUsageRecordCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<AzureResourceMonthlyUsageRecord, ResourceCollection<AzureResourceMonthlyUsageRecord>>
    {
        /// <summary>
        /// Retrieves the subscription's resource usage records.
        /// </summary>
        /// <returns>The subscription's resource usage records.</returns>
        new ResourceCollection<AzureResourceMonthlyUsageRecord> Get();

        /// <summary>
        /// Asynchronously retrieves the subscription's resource usage records.
        /// </summary>
        /// <returns>The subscription's resource usage records.</returns>
        new Task<ResourceCollection<AzureResourceMonthlyUsageRecord>> GetAsync();
    }
}
