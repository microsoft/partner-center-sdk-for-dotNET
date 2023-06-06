// -----------------------------------------------------------------------
// <copyright file="Subscription.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using Invoices;
    using Newtonsoft.Json;
    using Offers;

    /// <summary>
    /// The subscription resource represents the life cycle of a subscription and includes properties that define the states
    /// throughout the subscription life cycle.
    /// </summary>
    public sealed class Subscription : Contract
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscription"/> class.
        /// </summary>
        public Subscription()
        {
            this.Links = new SubscriptionLinks();
        }

        /// <summary>
        /// Gets or sets the subscription identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the entitlement identifier.
        /// </summary>
        public string EntitlementId { get; set; }

        /// <summary>
        /// Gets or sets the offer identifier.
        /// </summary>
        public string OfferId { get; set; }

        /// <summary>
        /// Gets or sets the offer name.
        /// </summary>
        public string OfferName { get; set; }

        /// <summary>
        /// Gets or sets the friendly name for the subscription.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the product type for the subscription.
        /// Note: This property will only be populated for modern products in R2 until legacy is supported and other names are confirmed.
        /// </summary>
        /// <value>
        /// Product type of a subscription.
        /// </value>
        public ItemType ProductType { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// For example, in case of seat based billing, this property is set to seat count.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the units defining Quantity for the subscription.
        /// </summary>
        public string UnitType { get; set; }

        /// <summary>
        /// Gets or sets the parent subscription identifier.
        /// </summary>
        public string ParentSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the consumption type.
        /// </summary>
        public string ConsumptionType { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the effective start date for this subscription. It is used to back date a migrated subscription or to align it with another.
        /// </summary>
        public DateTime EffectiveStartDate { get; set; }

        /// <summary>
        /// Gets or sets the commitment end date for this subscription. For subscriptions which are not auto renewable, this represents a date far away in the future.
        /// </summary>
        public DateTime CommitmentEndDate { get; set; }

        /// <summary>
        /// Gets or sets the commitment end date time for this subscription, in date-time format.
        /// </summary>
        /// <value>
        /// The commitment end date time.
        /// </value>
        public DateTime? CommitmentEndDateTime { get; set; }

        /// <summary>
        /// Gets or sets the last date that the partner can cancel their subscription for the given term, in UTC date-time format.
        /// For subscriptions that are not allowed to be cancelled, this value is null.
        /// For subscriptions that can be cancelled at any time, this value is term end date.
        /// </summary>
        /// <value>
        /// The last date for cancellation.
        /// </value>
        public DateTime? CancellationAllowedUntilDate { get; set; }

        /// <summary>
        /// Gets or sets the billing cycle end date for term-based subscription, in date-time format.
        /// </summary>
        /// <value>
        /// The billing cycle end date.
        /// </value>
        public DateTime? BillingCycleEndDate { get; set; }

        /// <summary>
        /// Gets or sets the billing cycle end date time for term-based subscription, in date-time format.
        /// </summary>
        /// <value>
        /// The billing cycle end date time.
        /// </value>
        public DateTime? BillingCycleEndDateTime { get; set; }

        /// <summary>
        /// Gets or sets the subscription status.
        /// </summary>
        public SubscriptionStatus Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether automatic renew is enabled or not.
        /// </summary>
        public bool AutoRenewEnabled { get; set; }

        /// <summary>
        /// Gets or sets the billing type.
        /// </summary>
        public BillingType BillingType { get; set; }

        /// <summary>
        /// Gets or sets the billing cycle. Defines how often the partner is billed for this subscription.
        /// </summary>
        public BillingCycleType BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the subscription has purchasable add-ons.
        /// </summary>
        public bool HasPurchasableAddons { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the subscription is a trial.
        /// </summary>
        public bool IsTrial { get; set; }

        /// <summary>
        /// Gets or sets the actions.
        /// </summary>
        public IEnumerable<string> Actions { get; set; }

        /// <summary>
        /// Gets or sets the term duration.
        /// </summary>
        public string TermDuration { get; set; }

        /// <summary>
        /// Gets or sets the renewal term duration.
        /// </summary>
        public string RenewalTermDuration { get; set; }

        /// <summary>
        /// Gets or sets the refund options for this subscription if applicable.
        /// </summary>
        /// <value>
        /// The refund options.
        /// </value>
        public IEnumerable<RefundOption> RefundOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the value indicating that the availability is a Microsoft product.
        /// </summary>
        public bool IsMicrosoftProduct { get; set; }

        #region Value-add Partner scenarios

        /// <summary>
        /// Gets or sets the MPN identifier. This only applies to indirect partner scenarios.
        /// </summary>
        public string PartnerId { get; set; }

        #endregion

        #region Suspension properties

        /// <summary>
        /// Gets or sets the suspension reason.
        /// </summary>
        public IEnumerable<string> SuspensionReasons { get; set; }

        #endregion

        /// <summary>
        /// Gets the type of contract.
        /// </summary>
        public override ContractType ContractType
        {
            get { return ContractType.Subscription; }
        }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        public SubscriptionLinks Links { get; set; }

        /// <summary>
        /// Gets or sets the publisher name.
        /// </summary>
        public string PublisherName { get; set; }

        /// <summary>
        /// Gets or sets the promotion id.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PromotionId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the Traditional-Commerce subscription which the New-Commerce subscription was migrated from.
        /// </summary>
        /// <value>The ID of the Traditional-Commerce subscription which the New-Commerce subscription was migrated from.</value>
        public string MigratedFromSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the number of seats eligible for reduction at the time of the request.
        /// </summary>
        public RefundableQuantity RefundableQuantity { get; set; }

        /// <summary>
        /// Gets or sets the next term instructions for scheduled changes during the next auto renewal (not applicable for legacy offers).
        /// </summary>
        /// <value>
        /// The scheduled next term instructions.
        /// </value>
        public ScheduledNextTermInstructions ScheduledNextTermInstructions { get; set; }

        /// <summary>
        /// Gets or sets the next charge instructions for billing cycle changes during the next charge cycle (not applicable for legacy offers).
        /// </summary>
        /// <value>
        /// The next charge instructions.
        /// </value>
        public NextChargeInstructions NextChargeInstructions { get; set; }
    }
}