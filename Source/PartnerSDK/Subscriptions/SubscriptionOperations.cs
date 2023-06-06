// -----------------------------------------------------------------------
// <copyright file="SubscriptionOperations.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Usage;
    using Microsoft.Store.PartnerCenter.Utilities;
    using Microsoft.Store.PartnerCenter.Utilization;

    /// <summary>
    /// This class implements the operations for a customer's subscription.
    /// </summary>
    internal class SubscriptionOperations : BasePartnerComponent<Tuple<string, string>>, ISubscription
    {
        /// <summary>
        /// A lazy reference to the current subscription's add-ons operations.
        /// </summary>
        private readonly Lazy<ISubscriptionAddOnCollection> subscriptionAddOnsOperations;

        /// <summary>
        /// A lazy reference to the current subscription's upgrade operations.
        /// </summary>
        private readonly Lazy<ISubscriptionUpgradeCollection> subscriptionUpgradeOperations;

        /// <summary>
        /// A lazy reference to the current subscription's conversion operations.
        /// </summary>
        private readonly Lazy<ISubscriptionConversionCollection> subscriptionConversionOperations;

        /// <summary>
        /// A lazy reference to the current subscription's transition eligibilities operations.
        /// </summary>
        private readonly Lazy<ISubscriptionTransitionEligibilityCollection> transitionEligibilitiesOperations;

        /// <summary>
        /// A lazy reference to the current subscription's transition operations.
        /// </summary>
        private readonly Lazy<ISubscriptionTransitionCollection> subscriptionTransitionOperations;

        /// <summary>
        /// A lazy reference to the current subscription's resource usage records operations.
        /// </summary>
        private readonly Lazy<ISubscriptionUsageRecordCollection> usageRecordsOperations;

        /// <summary>
        /// A lazy reference to the current subscription's usage summary operations.
        /// </summary>
        private readonly Lazy<ISubscriptionUsageSummary> subscriptionUsageSummaryOperations;

        /// <summary>
        /// A lazy reference to the current subscription's utilities operations.
        /// </summary>
        private readonly Lazy<IUtilizationCollection> subscriptionUtilizationOperations;

        /// <summary>
        /// A lazy reference to the current subscription's provisioning status operations.
        /// </summary>
        private readonly Lazy<ISubscriptionProvisioningStatus> subscriptionProvisioningStatusOperations;

        /// <summary>
        /// A lazy reference to the current subscription's support contact operations.
        /// </summary>
        private readonly Lazy<ISubscriptionSupportContact> subscriptionSupportContactOperations;

        /// <summary>
        /// A lazy reference to the current subscription's registration operations.
        /// </summary>
        private readonly Lazy<ISubscriptionRegistration> subscriptionRegistrationOperations;

        /// <summary>
        /// A lazy reference to the current subscription's registration status operations.
        /// </summary>
        private readonly Lazy<ISubscriptionRegistrationStatus> subscriptionRegistrationStatusOperations;

        /// <summary>
        /// A lazy reference to the current subscription's activation links.
        /// </summary>
        private readonly Lazy<ISubscriptionActivationLinks> subscriptionActivationLinks;

        /// <summary>
        /// A lazy reference to the current customer's Azure entitlement operations.
        /// </summary>
        private readonly Lazy<IAzureEntitlementCollection> azureEntitlementOpertions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription id.</param>
        public SubscriptionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
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

            this.subscriptionAddOnsOperations = new Lazy<ISubscriptionAddOnCollection>(() => new SubscriptionAddOnCollectionOperations(this.Partner, customerId, subscriptionId));
            this.subscriptionUpgradeOperations = new Lazy<ISubscriptionUpgradeCollection>(() => new SubscriptionUpgradeCollectionOperations(this.Partner, customerId, subscriptionId));
            this.subscriptionConversionOperations = new Lazy<ISubscriptionConversionCollection>(() => new SubscriptionConversionCollectionOperations(this.Partner, customerId, subscriptionId));
            this.usageRecordsOperations = new Lazy<ISubscriptionUsageRecordCollection>(() => new SubscriptionUsageRecordCollectionOperations(this.Partner, customerId, subscriptionId));
            this.subscriptionUsageSummaryOperations = new Lazy<ISubscriptionUsageSummary>(() => new SubscriptionUsageSummaryOperations(this.Partner, customerId, subscriptionId));
            this.subscriptionUtilizationOperations = new Lazy<IUtilizationCollection>(() => new UtilizationCollectionOperations(this.Partner, customerId, subscriptionId));
            this.subscriptionSupportContactOperations = new Lazy<ISubscriptionSupportContact>(() => new SubscriptionSupportContactOperations(this.Partner, customerId, subscriptionId));
            this.subscriptionProvisioningStatusOperations = new Lazy<ISubscriptionProvisioningStatus>(() => new SubscriptionProvisioningStatusOperations(this.Partner, customerId, subscriptionId));
            this.subscriptionRegistrationStatusOperations = new Lazy<ISubscriptionRegistrationStatus>(() => new SubscriptionRegistrationStatusOperations(this.Partner, customerId, subscriptionId));
            this.subscriptionRegistrationOperations = new Lazy<ISubscriptionRegistration>(() => new SubscriptionRegistrationOperations(this.Partner, customerId, subscriptionId));
            this.subscriptionActivationLinks = new Lazy<ISubscriptionActivationLinks>(() => new SubscriptionActivationLinks(this.Partner, customerId, subscriptionId));
            this.transitionEligibilitiesOperations = new Lazy<ISubscriptionTransitionEligibilityCollection>(() => new SubscriptionTransitionEligibilityOperations(this.Partner, customerId, subscriptionId));
            this.subscriptionTransitionOperations = new Lazy<ISubscriptionTransitionCollection>(() => new SubscriptionTransitionOperations(this.Partner, customerId, subscriptionId));
            this.azureEntitlementOpertions = new Lazy<IAzureEntitlementCollection>(() => new AzureEntitlementCollectionOperations(this.Partner, customerId, subscriptionId));
        }

        /// <summary>
        /// Gets the current subscription's add-ons operations.
        /// </summary>
        public ISubscriptionAddOnCollection AddOns
        {
            get
            {
                return this.subscriptionAddOnsOperations.Value;
            }
        }

        /// <summary>
        /// Gets the current subscription's upgrade operations.
        /// </summary>
        public ISubscriptionUpgradeCollection Upgrades => this.subscriptionUpgradeOperations.Value;

        /// <summary>
        /// Gets the current subscription's conversion operations.
        /// </summary>
        public ISubscriptionConversionCollection Conversions => this.subscriptionConversionOperations.Value;

        /// <summary>
        /// Gets the current subscription's transition eligibilities operations.
        /// </summary>
        public ISubscriptionTransitionEligibilityCollection TransitionEligibilities => this.transitionEligibilitiesOperations.Value;

        /// <summary>
        /// Gets the current subscription's transition operations.
        /// </summary>
        public ISubscriptionTransitionCollection Transitions => this.subscriptionTransitionOperations.Value;

        /// <summary>
        /// Gets the current subscription's resource usage records operations.
        /// </summary>
        public ISubscriptionUsageRecordCollection UsageRecords
        {
            get
            {
                return this.usageRecordsOperations.Value;
            }
        }

        /// <summary>
        /// Gets the current subscription's usage summary operations.
        /// </summary>
        public ISubscriptionUsageSummary UsageSummary
        {
            get
            {
                return this.subscriptionUsageSummaryOperations.Value;
            }
        }

        /// <summary>
        /// Gets the current subscription's utilization operations.
        /// </summary>
        public IUtilizationCollection Utilization
        {
            get
            {
                return this.subscriptionUtilizationOperations.Value;
            }
        }

        /// <summary>
        /// Gets the current subscription's provisioning status operations.
        /// </summary>
        public ISubscriptionProvisioningStatus ProvisioningStatus
        {
            get
            {
                return this.subscriptionProvisioningStatusOperations.Value;
            }
        }

        /// <summary>
        /// Gets the current subscription's support contact operations.
        /// </summary>
        public ISubscriptionSupportContact SupportContact
        {
            get
            {
                return this.subscriptionSupportContactOperations.Value;
            }
        }

        /// <summary>
        /// Gets the current subscription's registration operations.
        /// </summary>
        public ISubscriptionRegistration Registration
        {
            get
            {
                return this.subscriptionRegistrationOperations.Value;
            }
        }

        /// <summary>
        /// Gets the current subscription's registration status operations.
        /// </summary>
        public ISubscriptionRegistrationStatus RegistrationStatus
        {
            get
            {
                return this.subscriptionRegistrationStatusOperations.Value;
            }
        }

        /// <summary>
        /// Gets the current subscription's activation links.
        /// </summary>
        public ISubscriptionActivationLinks ActivationLinks
        {
            get
            {
                return this.subscriptionActivationLinks.Value;
            }
        }

        /// <summary>
        /// Gets the Azure entitlements for the customer.
        /// </summary>
        /// <value>
        /// The azure entitlement.
        /// </value>
        public IAzureEntitlementCollection AzureEntitlements
        {
            get
            {
                return this.azureEntitlementOpertions.Value;
            }
        }

        /// <summary>
        /// Gets the subscription.
        /// </summary>
        /// <returns>The subscription.</returns>
        public Subscription Get()
        {
            return PartnerService.Instance.SynchronousExecute<Subscription>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the subscription.
        /// </summary>
        /// <returns>The subscription.</returns>
        public async Task<Subscription> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Subscription, Subscription>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscription.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Patches a subscription.
        /// </summary>
        /// <param name="subscription">The subscription information.</param>
        /// <returns>The updated subscription information.</returns>
        public Subscription Patch(Subscription subscription)
        {
            return PartnerService.Instance.SynchronousExecute<Subscription>(() => this.PatchAsync(subscription));
        }

        /// <summary>
        /// Asynchronously patches a subscription.
        /// </summary>
        /// <param name="subscription">The subscription information.</param>
        /// <returns>The updated subscription information.</returns>
        public async Task<Subscription> PatchAsync(Subscription subscription)
        {
            ParameterValidator.Required(subscription, "subscription is required.");

            var partnerApiServiceProxy = new PartnerServiceProxy<Subscription, Subscription>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpdateSubscription.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.PatchAsync(subscription);
        }

        /// <summary>
        /// Activate subscription (This operation is currently available for Sandbox Partners only and used to activate a 3PP subscription).
        /// </summary>
        /// <returns>Subscription activation result.</returns>
        public SubscriptionActivationResult Activate()
        {
            return PartnerService.Instance.SynchronousExecute<SubscriptionActivationResult>(this.ActivateAsync);
        }

        /// <summary>
        /// Asynchronously activate subscription (This operation is currently available for Sandbox Partners only and used to activate a 3PP subscription).
        /// </summary>
        /// <returns>Subscription activation result.</returns>
        public async Task<SubscriptionActivationResult> ActivateAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<HttpRequestMessage, SubscriptionActivationResult>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.Activate3ppSubscription.Path, this.Context.Item1, this.Context.Item2));

            var response = await partnerApiServiceProxy.PostAsync(null);
            return response;
        }

        /// <summary>
        /// Gets an Azure Plan's subscription entitlements.
        /// </summary>
        /// <returns> A resource collection of Azure entitlements.</returns>
        public ResourceCollection<AzureEntitlement> GetAzurePlanSubscriptionEntitlements()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAzurePlanSubscriptionEntitlementsAsync());
        }

        /// <summary>
        /// Asynchronously gets an Azure Plan's subscription entitlements.
        /// </summary>
        /// <returns> A task which completes when a resource collection of Azure entitlements is returned.</returns>
        public async Task<ResourceCollection<AzureEntitlement>> GetAzurePlanSubscriptionEntitlementsAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ResourceCollection<AzureEntitlement>, ResourceCollection<AzureEntitlement>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetAzureEntitlements.Path, this.Context.Item1, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
