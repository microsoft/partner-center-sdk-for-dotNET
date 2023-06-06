// -----------------------------------------------------------------------
// <copyright file="OrderCollectionByBillingCycleTypeOperations.cs" company="Microsoft Corporation">
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
    using Models;
    using Models.Offers;
    using Models.Orders;
    using Network;

    /// <summary>
    /// Order collection by billing cycle type operations implementation class.
    /// </summary>
    internal class OrderCollectionByBillingCycleTypeOperations : BasePartnerComponent<Tuple<string, BillingCycleType>>, IOrderCollectionByBillingCycleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCollectionByBillingCycleTypeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        /// <param name="billingCyleType">The billing cycle type.</param>
        public OrderCollectionByBillingCycleTypeOperations(IPartner rootPartnerOperations, string customerId, BillingCycleType billingCyleType)
            : base(rootPartnerOperations, new Tuple<string, BillingCycleType>(customerId, billingCyleType))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <inheritdoc/>
        public ResourceCollection<Order> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Order>>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<ResourceCollection<Order>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Order, ResourceCollection<Order>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrdersByBillingCyleType.Path, this.Context.Item1));

            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOrdersByBillingCyleType.Parameters.BillingType, this.Context.Item2.ToString()));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public ResourceCollection<Order> Get(bool includePrice)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Order>>(() => this.GetAsync(includePrice));
        }

        /// <inheritdoc />
        public async Task<ResourceCollection<Order>> GetAsync(bool includePrice)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Order, ResourceCollection<Order>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrdersByBillingCyleType.Path, this.Context.Item1));

            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOrdersByBillingCyleType.Parameters.BillingType, this.Context.Item2.ToString()));

            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOrdersByBillingCyleType.Parameters.IncludePrice, includePrice.ToString()));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
