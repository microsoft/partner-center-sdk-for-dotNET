// -----------------------------------------------------------------------
// <copyright file="CustomerServiceRequestOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ServiceRequests
{
    using System;
    using System.Threading.Tasks;
    using Models.ServiceRequests;
    using Network;

    /// <summary>
    /// Implements operations that can be performed on a single customer's service requests.
    /// </summary>
    internal class CustomerServiceRequestOperations : BasePartnerComponent<Tuple<string, string>>, IServiceRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerServiceRequestOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        /// <param name="serviceRequestId">The service Request Id</param>
        public CustomerServiceRequestOperations(IPartner rootPartnerOperations, string customerId, string serviceRequestId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, serviceRequestId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException("customerId");
            }

            if (string.IsNullOrWhiteSpace(serviceRequestId))
            {
                throw new ArgumentNullException("serviceRequestId");
            }
        }

        /// <summary>
        /// Get Service Request By customer ID
        /// </summary>
        /// <returns>Service Request</returns>
        public ServiceRequest Get()
        {
            return PartnerService.Instance.SynchronousExecute<ServiceRequest>(() => this.GetAsync());
        }

        /// <summary>
        /// Get Service Request By ID
        /// </summary>
        /// <returns>Service Request</returns>
        public async Task<ServiceRequest> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ServiceRequest, ServiceRequest>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetServiceRequestCustomer.Path, this.Context.Item1, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Patches a Service Request.
        /// </summary>
        /// <param name="updatePayload">Payload of the update request</param>
        /// <returns>Updated Service Request</returns>
        public ServiceRequest Patch(ServiceRequest updatePayload)
        {
            return PartnerService.Instance.SynchronousExecute<ServiceRequest>(() => this.PatchAsync(updatePayload));
        }

        /// <summary>
        /// Patches a Service Request.
        /// </summary>
        /// <param name="updatePayload">Payload of the update request</param>
        /// <returns>Updated Service Request</returns>
        public Task<ServiceRequest> PatchAsync(ServiceRequest updatePayload)
        {
            var partnerServiceProxy = new PartnerServiceProxy<ServiceRequest, ServiceRequest>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.UpdateServiceRequestCustomer.Path, this.Context.Item1, this.Context.Item2));

            return partnerServiceProxy.PatchAsync(updatePayload);
        }
    }
}
