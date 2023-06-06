// -----------------------------------------------------------------------
// <copyright file="SubscriptionUsageSummaryOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Usage;
    using Network;    

    /// <summary>
    /// This class implements the operations for a customer's subscription.
    /// </summary>
    internal class SubscriptionUsageSummaryOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionUsageSummary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionUsageSummaryOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription id.</param>
        public SubscriptionUsageSummaryOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
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
        /// Gets the subscription usage summary.
        /// </summary>
        /// <returns>The subscription usage summary.</returns>
        public SubscriptionUsageSummary Get()
        {
            return PartnerService.Instance.SynchronousExecute<SubscriptionUsageSummary>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the subscription usage summary.
        /// </summary>
        /// <returns>The subscription usage summary.</returns>
        public async Task<SubscriptionUsageSummary> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<SubscriptionUsageSummary, SubscriptionUsageSummary>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionUsageSummary.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
