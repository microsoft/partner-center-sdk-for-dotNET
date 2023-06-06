//----------------------------------------------------------------
// <copyright file="VettingStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Partners
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Enumeration of vetting status
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum VettingStatus
    {
        /// <summary>
        /// None vetting status
        /// </summary>
        None = 0,

        /// <summary>
        /// Pending vetting status
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Authorized vetting status
        /// </summary>
        Authorized = 2,

        /// <summary>
        /// Rejected vetting status
        /// </summary>
        Rejected = 3
    }
}