// -----------------------------------------------------------------------
// <copyright file="RelationshipCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Relationships
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Models;
    using Models.Query;
    using Models.Relationships;
    using Network;
    using Newtonsoft.Json;

    /// <summary>
    /// The relationship collection implementation.
    /// </summary>
    internal class RelationshipCollectionOperations : BasePartnerComponent, IRelationshipCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelationshipCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public RelationshipCollectionOperations(IPartner rootPartnerOperations) 
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves all the partner relationships.
        /// </summary>
        /// <param name="partnerRelationshipType">The type of partner relationship.</param>
        /// <returns>The partner relationships.</returns>
        public ResourceCollection<PartnerRelationship> Get(PartnerRelationshipType partnerRelationshipType)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync(partnerRelationshipType));
        }

        /// <summary>
        /// Asynchronously retrieves all the partner relationships.
        /// </summary>
        /// <param name="partnerRelationshipType">The type of partner relationship.</param>
        /// <returns>The partner relationships.</returns>
        public async Task<ResourceCollection<PartnerRelationship>> GetAsync(PartnerRelationshipType partnerRelationshipType)
        {
            var partnerServiceProxy = new PartnerServiceProxy<PartnerRelationship, ResourceCollection<PartnerRelationship>>(
               this.Partner,
               PartnerService.Instance.Configuration.Apis.GetPartnerRelationships.Path);

            partnerServiceProxy.UriParameters.Add(
                new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetPartnerRelationships.Parameters.RelationshipType,
                    partnerRelationshipType.ToString()));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Queries partner relationships associated to the partner.
        /// - Only <see cref="SimpleQuery"/> with a <see cref="SimpleFieldFilter"/> is supported.
        /// - This query supports a <see cref="FieldFilterOperation.Substring"/> search of the partner. Check <see cref="PartnerRelationshipSearchField"/> for
        /// the list of supported search fields.
        /// </summary>
        /// <param name="partnerRelationshipType">The type of partner relationship.</param>
        /// <param name="query">A query to apply onto partner relationships. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The partner relationships that match the given query.</returns>
        public ResourceCollection<PartnerRelationship> Query(PartnerRelationshipType partnerRelationshipType, IQuery query)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.QueryAsync(partnerRelationshipType, query));
        }

        /// <summary>
        /// Asynchronously Queries partner relationships associated to the partner.
        /// - Only <see cref="SimpleQuery"/> with a <see cref="SimpleFieldFilter"/> is supported.
        /// - This query supports a <see cref="FieldFilterOperation.Substring"/> search of the partner. Check <see cref="PartnerRelationshipSearchField"/> for
        /// the list of supported search fields.
        /// </summary>
        /// <param name="partnerRelationshipType">The type of partner relationship.</param>
        /// <param name="query">A query to apply onto partner relationships. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The partner relationships that match the given query.</returns>
        public async Task<ResourceCollection<PartnerRelationship>> QueryAsync(PartnerRelationshipType partnerRelationshipType, IQuery query)
        {
            var partnerServiceProxy = new PartnerServiceProxy<PartnerRelationship, ResourceCollection<PartnerRelationship>>(
               this.Partner,
               PartnerService.Instance.Configuration.Apis.GetPartnerRelationships.Path);

            partnerServiceProxy.UriParameters.Add(
                new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetPartnerRelationships.Parameters.RelationshipType,
                    partnerRelationshipType.ToString()));

            if (query != null && query.Filter != null)
            {
                // add the filter to the request if specified
                partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetPartnerRelationships.Parameters.Filter, 
                    WebUtility.UrlEncode(JsonConvert.SerializeObject(query.Filter))));
            }

            return await partnerServiceProxy.GetAsync();
        }
    }
}
