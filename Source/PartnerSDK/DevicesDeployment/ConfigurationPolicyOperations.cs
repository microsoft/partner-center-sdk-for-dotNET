// -----------------------------------------------------------------------
// <copyright file="ConfigurationPolicyOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using System;
    using System.Threading.Tasks;
    using Models.DevicesDeployment;
    using Network;

    /// <summary>
    /// Implements operations that apply to configuration policy. 
    /// </summary>
    internal class ConfigurationPolicyOperations : BasePartnerComponent<Tuple<string, string>>, IConfigurationPolicy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationPolicyOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="policyId">Policy Id.</param>
        public ConfigurationPolicyOperations(IPartner rootPartnerOperations, string customerId, string policyId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, policyId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(policyId))
            {
                throw new ArgumentNullException(nameof(policyId));
            }
        }

        /// <summary>
        /// Gets the policy details.
        /// </summary>
        /// <returns>The policy retrieved by policy Id under a particular customer.</returns>
        public ConfigurationPolicy Get()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync());
        }

        /// <summary>
        /// Gets the policy details asynchronously.
        /// </summary>
        /// <returns>The policy retrieved by policy Id under a particular customer.</returns>
        public async Task<ConfigurationPolicy> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetConfigurationPolicy.Path, this.Context.Item1, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Updates a configuration policy.
        /// </summary>
        /// <param name="policy">Payload of the update request.</param>
        /// <returns>Updated configuration policy.</returns>
        public ConfigurationPolicy Patch(ConfigurationPolicy policy)
        {
            return PartnerService.Instance.SynchronousExecute<ConfigurationPolicy>(() => this.PatchAsync(policy));
        }

        /// <summary>
        /// Updates a configuration policy asynchronously.
        /// </summary>
        /// <param name="policy">Payload of the update request.</param>
        /// <returns>Updated configuration policy.</returns>
        public async Task<ConfigurationPolicy> PatchAsync(ConfigurationPolicy policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            var partnerServiceProxy = new PartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.UpdateConfigurationPolicy.Path, this.Context.Item1, this.Context.Item2));

            return await partnerServiceProxy.PutAsync(policy);
        }

        /// <summary>
        /// Deletes a configuration policy.
        /// </summary>
        public void Delete()
        {
            PartnerService.Instance.SynchronousExecute(() => this.DeleteAsync());
        }

        /// <summary>
        /// Deletes a configuration policy asynchronously.
        /// </summary>
        /// <returns>An operation that completes when the delete is complete.</returns>
        public async Task DeleteAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.UpdateConfigurationPolicy.Path, this.Context.Item1, this.Context.Item2));

            await partnerServiceProxy.DeleteAsync();
        }
    }
}
