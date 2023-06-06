// -----------------------------------------------------------------------
// <copyright file="SelfServePoliciesCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.SelfServePolicies
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.SelfServePolicies;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// The Self Serve Policies collection implementation.
    /// </summary>
    internal class SelfServePoliciesCollectionOperations : BasePartnerComponent<string>, ISelfServePoliciesCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfServePoliciesCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="entityId">The entity id used to get policies.</param>
        public SelfServePoliciesCollectionOperations(IPartner rootPartnerOperations, string entityId = null)
            : base(rootPartnerOperations, entityId)
        {
        }

        /// <summary>
        /// Obtains a specific self serve policy.
        /// </summary>
        /// <param name="id">The self serve policy identifier.</param>
        /// <returns>The self serve policy operations.</returns>
        public ISelfServePolicy ById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("id must be set.");
            }

            return new SelfServePolicyOperations(this.Partner, id);
        }

        /// <summary>
        /// Retrieves all the self serve policies by entity id.
        /// </summary>
        /// <param name="entityId">The id of the entity.</param>
        /// <returns>The self serve policies.</returns>
        public async Task<ResourceCollection<SelfServePolicy>> GetAsync(string entityId)
        {
            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentException("entityId must be set.");
            }

            var partnerServiceProxy = new PartnerServiceProxy<SelfServePolicy, ResourceCollection<SelfServePolicy>>(
               this.Partner,
               PartnerService.Instance.Configuration.Apis.GetSelfServePolicies.Path);

            partnerServiceProxy.UriParameters.Add(
                new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetSelfServePolicies.Parameters.EntityId,
                    entityId));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Retrieves all the self serve policies by entity id.
        /// </summary>
        /// <param name="entityId">The id of the entity.</param>
        /// <returns>The self serve policies.</returns>
        public ResourceCollection<SelfServePolicy> Get(string entityId)
        {
            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentException("entityId must be set.");
            }

            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync(entityId));
        }

        /// <summary>
        /// Adds a self serve policy.
        /// </summary>
        /// <param name="newEntity">Self serve policy to add.</param>
        /// <returns>The self serve policy.</returns>
        public async Task<SelfServePolicy> CreateAsync(SelfServePolicy newEntity)
        {
            ParameterValidator.Required(newEntity, "newEntity can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<SelfServePolicy, SelfServePolicy>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CreateSelfServePolicy.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(newEntity);
        }

        /// <summary>
        /// Adds a self serve policy.
        /// </summary>
        /// <param name="newEntity">Self serve policy to add.</param>
        /// <returns>The self serve policy.</returns>
        public SelfServePolicy Create(SelfServePolicy newEntity)
        {
            ParameterValidator.Required(newEntity, "newEntity can't be null");
            return PartnerService.Instance.SynchronousExecute<SelfServePolicy>(() => this.CreateAsync(newEntity));
        }
    }
}
