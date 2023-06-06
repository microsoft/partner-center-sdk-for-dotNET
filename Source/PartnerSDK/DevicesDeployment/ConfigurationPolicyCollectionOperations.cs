// -----------------------------------------------------------------------
// <copyright file="ConfigurationPolicyCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.DevicesDeployment;
    using Network;

    /// <summary>
    /// Implements operations that apply to configuration policy collections.  
    /// </summary>
    internal class ConfigurationPolicyCollectionOperations : BasePartnerComponent, IConfigurationPolicyCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationPolicyCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public ConfigurationPolicyCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException(nameof(customerId));
            }
        }

        /// <summary>
        /// Retrieves configuration policy behavior.
        /// </summary>
        /// <param name="policyId">The policy id.</param>
        /// <returns>The customer configuration policy behavior.</returns>
        public IConfigurationPolicy this[string policyId]
        {
            get
            {
                return this.ById(policyId);
            }
        }

        /// <summary>
        /// Retrieves configuration policy behavior.
        /// </summary>
        /// <param name="policyId">The policy id.</param>
        /// <returns>The customer configuration policy.</returns>
        public IConfigurationPolicy ById(string policyId)
        {
            return new ConfigurationPolicyOperations(this.Partner, this.Context, policyId);
        }

        /// <summary>
        /// Retrieves a collection of configuration policies associated to the customer.
        /// </summary>
        /// <returns>A collection of configuration policies. </returns>
        public ResourceCollection<ConfigurationPolicy> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<ConfigurationPolicy>>(this.GetAsync);
        }

        /// <summary>
        /// Retrieves a collection of configuration policies associated to the customer asynchronously.
        /// </summary>
        /// <returns>A collection of configuration policies. </returns>
        public async Task<ResourceCollection<ConfigurationPolicy>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ConfigurationPolicy, ResourceCollection<ConfigurationPolicy>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetConfigurationPolicies.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Creates a new configuration policy.
        /// </summary>
        /// <param name="newPolicy">The new configuration policy information.</param>
        /// <returns>The policy information that was just created.</returns>
        public ConfigurationPolicy Create(ConfigurationPolicy newPolicy)
        {
            return PartnerService.Instance.SynchronousExecute<ConfigurationPolicy>(() => this.CreateAsync(newPolicy));
        }

        /// <summary>
        /// Asynchronously creates a new configuration policy.
        /// </summary>
        /// <param name="newPolicy">The new configuration policy information.</param>
        /// <returns>The policy information that was just created.</returns>
        public async Task<ConfigurationPolicy> CreateAsync(ConfigurationPolicy newPolicy)
        {
            if (newPolicy == null)
            {
                throw new ArgumentNullException("newPolicy");
            }

            var partnerServiceProxy = new PartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.CreateConfigurationPolicy.Path, this.Context));

            return await partnerServiceProxy.PostAsync(newPolicy);
        }
    }
}
