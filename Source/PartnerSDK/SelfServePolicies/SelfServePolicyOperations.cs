// -----------------------------------------------------------------------
// <copyright file="SelfServePolicyOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.SelfServePolicies
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.SelfServePolicies;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// The Self Serve Policies collection implementation.
    /// </summary>
    internal class SelfServePolicyOperations : BasePartnerComponent<string>, ISelfServePolicy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfServePolicyOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="id">The id of the self serve policy.</param>
        public SelfServePolicyOperations(IPartner rootPartnerOperations, string id = null)
            : base(rootPartnerOperations, id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("id must be set.");
            }
        }

        /// <summary>
        /// Asynchronously updates a self serve policy.
        /// </summary>
        /// <param name="newEntity">self serve policy to add.</param>
        /// <returns>The self serve policy.</returns>
        public async Task<SelfServePolicy> PutAsync(SelfServePolicy newEntity)
        {
            ParameterValidator.Required(newEntity, "newEntity can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<SelfServePolicy, SelfServePolicy>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpdateSelfServePolicy.Path, this.Context));

            return await partnerApiServiceProxy.PutAsync(newEntity);
        }

        /// <summary>
        /// Updates a self serve policy.
        /// </summary>
        /// <param name="newEntity">self serve policy to add.</param>
        /// <returns>The self serve policy.</returns>
        public SelfServePolicy Put(SelfServePolicy newEntity)
        {
            ParameterValidator.Required(newEntity, "newEntity can't be null");

            return PartnerService.Instance.SynchronousExecute<SelfServePolicy>(() => this.PutAsync(newEntity));
        }

        /// <summary>
        /// Remove a self serve policye.
        /// </summary>
        public void Delete()
        {
            PartnerService.Instance.SynchronousExecute(() => this.DeleteAsync());
        }

        /// <summary>
        /// Asynchronously removes a self serve policy.
        /// </summary>
        /// <returns>A task when the operation is finished.</returns>
        public async Task DeleteAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<SelfServePolicy, SelfServePolicy>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.DeleteSelfServePolicy.Path, this.Context));

            await partnerApiServiceProxy.DeleteAsync();
        }
    }
}
