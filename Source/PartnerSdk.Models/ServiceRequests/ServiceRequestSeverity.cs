//----------------------------------------------------------------
// <copyright file="ServiceRequestSeverity.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ServiceRequests
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Describes service request severity.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum ServiceRequestSeverity
    {
        /// <summary>
        /// Unknown severity.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Critical service request impact.
        /// </summary>
        Critical = 1,

        /// <summary>
        /// Moderate service request impact.
        /// </summary>
        Moderate = 2,

        /// <summary>
        /// Minimal service request impact.
        /// </summary>
        Minimal = 3
    }
}