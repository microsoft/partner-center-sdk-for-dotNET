// -----------------------------------------------------------------------
// <copyright file="SubscriptionTransitionEligibilityOperations.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// The customer subscription upgrade implementation.
    /// </summary>
    internal class SubscriptionTransitionEligibilityOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionTransitionEligibilityCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionTransitionEligibilityOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id to whom the subscriptions belong.</param>
        /// <param name="subscriptionId">The subscription Id where the upgrade is occurring.</param>
        public SubscriptionTransitionEligibilityOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
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
        /// <param name="eligibilityType">The eligibility type.</param>
        /// <returns>The subscription upgrades.</returns>
        public ResourceCollection<TransitionEligibility> Get(string eligibilityType) => PartnerService.Instance.SynchronousExecute(() => this.GetAsync(eligibilityType));

        /// <summary>
        /// Retrieves transition eligibilities.
        /// </summary>
        /// <returns>The transition eligiblity.</returns>
        public ResourceCollection<TransitionEligibility> Get()
        {
            return this.Get(null);
        }

        /// <inheritdoc/>
        public Task<ResourceCollection<TransitionEligibility>> GetAsync()
        {
            return this.GetAsync(null);
        }

        /// <summary>
        /// Asynchronously retrieves all subscription upgrades.
        /// </summary>
        /// <param name="eligibilityType">The eligibility type.</param>
        /// <returns>The subscription upgrades.</returns>
        public async Task<ResourceCollection<TransitionEligibility>> GetAsync(string eligibilityType = null)
        {
            var resourcePath = string.Format(
                CultureInfo.InvariantCulture,
                PartnerService.Instance.Configuration.Apis.GetTransitionEligibilities.Path + (!string.IsNullOrEmpty(eligibilityType) ? $"?eligibilityType={eligibilityType}" : string.Empty),
                this.Context.Item1,
                this.Context.Item2);
            var partnerApiServiceProxy = new PartnerServiceProxy<TransitionEligibility, ResourceCollection<TransitionEligibility>>(this.Partner, resourcePath);
            partnerApiServiceProxy.IsUrlPathAlreadyBuilt = true;
            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
