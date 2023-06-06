// -----------------------------------------------------------------------
// <copyright file="IBatchJobStatusCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using Microsoft.Store.PartnerCenter.GenericOperations;

    /// <summary>
    /// Represents the operations that can be done on the partner's batch upload status collection.
    /// </summary>
    public interface IBatchJobStatusCollection : IPartnerComponent, IEntitySelector<IBatchJobStatus>
    {
        /// <summary>
        /// Retrieves a specific customer's devices batch upload status behavior.
        /// </summary>
        /// <param name="trackingId">The tracking id.</param>
        /// <returns>The customer's devices batch upload status operations.</returns>
        new IBatchJobStatus this[string trackingId] { get; }

        /// <summary>
        /// Retrieves a specific customer's devices batch upload status behavior.
        /// </summary>
        /// <param name="trackingId">The tracking id.</param>
        /// <returns>The customer's devices batch upload status operations.</returns>
        new IBatchJobStatus ById(string trackingId);
    }
}