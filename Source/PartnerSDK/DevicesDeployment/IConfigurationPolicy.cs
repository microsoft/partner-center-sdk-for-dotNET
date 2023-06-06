// -----------------------------------------------------------------------
// <copyright file="IConfigurationPolicy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.DevicesDeployment;

    /// <summary>
    /// Represents all the operations that can be done on a configuration policy.
    /// </summary>
    public interface IConfigurationPolicy : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<ConfigurationPolicy>, IEntityPatchOperations<ConfigurationPolicy>, IEntityDeleteOperations<ConfigurationPolicy>
    {
        /// <summary>
        /// Retrieves the configuration policy. 
        /// </summary>
        /// <returns>The configuration policy. </returns>
        new ConfigurationPolicy Get();

        /// <summary>
        /// Retrieves the configuration policy asynchronously. 
        /// </summary>
        /// <returns>The configuration policy. </returns>
        new Task<ConfigurationPolicy> GetAsync();

        /// <summary>
        /// Patches the configuration policy. 
        /// </summary>
        /// <param name="policy">policy to be updated. </param>
        /// <returns>Updated configuration policy. </returns>
        new ConfigurationPolicy Patch(ConfigurationPolicy policy);

        /// <summary>
        /// Patches the configuration policy asynchronously.
        /// </summary>
        /// <param name="policy">policy to be updated. </param>
        /// <returns>Updated configuration policy. </returns>
        new Task<ConfigurationPolicy> PatchAsync(ConfigurationPolicy policy);

        /// <summary>
        /// Deletes the configuration policy. 
        /// </summary>
        new void Delete();

        /// <summary>
        /// Deletes the configuration policy asynchronously. 
        /// </summary>
        /// <returns>A task which completed when the request is finished.</returns>
        new Task DeleteAsync();
    }
}
