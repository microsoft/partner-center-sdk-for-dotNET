// -----------------------------------------------------------------------
// <copyright file="IBatchJobStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Models.DevicesDeployment;

    /// <summary>
    /// Represents the operations that can be done on the partner's Batch Upload Status.
    /// </summary>
    public interface IBatchJobStatus : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<BatchUploadDetails>
    {
        /// <summary>
        /// Retrieves a specific customer devices batch upload status details.
        /// </summary>
        /// <returns>The devices batch upload status details.</returns>
        new BatchUploadDetails Get();

        /// <summary>
        /// Retrieves a specific customer devices batch upload status details asynchronously.
        /// </summary>
        /// <returns>The devices batch upload status details.</returns>
        new Task<BatchUploadDetails> GetAsync();
    }
}