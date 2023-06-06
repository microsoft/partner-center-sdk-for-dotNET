//----------------------------------------------------------------
// <copyright file="SubscriptionStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Lists the available states for a subscription.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum SubscriptionStatus
    {
        /// <summary>
        /// Indicates nothing - no status, used as an initializer.
        /// </summary>
        None = 0,

        /// <summary>
        /// Active subscription.
        /// </summary>
        Active = 1,

        /// <summary>
        /// Suspended subscription.
        /// </summary>
        Suspended = 2,

        /// <summary>
        /// Deleted subscription.
        /// </summary>
        Deleted = 3,

        /// <summary>
        /// Subscription state: Expired
        /// </summary>
        Expired = 4,

        /// <summary>
        /// Subscription state: Pending
        /// </summary>
        Pending = 5,

        /// <summary>
        /// Subscription state: Disabled
        /// </summary>
        Disabled = 6,
    }
}