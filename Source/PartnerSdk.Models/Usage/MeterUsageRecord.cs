﻿// -----------------------------------------------------------------------
// <copyright file="MeterUsageRecord.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Usage
{
    /// <summary>
    /// Defines the estimated monetary cost of a subscription's meter level usage in the current billing cycle.
    /// </summary>
    public class MeterUsageRecord : UsageRecordBase
    {
        /// <summary>
        /// Gets or sets the subscription Id.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the meter Id.
        /// </summary>
        public string MeterId { get; set; }

        /// <summary>
        /// Gets or sets the meter name.
        /// </summary>
        public string MeterName { get; set; }

        /// <summary>
        /// Gets or sets the Azure resource category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the Azure resource sub-category.
        /// </summary>
        public string Subcategory { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the Azure resource used.
        /// </summary>
        public decimal QuantityUsed { get; set; }

        /// <summary>
        /// Gets or sets the unit of measure for the Azure resource.
        /// </summary>
        public string Unit { get; set; }
    }
}