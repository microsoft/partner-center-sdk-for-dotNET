// -----------------------------------------------------------------------
// <copyright file="ICustomerUsageRecordCollection.cs" company="Microsoft Corporation">
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
    /// Defines the behavior of a customer usage record collection operations.
    /// </summary>
    public interface ICustomerUsageRecordCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<CustomerMonthlyUsageRecord, ResourceCollection<CustomerMonthlyUsageRecord>>
    {
        /// <summary>
        /// Retrieves all customer usage records.
        /// </summary>
        /// <returns>The customer usage records.</returns>
        new ResourceCollection<CustomerMonthlyUsageRecord> Get();

        /// <summary>
        /// Asynchronously retrieves all customer usage records.
        /// </summary>
        /// <returns>The customer usage records.</returns>
        new Task<ResourceCollection<CustomerMonthlyUsageRecord>> GetAsync();
    }
}