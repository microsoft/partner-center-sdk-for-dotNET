// -----------------------------------------------------------------------
// <copyright file="SubscriptionRegistrationOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// This class implements the operations available on a customer's subscription registration.
    /// </summary>
    internal class SubscriptionRegistrationOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionRegistrationOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription Id.</param>
        public SubscriptionRegistrationOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
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
        /// Register a subscription to enable Azure Reserved instance purchase.
        /// </summary>
        /// <returns>The location which indicates the URL of the API to query for status.</returns>
        public string Register()
        {
            return PartnerService.Instance.SynchronousExecute<string>(() => this.RegisterAsync());
        }

        /// <summary>
        /// Asynchronously register a subscription to enable Azure Reserved instance purchase.
        /// </summary>
        /// <returns>The location which indicates the URL of the API to query for status.</returns>
        public async Task<string> RegisterAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<HttpRequestMessage, HttpResponseMessage>(
                 this.Partner,
                 string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpdateSubscriptionRegistrationStatus.Path, this.Context.Item1, this.Context.Item2));

            var response = await partnerApiServiceProxy.PostAsync(null);

            return response.Headers.Location != null ? response.Headers.Location.ToString() : string.Empty;
        }
    }
}