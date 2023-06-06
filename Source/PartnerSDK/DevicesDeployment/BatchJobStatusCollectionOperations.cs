// -----------------------------------------------------------------------
// <copyright file="BatchJobStatusCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.DevicesDeployment;
    using Network;

    /// <summary>
    /// Implements operations that apply to devices batch upload status collection.
    /// </summary>
    internal class BatchJobStatusCollectionOperations : BasePartnerComponent, IBatchJobStatusCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchJobStatusCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public BatchJobStatusCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException(nameof(customerId));
            }
        }

        /// <summary>
        /// Retrieves a specific customer's devices batch upload status operations.
        /// </summary>
        /// <param name="trackingId">The device collection upload tracking id.</param>
        /// <returns>The customer's devices batch upload status operations.</returns>
        public IBatchJobStatus this[string trackingId]
        {
            get
            {
                return this.ById(trackingId);
            }
        }

        /// <summary>
        /// Retrieves a specific customer's devices batch upload status operations.
        /// </summary>
        /// <param name="trackingId">The tracking id.</param>
        /// <returns>The customer's devices batch upload status operations.</returns>
        public IBatchJobStatus ById(string trackingId)
        {
            return new BatchJobStatusOperations(this.Partner, this.Context, trackingId);
        }        
    }
}
