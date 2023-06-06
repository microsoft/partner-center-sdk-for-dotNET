// -----------------------------------------------------------------------
// <copyright file="TransitionErrorCode.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    /// <summary>
    /// The type of errors that prevents subscription transfer.
    /// </summary>
    public enum TransitionErrorCode
    {
        /// <summary>
        /// General error.
        /// </summary>
        Other = 0,

        /// <summary>
        /// Transition cannot be performed because DAP has been disabled.
        /// </summary>
        DelegatedAdminPermissionsDisabled = 1,

        /// <summary>
        /// Transition cannot be performed because the subscription status is suspended or deleted.
        /// </summary>
        SubscriptionStatusNotActive = 2,

        /// <summary>
        /// Transition cannot be performed because of conflicting source service types.
        /// </summary>
        ConflictingServiceTypes = 3,

        /// <summary>
        /// Transition cannot be performed due to concurrent subscription restrictions.
        /// </summary>
        ConcurrencyConflicts = 4,

        /// <summary>
        /// Transition cannot be performed because the current request is using App context.
        /// </summary>
        UserContextRequired = 5,

        /// <summary>
        /// Transition cannot be performed because the source subscription has previously purchased add-ons.
        /// </summary>
        SubscriptionAddOnsPresent = 6,

        /// <summary>
        /// Transition cannot be performed because the source subscription does not have upgrade paths.
        /// </summary>
        SubscriptionDoesNotHaveAnyTransitionedPaths = 7,

        /// <summary>
        /// Transition cannot be performed because the specified transition path is not an available transition path.
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
        /// Cannot find any conversions for the trial subscription to convert to.
        /// </summary>
        ConversionsNotFound = 11,

        /// <summary>
        /// User license transfer failed to complete.
        /// </summary>
        UserLicenseError = 12,

        /// <summary>
        /// Transition cannot be performed due to insufficient GDAP claims.
        /// </summary>
        InsufficientGranularAdminPermissions = 13,
    }
}