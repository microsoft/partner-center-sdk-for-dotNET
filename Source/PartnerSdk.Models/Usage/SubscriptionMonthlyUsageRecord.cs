// -----------------------------------------------------------------------
// <copyright file="SubscriptionMonthlyUsageRecord.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Usage
{
    using Subscriptions;

    /// <summary>
    /// Defines the monthly usage record of a subscription.
    /// </summary>
    public sealed class SubscriptionMonthlyUsageRecord : UsageRecordBase
    {
        /// <summary>
        /// Gets or sets the subscription Id.
        /// </summary>
        public new string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the subscription name.
        /// </summary>
        public new string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the estimated total cost of usage for the subscription.
        /// </summary>
        public new decimal TotalCost { get; set; }

        /// <summary>
        /// Gets or sets the status of the subscription.
        /// </summary>
        public SubscriptionStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the partner on record.
        /// </summary>
        public string PartnerOnRecord { get; set; }

        /// <summary>
        /// Gets or sets the offer id associated to this subscription.
        /// </summary>
        public string OfferId { get; set; }
    }
}
