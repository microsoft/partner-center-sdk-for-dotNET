//----------------------------------------------------------------
// <copyright file="ProvisioningStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Lists the available status for a subscription provisioning status.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum ProvisioningStatus
    {
        /// <summary>
        /// Default value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Subscription provisioning status success.
        /// </summary>
        Success = 1,

        /// <summary>
        /// Subscription provisioning status pending.
        /// </summary>
        Pending = 2,

        /// <summary>
        /// Subscription provisioning status failed.
        /// </summary>
        Failed = 3,
    }
}
