//----------------------------------------------------------------
// <copyright file="BillingProvider.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Different providers of billing information.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum BillingProvider
    {
        /// <summary>
        /// Enum initializer.
        /// </summary>
        None,

        /// <summary>
        /// Bill is provided by Office. Example: O365, and In-tune.
        /// </summary>
        Office,

        /// <summary>
        /// Bill is provided by Azure. Example: Azure Services.
        /// </summary>
        Azure,

        /// <summary>
        /// Bill is provided for one time purchases.
        /// </summary>
        OneTime,

        /// <summary>
        /// Indicates that the provider is marketplace
        /// </summary>
        Marketplace,

        /// <summary>
        /// Indicates that the provider is both first party and marketplace
        /// </summary>
        All
    }
}
