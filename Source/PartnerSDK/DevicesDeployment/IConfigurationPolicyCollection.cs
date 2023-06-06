// -----------------------------------------------------------------------
// <copyright file="IConfigurationPolicyCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.DevicesDeployment;
    using Models.Query;

    /// <summary>
    /// Represents the operations that can be done on the partner's configuration policies.
    /// </summary>
    public interface IConfigurationPolicyCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<ConfigurationPolicy, ResourceCollection<ConfigurationPolicy>>, IEntityCreateOperations<ConfigurationPolicy>, IEntitySelector<IConfigurationPolicy>
    {
        /// <summary>
        /// Retrieves configuration policy behavior.
        /// </summary>
        /// <param name="policyId">The policy id.</param>
        /// <returns>The configuration policy behavior.</returns>
        new IConfigurationPolicy this[string policyId] { get; }

        /// <summary>
        /// Retrieves configuration policy behavior.
        /// </summary>
        /// <param name="policyId">The policy id.</param>
        /// <returns>The configuration policy behavior.</returns>
        new IConfigurationPolicy ById(string policyId);

        /// <summary>
        /// Retrieves all configuration policies.
        /// </summary>
        /// <returns>The collection of configuration policies.</returns>
        new ResourceCollection<ConfigurationPolicy> Get();

        /// <summary>
        /// Asynchronously retrieves all configuration policies.
        /// </summary>
        /// <returns>The collection of configuration policies.</returns>
        new Task<ResourceCollection<ConfigurationPolicy>> GetAsync();
    }
}
