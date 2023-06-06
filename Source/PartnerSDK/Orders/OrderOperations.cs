// -----------------------------------------------------------------------
// <copyright file="OrderOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Orders;
    using Network;
    using Utilities;

    /// <summary>
    /// Order operations implementation class.
    /// </summary>
    internal class OrderOperations : BasePartnerComponent<Tuple<string, string>>, IOrder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="orderId">The order Id.</param>
        public OrderOperations(IPartner rootPartnerOperations, string customerId, string orderId)
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
        /// Gets an Order provisioning status operations.
        /// </summary>
        public IOrderProvisioningStatus ProvisioningStatus
        {
            get { return new OrderProvisioningStatusOperations(this.Partner, this.Context.Item1, this.Context.Item2); }
        }

        /// <summary>
        /// Gets Order attachment collection operations.
        /// </summary>
        public IOrderAttachmentCollection Attachments
        {
            get { return new OrderAttachmentCollectionOperations(this.Partner, this.Context.Item1, this.Context.Item2); }
        }

        /// <summary>
        /// Gets line item collection operations.
        /// </summary>
        public IOrderLineItemCollection OrderLineItems
        {
            get { return new OrderLineItemCollectionOperations(this.Partner, this.Context.Item1, this.Context.Item2); }
        }

        /// <summary>
        /// Gets order activation link collection operation.
        /// </summary>
        public IOrderActivationLinks OrderActivationLinks
        {
            get { return new OrderActivationLinks(this.Partner, this.Context.Item1, this.Context.Item2); }
        }

        /// <summary>
        /// Retrieves the customer order.
        /// </summary>
        /// <returns>The customer order.</returns>
        public Order Get()
        {
            return PartnerService.Instance.SynchronousExecute<Order>(() => this.GetAsync());
        }

        /// <summary>
        /// Retrieves the order information including pricing details (based on access permissions) when requested..
        /// </summary>
        /// <param name="includePrice">Whether to include pricing details in the order information.</param>
        /// <returns>The order information including pricing details (based on access permissions) when requested.</returns>
        public Order Get(bool includePrice)
        {
            return PartnerService.Instance.SynchronousExecute<Order>(() => this.GetAsync(includePrice));
        }

        /// <summary>
        /// Asynchronously retrieves the customer order.
        /// </summary>
        /// <returns>The customer order.</returns>
        public async Task<Order> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Order, Order>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrder.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Asynchronously retrieves the order the customer made including pricing details (based on access permissions) when requested.
        /// </summary>
        /// <param name="includePrice">Whether to include pricing details in the order information.</param>
        /// <returns>The customer order including pricing details (based on access permissions) when requested.</returns>
        public async Task<Order> GetAsync(bool includePrice)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Order, Order>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrder.Path, this.Context.Item1, this.Context.Item2));

            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOrder.Parameters.IncludePrice, includePrice.ToString()));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Patches a customer order.
        /// </summary>
        /// <param name="order">The order to patch.</param>
        /// <returns>The updated order.</returns>
        public Order Patch(Order order)
        {
            return PartnerService.Instance.SynchronousExecute<Order>(() => this.PatchAsync(order));
        }

        /// <summary>
        /// Asynchronously patches a customer order.
        /// </summary>
        /// <param name="order">The order to patch.</param>
        /// <returns>The updated order.</returns>
        public async Task<Order> PatchAsync(Order order)
        {
            ParameterValidator.Required(order, "order can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<Order, Order>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpdateOrder.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.PatchAsync(order);
        }
    }
}
