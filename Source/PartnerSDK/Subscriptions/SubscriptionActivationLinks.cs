// -----------------------------------------------------------------------
// <copyright file="SubscriptionActivationLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Orders;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// Implements getting customer subscription activation link resource collection for a given subscription.
    /// </summary>
    internal class SubscriptionActivationLinks : BasePartnerComponent<Tuple<string, string>>, ISubscriptionActivationLinks
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionActivationLinks"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription Id.</param>
        public SubscriptionActivationLinks(IPartner rootPartnerOperations, string customerId, string subscriptionId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, subscriptionId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("subscriptionId must be set.");
            }
        }

        /// <summary>
        /// Synchronously gets the subscription activation links.
        /// </summary>
        /// <returns>The subscription activation links.</returns>
        public ResourceCollection<OrderLineItemActivationLink> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<OrderLineItemActivationLink>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the subscription activation links.
        /// </summary>
        /// <returns>The subscription activation links.</returns>
        public async Task<ResourceCollection<OrderLineItemActivationLink>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ResourceCollection<OrderLineItemActivationLink>, ResourceCollection<OrderLineItemActivationLink>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionActivationLink.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}