// -----------------------------------------------------------------------
// <copyright file="SubscriptionRegistrationStatusOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// This class implements the operations available on a customer's subscription registration status.
    /// </summary>
    internal class SubscriptionRegistrationStatusOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionRegistrationStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionRegistrationStatusOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription Id.</param>
        public SubscriptionRegistrationStatusOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
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
        /// Retrieves a subscription registration status.
        /// </summary>
        /// <returns>The subscription registration status details.</returns>
        public SubscriptionRegistrationStatus Get()
        {
            return PartnerService.Instance.SynchronousExecute<SubscriptionRegistrationStatus>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves a subscription registration status.
        /// </summary>
        /// <returns>The subscription registration status details.</returns>
        public async Task<SubscriptionRegistrationStatus> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<SubscriptionRegistrationStatus, SubscriptionRegistrationStatus>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionRegistrationStatus.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}