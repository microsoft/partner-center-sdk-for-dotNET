//----------------------------------------------------------------
// <copyright file="MessageType.cs" company="Microsoft Corporation">
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
    public enum MessageType
    {
        /// <summary>
        /// Default type none.
        /// </summary>
        None = 0,

        /// <summary>
        /// Active incident.
        /// </summary>
        Incident = 1,

        /// <summary>
        /// Message center message.
        /// </summary>
        MessageCenter = 2,

        /// <summary>
        /// All types.
        /// </summary>
        All = 3,
    }
}
