// -----------------------------------------------------------------------
// <copyright file="AzureUtilizationCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Utilization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Query;
    using Models.Utilizations;
    using Network;
    
    /// <summary>
    /// Groups behavior related to Azure subscription utilization records.
    /// </summary>
    internal class AzureUtilizationCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IAzureUtilizationCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureUtilizationCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription Id.</param>
        public AzureUtilizationCollectionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId) :
            base(rootPartnerOperations, new Tuple<string, string>(customerId, subscriptionId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set");
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("subscriptionId must be set");
            }
        }

        /// <summary>
        /// Retrieves utilization records for the Azure subscription.
        /// </summary>
        /// <param name="startTime">The starting time of when the utilization was metered in the billing system.</param>
        /// <param name="endTime">The ending time of when the utilization was metered in the billing system.</param>
        /// <param name="granularity">The resource usage time granularity. Can either be daily or hourly. Default is daily.</param>
        /// <param name="showDetails">If set to true, the utilization records will be split by the resource instance levels. If set to false, the utilization records
        /// will be aggregated on the resource level. Default is true.</param>
        /// <param name="size">An optional maximum number of records to return. Default is 1000. The returned resource collection will specify a next link in case there
        /// were more utilization records available.</param>
        /// <returns>The Azure resource utilization for the subscription.</returns>
        public ResourceCollection<AzureUtilizationRecord> Query(
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            AzureUtilizationGranularity granularity = AzureUtilizationGranularity.Daily,
            bool showDetails = true,
            int? size = null)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.QueryAsync(startTime, endTime, granularity, showDetails, size));
        }

        /// <summary>
        /// Asynchronously retrieves utilization records for the Azure subscription.
        /// </summary>
        /// <param name="startTime">The starting time of when the utilization was metered in the billing system.</param>
        /// <param name="endTime">The ending time of when the utilization was metered in the billing system.</param>
        /// <param name="granularity">The resource usage time granularity. Can either be daily or hourly. Default is daily.</param>
        /// <param name="showDetails">If set to true, the utilization records will be split by the resource instance levels. If set to false, the utilization records
        /// will be aggregated on the resource level. Default is true.</param>
        /// <param name="size">An optional maximum number of records to return. The returned resource collection will specify a next link in case there
        /// were more utilization records available.</param>
        /// <returns>A task that returns the Azure resource utilization for the subscription.</returns>
        public async Task<ResourceCollection<AzureUtilizationRecord>> QueryAsync(
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            AzureUtilizationGranularity granularity = AzureUtilizationGranularity.Daily,
            bool showDetails = true,
            int? size = null)
        {
            var partnerServiceProxy = new PartnerServiceProxy<AzureUtilizationRecord, ResourceCollection<AzureUtilizationRecord>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetAzureUtilizationRecords.Path, this.Context.Item1, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetAzureUtilizationRecords.Parameters.StartTime,
                    startTime.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture)));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetAzureUtilizationRecords.Parameters.EndTime,
                    endTime.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture)));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetAzureUtilizationRecords.Parameters.Granularity,
                    granularity.ToString()));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetAzureUtilizationRecords.Parameters.ShowDetails,
                    showDetails.ToString()));

            if (size.HasValue)
            {
                partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetAzureUtilizationRecords.Parameters.Size,
                    size.Value.ToString()));
            }

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Seeks pages of of utilization for resources that belong to an Azure subscription owned by a customer of the partner.
        /// </summary>
        /// <param name="continuationToken">The continuation token from the previous results.</param>
        /// <param name="seekOperation">The seek operation to perform. Next is only supported.</param>
        /// <returns>The next page of utilization records.</returns>
        public ResourceCollection<AzureUtilizationRecord> Seek(string continuationToken, SeekOperation seekOperation)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.SeekAsync(continuationToken, seekOperation));
        }

        /// <summary>
        /// Asynchronously seeks pages of utilization for resources that belong to an Azure subscription owned by a customer of the partner.
        /// </summary>
        /// <param name="continuationToken">The continuation token from the previous results.</param>
        /// <param name="seekOperation">The seek operation to perform. Next is only supported.</param>
        /// <returns>A task that returns the next page of utilization records.</returns>
        public async Task<ResourceCollection<AzureUtilizationRecord>> SeekAsync(string continuationToken, SeekOperation seekOperation = SeekOperation.Next)
        {
            if (string.IsNullOrWhiteSpace(continuationToken))
            {
                throw new ArgumentException("continuationToken must be non empty");
            }

            var partnerServiceProxy = new PartnerServiceProxy<AzureUtilizationRecord, ResourceCollection<AzureUtilizationRecord>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.SeekAzureUtilizationRecords.Path, this.Context.Item1, this.Context.Item2));

            partnerServiceProxy.AdditionalRequestHeaders.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.SeekAzureUtilizationRecords.AdditionalHeaders.ContinuationToken,
                continuationToken));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.SeekAzureUtilizationRecords.Parameters.SeekOperation,
                seekOperation.ToString()));

            return await partnerServiceProxy.GetAsync();
        }
    }
}