// -----------------------------------------------------------------------
// <copyright file="SubscriptionAddOnCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Subscriptions;
    using Network;

    /// <summary>
    /// Implements operations related to a single subscription add-ons.
    /// </summary>
    internal class SubscriptionAddOnCollectionOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionAddOnCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionAddOnCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription id.</param>
        public SubscriptionAddOnCollectionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, subscriptionId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId should be set.");
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("subscriptionId should be set.");
            }
        }

        /// <summary>
        /// Retrieves the add-on subscriptions.
        /// </summary>
        /// <returns>Collection of add-on subscriptions.</returns>
        public ResourceCollection<Subscription> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Subscription>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the add-on subscriptions.
        /// </summary>
        /// <returns>Collection of add-on subscriptions.</returns>
        public async Task<ResourceCollection<Subscription>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Subscription, ResourceCollection<Subscription>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetAddOnSubscriptions.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
