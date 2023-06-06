// -----------------------------------------------------------------------
// <copyright file="CartErrorCode.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Carts.Enums
{
    using Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Types of cart error code.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum CartErrorCode
    {
        /// <summary>
        /// Default value
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Currency is not supported for given market
        /// </summary>
        CurrencyIsNotSupported = 10000,

        /// <summary>
        /// Catalog item id is not valid
        /// </summary>
        CatalogItemIdIsNotValid = 10001,

        /// <summary>
        /// Not enough quota available
        /// </summary>
        QuotaNotAvailable = 10002,

        /// <summary>
        /// Inventory is not available for selected offer
        /// </summary>
        InventoryNotAvailable = 10003,

        /// <summary>
        /// Setting participants is not supported for Partner
        /// </summary>
        ParticipantsIsNotSupportedForPartner = 10004,

        /// <summary>
        /// Unable to process cart line item.
        /// </summary>
        UnableToProcessCartLineItem = 10006,

        /// <summary>
        /// Subscription is not valid.
        /// </summary>
        SubscriptionIsNotValid = 10007,

        /// <summary>
        /// Subscription is not enabled for RI purchase.
        /// </summary>
        SubscriptionIsNotEnabledForRI = 10008,

        /// <summary>
        /// The sandbox limit has been exceeded.
        /// </summary>
        SandboxLimitExceeded = 10009,

        /// <summary>
        /// Generic input is not valid.
        /// </summary>
        InvalidInput = 10010,

        /// <summary>
        /// The subscription is not registered.
        /// </summary>
        SubscriptionNotRegistered = 10011,

        /// <summary>
        /// Attestation has not been accepted.
        /// </summary>
        AttestationNotAccepted = 10012,

        /// <summary>
        /// Billing plan is not supported.
        /// </summary>
        BillingPlanNotSupported = 10013,

        /// <summary>
        /// The caller passed an invalid promotionId.
        /// </summary>
        InvalidPromotionId = 10014,

        /// <summary>
        /// The desired seat count exceeded the maximum seat count allowed per subscription.
        /// </summary>
        MaxAllowedSeatsPerSubscriptionExceeded = 10015,

        /// <summary>
        /// Custom term end date can only be set for non-trial modern office products with a term duration.
        /// </summary>
        InvalidCustomTermEndDateProduct = 10016,

        /// <summary>
        /// The custom term end date is invalid.
        /// </summary>
        InvalidCustomTermEndDate = 10017,

        /// <summary>
        /// The base offer seat quantity has not been met to purchase the addon.
        /// </summary>
        BaseOfferSeatQuantityNotMet = 10018,

        /// <summary>
        /// The minimum seats have not been met to purchase the addon.
        /// </summary>
        MinimumAddOnSeatsNotMet = 10019,

        /// <summary>
        /// The legacy offer is no longer eligible for purchase.
        /// </summary>
        LegacyOfferNotEligibleForPurchase = 500600,
    }
}
