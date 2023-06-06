// -----------------------------------------------------------------------
// <copyright file="PartnerUsageSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Usage
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the usage summary of a partner for all its customers with an Azure subscription.
    /// </summary>
    public sealed class PartnerUsageSummary : UsageSummaryBase
    {
        /// <summary>
        /// Gets or sets the azure active directory tenant Id of the partner this usage summary applies to.
        /// </summary>
        public new string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the partner this usage summary applies to.
        /// </summary>
        public new string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the list of email addresses for notifications.
        /// </summary>
        public IEnumerable<string> EmailsToNotify { get; set; }

        /// <summary>
        /// Gets or sets the number of customers that are over budget.
        /// </summary>
        public int CustomersOverBudget { get; set; }

        /// <summary>
        /// Gets or sets the number of customers that are close to going over budget.
        /// </summary>
        public int CustomersTrendingOver { get; set; }

        /// <summary>
        /// Gets or sets the number of customers with a usage-based subscription.
        /// </summary>
        public int CustomersWithUsageBasedSubscription { get; set; }
    }
}
