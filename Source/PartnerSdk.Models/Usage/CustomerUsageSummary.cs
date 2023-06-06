// -----------------------------------------------------------------------
// <copyright file="CustomerUsageSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Usage
{
    /// <summary>
    /// Defines the usage summary for a specific customer.
    /// </summary>
    public sealed class CustomerUsageSummary : UsageSummaryBase
    {
        /// <summary>
        /// Gets or sets the azure active directory tenant Id of the customer which this usage summary applies to.
        /// </summary>
        public new string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer which this usage summary applies to.
        /// </summary>
        public new string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the spending budget allocated to the customer.
        /// </summary>
        public SpendingBudget Budget { get; set; }
    }
}
