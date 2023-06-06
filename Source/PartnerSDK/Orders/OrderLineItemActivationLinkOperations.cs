// -----------------------------------------------------------------------
// <copyright file="OrderLineItemActivationLinkOperations.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// Order line item activation link operations implementation class.
    /// </summary>
    internal class OrderLineItemActivationLinkOperations : BasePartnerComponent<Tuple<string, string, string>>, IOrderLineItemActivationLink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderLineItemActivationLinkOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="orderId">The order Id.</param>
        /// <param name="lineItemNumber">The line item number.</param>
        public OrderLineItemActivationLinkOperations(IPartner rootPartnerOperations, string customerId, string orderId, string lineItemNumber)
            : base(rootPartnerOperations, new Tuple<string, string, string>(customerId, orderId, lineItemNumber))
        {
            if (string.IsNullOrWhiteSpace(lineItemNumber))
            {
                throw new ArgumentException("lineItemId must be set.");
            }

            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("orderId must be set.");
            }

            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Retrieves the order line item activation link collection.
        /// </summary>
        /// <returns>The order line item activation link collection.</returns>
        public ResourceCollection<OrderLineItemActivationLink> Get()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the order line item activation link collection.
        /// </summary>
        /// <returns>The order line item activation link collection.</returns>
        public async Task<ResourceCollection<OrderLineItemActivationLink>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ResourceCollection<OrderLineItemActivationLink>, ResourceCollection<OrderLineItemActivationLink>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetActivationLinksByLineItemNumber.Path, this.Context.Item1, this.Context.Item2, this.Context.Item3));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}