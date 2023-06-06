// -----------------------------------------------------------------------
// <copyright file="ISubscription.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Usage;
    using Microsoft.Store.PartnerCenter.Utilization;

    /// <summary>
    /// This interface defines the operations available on a customer's subscription.
    /// </summary>
    public interface ISubscription : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<Subscription>, IEntityPatchOperations<Subscription>
    {
        /// <summary>
        /// Gets the current subscription's add-ons operations.
        /// </summary>
        ISubscriptionAddOnCollection AddOns { get; }

        /// <summary>
        /// Gets the current subscription's upgrade operations.
        /// </summary>
        ISubscriptionUpgradeCollection Upgrades { get; }

        /// <summary>
        /// Gets the current subscription's conversion operations. These operations will only apply to trial subscriptions.
        /// </summary>
        ISubscriptionConversionCollection Conversions { get; }

        /// <summary>
        /// Gets the current subscription's transition eligibility operations.
        /// </summary>
        ISubscriptionTransitionEligibilityCollection TransitionEligibilities { get; }

        /// <summary>
        /// Gets the current subscription's transition operations.
        /// </summary>
        ISubscriptionTransitionCollection Transitions { get; }

        /// <summary>
        /// Gets the current subscription's resource usage records operations.
        /// </summary>
        ISubscriptionUsageRecordCollection UsageRecords { get; }

        /// <summary>
        /// Gets the current subscription's usage summary operations.
        /// </summary>
        ISubscriptionUsageSummary UsageSummary { get; }

        /// <summary>
        /// Gets the current subscription's utilization operations.
        /// </summary>
        IUtilizationCollection Utilization { get; }

        /// <summary>
        /// Gets the current subscription's provisioning status operations.
        /// </summary>
        ISubscriptionProvisioningStatus ProvisioningStatus { get; }

        /// <summary>
        /// Gets the current subscription's support contact operations.
        /// </summary>
        ISubscriptionSupportContact SupportContact { get; }

        /// <summary>
        /// Gets the current subscription's registration operations.
        /// </summary>
        ISubscriptionRegistration Registration { get; }

        /// <summary>
        /// Gets the current subscription's registration status operations.
        /// </summary>
        ISubscriptionRegistrationStatus RegistrationStatus { get; }

        /// <summary>
        /// Gets the current subscription's activation links.
        /// </summary>
        ISubscriptionActivationLinks ActivationLinks { get; }

        /// <summary>
        /// Gets the azure entitlement operations.
        /// </summary>
        /// <value>
        /// The azure entitlement.
        /// </value>
        IAzureEntitlementCollection AzureEntitlements { get; }

        /// <summary>
        /// Retrieves the subscription.
        /// </summary>
        /// <returns>The subscription.</returns>
        new Subscription Get();

        /// <summary>
        /// Asynchronously retrieves the subscription.
        /// </summary>
        /// <returns>The subscription.</returns>
        new Task<Subscription> GetAsync();

        /// <summary>
        /// Patches the subscription.
        /// </summary>
        /// <param name="subscription">A subscription that has the properties to be patched set.</param>
        /// <returns>The updated subscription.</returns>
        new Subscription Patch(Subscription subscription);

        /// <summary>
        /// Asynchronously patches the subscription.
        /// </summary>
        /// <param name="subscription">A subscription that has the properties to be patched set.</param>
        /// <returns>The updated subscription.</returns>
        new Task<Subscription> PatchAsync(Subscription subscription);

        /// <summary>
        /// Activate subscription (This operation is currently available for Sandbox Partners only and used to activate a 3PP subscription).
        /// </summary>
        /// <returns>Subscription activation result.</returns>
        SubscriptionActivationResult Activate();

        /// <summary>
        /// Asynchronously activate subscription (This operation is currently available for Sandbox Partners only and used to activate a 3PP subscription).
        /// </summary>
        /// <returns>Subscription activation result.</returns>
        Task<SubscriptionActivationResult> ActivateAsync();

        /// <summary>
        /// Gets an Azure Plan's subscription entitlements.
        /// </summary>
        /// <returns>A resource collection of Azure entitlements.</returns>
        ResourceCollection<AzureEntitlement> GetAzurePlanSubscriptionEntitlements();

        /// <summary>
        /// Asynchronously gets an Azure Plan's subscription entitlements.
        /// </summary>
        /// <returns>A resource collection of Azure entitlements.</returns>
        Task<ResourceCollection<AzureEntitlement>> GetAzurePlanSubscriptionEntitlementsAsync();
    }
}
