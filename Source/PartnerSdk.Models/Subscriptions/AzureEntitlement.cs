// -----------------------------------------------------------------------
// <copyright file="AzureEntitlement.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    /// <summary>
    /// Represents an Azure entitlement.
    /// </summary>
    public class AzureEntitlement
    {
        /// <summary>
        /// Gets or sets the id of the entitlement.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the status of the entitlement.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the friendly name of the entitlement.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the subscription id of the entitlement.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the Azure entitlement links.
        /// </summary>
        public AzureEntitlementLinks Links { get; set; }
    }
}
