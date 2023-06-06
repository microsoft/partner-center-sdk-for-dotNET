// -----------------------------------------------------------------------
// <copyright file="CustomerMonthlyUsageRecord.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Usage
{
    /// <summary>
    /// This class defines the monthly usage record of a customer for all its subscriptions.
    /// </summary>
    public sealed class CustomerMonthlyUsageRecord : UsageRecordBase
    {
        /// <summary>
        /// Gets or sets azure active directory tenant ID of the customer which this monthly usage record applies to.
        /// </summary>
        public new string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets azure name of the customer which this monthly usage record applies to.
        /// </summary>
        public new string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the estimated total cost of usage for the customer.
        /// </summary>
        public new decimal TotalCost { get; set; }

        /// <summary>
        /// Gets or sets the spending budget allocated for the customer.
        /// </summary>
        public SpendingBudget Budget { get; set; }

        /// <summary>
        /// Gets or sets the percentage used out of the allocated budget.
        /// </summary>
        public decimal PercentUsed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer's azure subscription is upgraded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this customer's azure subscription is upgraded; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpgraded { get; set; }
    }
}
