// -----------------------------------------------------------------------
// <copyright file="SubscriptionUsageSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Usage
{
    /// <summary>
    /// Defines the usage summary for a specific subscription.
    /// </summary>
    public sealed class SubscriptionUsageSummary : UsageSummaryBase
    {
        /// <summary>
        /// Gets or sets the Id of the subscription which this usage summary applies to.
        /// </summary>
        public new string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the subscription which this usage summary applies to.
        /// </summary>
        public new string ResourceName { get; set; }
    }
}
