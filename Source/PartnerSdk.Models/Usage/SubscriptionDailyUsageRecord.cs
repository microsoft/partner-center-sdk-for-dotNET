// -----------------------------------------------------------------------
// <copyright file="SubscriptionDailyUsageRecord.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Usage
{
    using System;

    /// <summary>
    /// Defines the daily usage record of a specific subscription.
    /// </summary>
    public sealed class SubscriptionDailyUsageRecord : UsageRecordBase
    {
        /// <summary>
        /// Gets or sets the subscription ID.
        /// </summary>
        public new string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the subscription name.
        /// </summary>
        public new string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the estimated total cost of daily usage for the subscription.
        /// </summary>
        public new decimal TotalCost { get; set; }

        /// <summary>
        /// Gets or sets the usage date.
        /// </summary>
        public DateTimeOffset DateUsed { get; set; }
    }
}
