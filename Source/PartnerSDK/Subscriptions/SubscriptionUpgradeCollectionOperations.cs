// -----------------------------------------------------------------------
// <copyright file="SubscriptionUpgradeCollectionOperations.cs" company="Microsoft Corporation">
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
    /// The customer subscription upgrade implementation.
    /// </summary>
    internal class SubscriptionUpgradeCollectionOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionUpgradeCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionUpgradeCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id to whom the subscriptions belong.</param>
        /// <param name="subscriptionId">The subscription Id where the upgrade is occurring.</param>
        public SubscriptionUpgradeCollectionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
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
        /// Retrieves all subscription upgrades.
        /// </summary>
        /// <returns>The subscription upgrades.</returns>
        public ResourceCollection<Upgrade> Get() => PartnerService.Instance.SynchronousExecute(this.GetAsync);

        /// <summary>
        /// Asynchronously retrieves all subscription upgrades.
        /// </summary>
        /// <returns>The subscription upgrades.</returns>
        public async Task<ResourceCollection<Upgrade>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Upgrade, ResourceCollection<Upgrade>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionUpgrades.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Performs a subscription upgrade.
        /// </summary>
        /// <param name="subscriptionUpgrade">The subscription upgrade to perform.</param>
        /// <returns>The subscription upgrade result.</returns>
        public UpgradeResult Create(Upgrade subscriptionUpgrade) => PartnerService.Instance.SynchronousExecute(() => this.CreateAsync(subscriptionUpgrade));

        /// <summary>
        /// Asynchronously performs a subscription upgrade.
        /// </summary>
        /// <param name="subscriptionUpgrade">The subscription upgrade to perform.</param>
        /// <returns>A task containing the subscription upgrade result.</returns>
        public async Task<UpgradeResult> CreateAsync(Upgrade subscriptionUpgrade)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Upgrade, UpgradeResult>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.PostSubscriptionUpgrade.Path, this.Context.Item1, this.Context.Item2),
                jsonConverter: new ResourceCollectionConverter<Upgrade>());

            return await partnerApiServiceProxy.PostAsync(subscriptionUpgrade);
        }
    }
}
