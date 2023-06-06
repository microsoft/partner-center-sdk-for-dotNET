// -----------------------------------------------------------------------
// <copyright file="BillingCycleType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.PromotionEligibilities.Enums
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// The billing cycle type enum.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum BillingCycleType
    {
        /// <summary>
        /// Enum initializer
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Indicates that the partner will be charged monthly.
        /// </summary>
        Monthly = 1,

        /// <summary>
        /// Indicates that the partner will be charged annually.
        /// </summary>
        Annual = 2,

        /// <summary>
        /// Indicates that the partner will not be charged.
        /// This value is used for trial offers.
        /// </summary>
        None = 3,

        /// <summary>
        /// Indicates that the partner will be charged one time.
        /// This value is used for modern product skus.
        /// </summary>
        OneTime = 4,

        /// <summary>
        /// Indicates that the partner will be charged every three years for the subscription.
        /// </summary>
        Triennial = 5,

        /// <summary>
        /// Indicates that the partner will be charged every two years for the subscription.
        /// </summary>
        Biennial = 6,
    }
}
