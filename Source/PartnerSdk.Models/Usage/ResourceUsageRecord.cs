// -----------------------------------------------------------------------
// <copyright file="ResourceUsageRecord.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Usage
{
    /// <summary>
    /// Defines the estimated monetary cost of a subscription's resource level usage in the current billing cycle.
    /// </summary>
    public class ResourceUsageRecord : UsageRecordBase
    {
        /// <summary>
        /// Gets or sets the subscription Id.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the resource Uri.
        /// </summary>
        public string ResourceUri { get; set; }

        /// <summary>
        /// Gets or sets the resource type.
        /// </summary>
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the entitlement Id.
        /// </summary>
        public string EntitlementId { get; set; }

        /// <summary>
        /// Gets or sets the entitlement name.
        /// </summary>
        public string EntitlementName { get; set; }

        /// <summary>
        /// Gets or sets the resource group name.
        /// </summary>
        public string ResourceGroupName { get; set; }
    }
}