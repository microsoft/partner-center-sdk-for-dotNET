// -----------------------------------------------------------------------
// <copyright file="BatchJobStatusOperations.cs" company="Microsoft Corporation">
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
    /// Implements operations that apply to devices batch upload status.  
    /// </summary>
    internal class BatchJobStatusOperations : BasePartnerComponent<Tuple<string, string>>, IBatchJobStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchJobStatusOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        /// <param name="trackingId">The status tracking Id.</param>
        public BatchJobStatusOperations(IPartner rootPartnerOperations, string customerId, string trackingId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, trackingId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(trackingId))
            {
                throw new ArgumentNullException(nameof(trackingId));
            }
        }

        /// <summary>
        /// Retrieves devices batch upload status of the customer.
        /// </summary>
        /// <returns>Devices batch upload status.</returns>
        public BatchUploadDetails Get()
        {
            return PartnerService.Instance.SynchronousExecute<BatchUploadDetails>(() => this.GetAsync());
        }

        /// <summary>
        /// Retrieves batch upload status of the customer asynchronously.
        /// </summary>
        /// <returns>Devices batch upload status. </returns>
        public Task<BatchUploadDetails> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<string, BatchUploadDetails>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetBatchUploadStatus.Path, this.Context.Item1, this.Context.Item2));

            return partnerServiceProxy.GetAsync();
        }
    }
}
