// -----------------------------------------------------------------------
// <copyright file="OrderActivationLinks.cs" company="Microsoft Corporation">
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
    /// Order activation link operations implementation class.
    /// </summary>
    internal class OrderActivationLinks : BasePartnerComponent<Tuple<string, string>>, IOrderActivationLinks
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderActivationLinks"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="orderId">The order Id.</param>
        public OrderActivationLinks(IPartner rootPartnerOperations, string customerId, string orderId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, orderId))
        {
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
        /// Retrieves the order activation link collection.
        /// </summary>
        /// <returns>The order activation link collection.</returns>
        public ResourceCollection<OrderLineItemActivationLink> Get()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the activation link collection for an order.
        /// </summary>
        /// <returns>The order activation link collection.</returns>
        public async Task<ResourceCollection<OrderLineItemActivationLink>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ResourceCollection<OrderLineItemActivationLink>, ResourceCollection<OrderLineItemActivationLink>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrderActivationLinks.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}