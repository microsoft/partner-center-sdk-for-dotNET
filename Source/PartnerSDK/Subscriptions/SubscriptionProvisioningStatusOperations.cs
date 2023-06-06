// -----------------------------------------------------------------------
// <copyright file="SubscriptionProvisioningStatusOperations.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// Implements getting customer subscription provisioning status details for a given subscription.
    /// </summary>
    internal class SubscriptionProvisioningStatusOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionProvisioningStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionProvisioningStatusOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription Id.</param>
        public SubscriptionProvisioningStatusOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
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
        /// Gets the subscription provisioning status details.
        /// </summary>
        /// <returns>The subscription usage summary.</returns>
        public SubscriptionProvisioningStatus Get()
        {
            return PartnerService.Instance.SynchronousExecute<SubscriptionProvisioningStatus>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the subscription provisioning status details.
        /// </summary>
        /// <returns>The subscription provisioning status details.</returns>
        public async Task<SubscriptionProvisioningStatus> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<SubscriptionProvisioningStatus, SubscriptionProvisioningStatus>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionProvisioningStatus.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}