//----------------------------------------------------------------
// <copyright file="VettingSubStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Partners
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Vetting sub status
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum VettingSubStatus
    {
        /// <summary>
        /// None vetting sub status
        /// </summary>
        None = 0,

        /// <summary>
        /// The entry step
        /// </summary>
        Entry = 1,

        /// <summary>
        /// Email ownership check for business accounts
        /// </summary>
        EmailOwnership = 2,

        /// <summary>
        /// Email Domain for business accounts
        /// </summary>
        EmailDomain = 3,

        /// <summary>
        /// Employment verification sub status
        /// </summary>
        EmploymentVerification = 4,

        /// <summary>
        /// Decision making process
        /// </summary>
        Decision = 5,

        /// <summary>
        /// Other vetting sub status
        /// </summary>
        Other = 6,

        /// <summary>
        /// Business verification sub status
        /// </summary>
        BusinessVerification = 7
    }
}
