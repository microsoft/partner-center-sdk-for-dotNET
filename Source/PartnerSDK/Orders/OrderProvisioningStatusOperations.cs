// -----------------------------------------------------------------------
// <copyright file="OrderProvisioningStatusOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Orders;
    using Network;

    /// <summary>
    /// Implements operations that apply to order provisioning status.
    /// </summary>
    internal class OrderProvisioningStatusOperations : BasePartnerComponent<Tuple<string, string>>, IOrderProvisioningStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderProvisioningStatusOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="orderId">The order Id.</param>
        public OrderProvisioningStatusOperations(IPartner rootPartnerOperations, string customerId, string orderId) 
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, orderId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("orderId must be set.");
            }
        }

        /// <summary>
        /// Retrieves the order provisioning status.
        /// </summary>
        /// <returns>The customer order.</returns>
        public ResourceCollection<OrderLineItemProvisioningStatus> Get()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the order provisioning status.
        /// </summary>
        /// <returns>The order provisioning status.</returns>
        public async Task<ResourceCollection<OrderLineItemProvisioningStatus>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ResourceCollection<OrderLineItemProvisioningStatus>, ResourceCollection<OrderLineItemProvisioningStatus>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrderProvisioningStatus.Path, this.Context.Item1, this.Context.Item2));
            
            return await partnerApiServiceProxy.GetAsync();
        }
    }
}