// -----------------------------------------------------------------------
// <copyright file="AuditRecordsCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.AuditRecords
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Auditing;
    using Models.JsonConverters;
    using Models.Query;
    using Network;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>
    /// Represents the operations that can be performed on a partner's audit records.
    /// </summary>
    internal class AuditRecordsCollection : BasePartnerComponent, IAuditRecordsCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditRecordsCollection"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public AuditRecordsCollection(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

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
        public SeekBasedResourceCollection<AuditRecord> Query(DateTime startDate, DateTime? endDate = null, IQuery query = null)
        {
            return PartnerService.Instance.SynchronousExecute<SeekBasedResourceCollection<AuditRecord>>(() => this.QueryAsync(startDate, endDate, query));
        }

        /// <summary>
        /// Asynchronously audit records invoices associated to the partner.
        /// The following queries are supported:
        /// - Specify the number of audit record to return.
        /// - Filter the result with a customer name.
        /// </summary>
        /// <param name="startDate">The start date of the audit record logs.</param>
        /// <param name="endDate">The end date of the audit record logs.</param>
        /// <param name="query">The query.</param>
        /// <returns>The audit records that match the given query.</returns>
        public async Task<SeekBasedResourceCollection<AuditRecord>> QueryAsync(DateTime startDate, DateTime? endDate = null, IQuery query = null)
        {
            if (query != null && query.Type != QueryType.Indexed && query.Type != QueryType.Simple)
            {
                throw new ArgumentException("This type of query is not supported.");
            }

        var partnerServiceProxy = new PartnerServiceProxy<AuditRecord, SeekBasedResourceCollection<AuditRecord>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetAuditRecordsRequest.Path,
                jsonConverter: new ResourceCollectionConverter<AuditRecord>());

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetAuditRecordsRequest.Parameters.StartDate,
                startDate.ToString(CultureInfo.InvariantCulture)));

            if (endDate.HasValue)
            {
                partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetAuditRecordsRequest.Parameters.EndDate,
                    endDate.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (query != null)
            {
                if (query.Type == QueryType.Indexed)
                {
                    partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetAuditRecordsRequest.Parameters.Size,
                        query.PageSize.ToString()));
                }

                if (query.Filter != null)
                {
                    partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetAuditRecordsRequest.Parameters.Filter, JsonConvert.SerializeObject(query.Filter)));
                }

                if (query.Token != null)
                {
                    partnerServiceProxy.AdditionalRequestHeaders.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetAuditRecordsRequest.AdditionalHeaders.ContinuationToken, query.Token.ToString()));
                }
            }

            return await partnerServiceProxy.GetAsync();
        }
    }
}
