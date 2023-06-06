//----------------------------------------------------------------
// <copyright file="BillingCycleType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Offers
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// The way billing is processed for a subscription.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum BillingCycleType
    {
        /// <summary>
        /// Enum initializer
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Indicates that the partner will be charged monthly for the subscription
        /// </summary>
        Monthly = 1,

        /// <summary>
        /// Indicates that the partner will be charged annually for the subscription
        /// </summary>
        Annual = 2,

        /// <summary>
        /// Indicates that the partner will not be charged monthly for the subscription
        /// This value may be used for trial offers
        /// </summary>
        None = 3,

        /// <summary>
        /// Indicates that the partner will be charged one-time
        /// This value does not apply to all products.
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