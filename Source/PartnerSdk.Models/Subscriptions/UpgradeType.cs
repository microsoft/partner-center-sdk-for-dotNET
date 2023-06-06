// -----------------------------------------------------------------------
// <copyright file="UpgradeType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Lists upgrade options for a subscription.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum UpgradeType
    {
        /// <summary>
        /// Default value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Subscription upgrade only.
        /// </summary>
        UpgradeOnly = 1,

        /// <summary>
        /// Subscription upgrade and license transfer.
        /// </summary>
        UpgradeWithLicenseTransfer = 2
    }
}
