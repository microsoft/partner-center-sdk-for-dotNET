//----------------------------------------------------------------
// <copyright file="ServiceIncidentStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ServiceIncidents
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the status of partner center services.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum ServiceIncidentStatus
    {
        /// <summary>
        /// Default status - normal.
        /// </summary>
        Normal = 0,
      
        /// <summary>
        /// Informational status.
        /// </summary>
        Information = 1,

        /// <summary>
        /// Warning status.
        /// </summary>
        Warning = 2,
   
        /// <summary>
        /// Critical status.
        /// </summary>
        Critical = 3,
    }
}
