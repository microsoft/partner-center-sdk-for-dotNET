// -----------------------------------------------------------------------
// <copyright file="SubscriptionRegistrationStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    /// <summary>
    /// Provides information about the registration status of a subscription.
    /// </summary>
    public class SubscriptionRegistrationStatus : ResourceBase
    {
        /// <summary>
        /// Gets or sets a GUID formatted string that identifies the subscription.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this subscription is registered.
        /// </summary>
        public string Status { get; set; }
    }
}
