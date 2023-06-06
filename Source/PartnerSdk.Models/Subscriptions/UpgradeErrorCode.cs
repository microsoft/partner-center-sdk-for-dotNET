// -----------------------------------------------------------------------
// <copyright file="UpgradeErrorCode.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    /// <summary>
    /// The type of errors that prevent subscription upgrading from happening.
    /// </summary>
    public enum UpgradeErrorCode
    {
        /// <summary>
        /// General error.
        /// </summary>
        Other = 0,

        /// <summary>
        /// Upgrade cannot be performed because administrative permissions have been removed.
        /// </summary>
        DelegatedAdminPermissionsDisabled = 1,

        /// <summary>
        /// Upgrade cannot be performed because the subscription status is suspended or deleted.
        /// </summary>
        SubscriptionStatusNotActive = 2,

        /// <summary>
        /// Upgrade cannot be performed because of conflicting source service types.
        /// </summary>
        ConflictingServiceTypes = 3,

        /// <summary>
        /// Upgrade cannot be performed due to concurrent subscription restrictions.
        /// </summary>
        ConcurrencyConflicts = 4,

        /// <summary>
        /// Upgrade cannot be performed because the current request is using app context.
        /// </summary>
        UserContextRequired = 5,

        /// <summary>
        /// Upgrade cannot be performed because the source subscription has previously purchased add-ons.
        /// </summary>
        SubscriptionAddOnsPresent = 6,

        /// <summary>
        /// Upgrade cannot be performed because the source subscription does not have upgrade paths.
        /// </summary>
        SubscriptionDoesNotHaveAnyUpgradePaths = 7,

        /// <summary>
        /// Upgrade cannot be performed because the specified upgrade path is not an available upgrade path.
        /// </summary>
        SubscriptionTargetOfferNotFound = 8,

        /// <summary>
        /// The subscription is not provisioned yet.
        /// Happens when the order is still being processed. Eventually the subscription will be provisioned and an entitlement is created.
        /// </summary>
        SubscriptionNotProvisioned = 9,

        /// <summary>
        /// The offer does not support the billing cycle
        /// Happens when the target offer does not support the billing cycle set on the source subscription.
        /// </summary>
        OfferDoesNotSupportBillingCycle = 10,

        /// <summary>
        /// Upgrade cannot be performed due to insufficient GDAP claims.
        /// </summary>
        InsufficientGranularAdminPermissions = 11,
    }
}