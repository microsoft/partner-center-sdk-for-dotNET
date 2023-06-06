// -----------------------------------------------------------------------
// <copyright file="OrderCollectionOperations.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models.Offers;
    using Models;
    using Models.Orders;
    using Network;
    using Utilities;

    /// <summary>
    /// Order collection operations implementation class.
    /// </summary>
    internal class OrderCollectionOperations : BasePartnerComponent, IOrderCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public OrderCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Obtains a specific order behavior.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <returns>The order operations.</returns>
        public IOrder this[string orderId]
        {
            get
            {
                return this.ById(orderId);
            }
        }

        /// <summary>
        /// Obtains a specific order behavior.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <returns>The order operations.</returns>
        public IOrder ById(string orderId)
        {
            return new OrderOperations(this.Partner, this.Context, orderId);
        }

        /// <summary>
        /// Places a new order for the customer.
        /// </summary>
        /// <param name="newOrder">The new order.</param>
        /// <returns>The newly created order.</returns>
        public Order Create(Order newOrder)
        {
            return PartnerService.Instance.SynchronousExecute<Order>(() => this.CreateAsync(newOrder));
        }

        /// <summary>
        /// Asynchronously places a new order for the customer.
        /// </summary>
        /// <param name="newOrder">The new order.</param>
        /// <returns>The newly created order.</returns>
        public async Task<Order> CreateAsync(Order newOrder)
        {
            ParameterValidator.Required(newOrder, "newOrder can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<Order, Order>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrders.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(newOrder);
        }

        /// <summary>
        /// Places a new order for the customer with a Customer User UPN for license assignment.
        /// </summary>
        /// <param name="newOrder">The new order.</param>
        /// <param name="customerUserUpn">The customer user UPN for license assignment.</param>
        /// <returns>The newly created order.</returns>
        public Order Create(Order newOrder, string customerUserUpn)
        {
            return PartnerService.Instance.SynchronousExecute<Order>(() => this.CreateAsync(newOrder, customerUserUpn));
        }

        /// <summary>
        /// Asynchronously places a new order for the customer with a Customer User UPN for license assignment.
        /// </summary>
        /// <param name="newOrder">The new order.</param>
        /// <param name="customerUserUpn">The customer user UPN for license assignment.</param>
        /// <returns>The newly created order.</returns>
        public async Task<Order> CreateAsync(Order newOrder, string customerUserUpn)
        {
            ParameterValidator.Required(newOrder, "newOrder can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<Order, Order>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrders.Path, this.Context));

            if (!string.IsNullOrWhiteSpace(customerUserUpn))
            {
                partnerApiServiceProxy.CustomerUserUpn = customerUserUpn;
            }

            return await partnerApiServiceProxy.PostAsync(newOrder);
        }

        /// <summary>
        /// Retrieves all the orders the customer made.
        /// </summary>
        /// <returns>All the customer orders.</returns>
        public ResourceCollection<Order> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Order>>(() => this.GetAsync());
        }

        /// <summary>
        /// Retrieves all the orders the customer made including pricing details (based on access permissions) when requested.
        /// </summary>
        /// <param name="includePrice">Whether to include pricing details in the order information.</param>
        /// <returns>All the customer orders including pricing details (based on access permissions) when requested.</returns>
        public ResourceCollection<Order> Get(bool includePrice)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Order>>(() => this.GetAsync(includePrice));
        }

        /// <summary>
        /// Asynchronously retrieves all the orders the customer made.
        /// </summary>
        /// <returns>All the customer orders.</returns>
        public async Task<ResourceCollection<Order>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Order, ResourceCollection<Order>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrders.Path, this.Context));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Asynchronously retrieves all the orders the customer made including pricing details (based on access permissions) when requested.
        /// </summary>
        /// <param name="includePrice">Whether to include pricing details in the order information.</param>
        /// <returns>All the customer orders including pricing details (based on access permissions) when requested.</returns>
        public async Task<ResourceCollection<Order>> GetAsync(bool includePrice)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Order, ResourceCollection<Order>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrders.Path, this.Context));

            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOrders.Parameters.IncludePrice, includePrice.ToString()));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <inheritdoc />
        public IOrderCollectionByBillingCycleType ByBillingCycleType(BillingCycleType billingCycleType)
        {
            return new OrderCollectionByBillingCycleTypeOperations(this.Partner, this.Context, billingCycleType);
        }
    }
}
