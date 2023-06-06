// -----------------------------------------------------------------------
// <copyright file="IAuditRecordsCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.AuditRecords
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.Auditing;
    using Models.Query;

    /// <summary>
    /// Represents the operations that can be done on partners audit collection.
    /// </summary>
    public interface IAuditRecordsCollection : IPartnerComponent
    {
        /// <summary>
        /// Queries audit records associated to the partner.
        /// The following queries are supported:
        /// - Specify the number of audit record to return.
        /// - Filter the result with a customer name.
        /// </summary>
        /// <param name="startDate">The start date of the audit record logs.</param>
        /// <param name="endDate">The end date of the audit record logs.</param>
        /// <param name="query">The query.</param>
        /// <returns>The audit records that match the given query.</returns>
        SeekBasedResourceCollection<AuditRecord> Query(DateTime startDate, DateTime? endDate = null, IQuery query = null);

        /// <summary>
        /// Asynchronously queries audit records associated to the partner.
        /// The following queries are supported:
        /// - Specify the number of audit record to return.
        /// - Filter the result with a customer name.
        /// </summary>
        /// <param name="startDate">The start date of the audit record logs.</param>
        /// <param name="endDate">The end date of the audit record logs.</param>
        /// <param name="query">The query.</param>
        /// <returns>The audit records that match the given query.</returns>
        Task<SeekBasedResourceCollection<AuditRecord>> QueryAsync(DateTime startDate, DateTime? endDate = null, IQuery query = null);
    }
}
