//----------------------------------------------------------------
// <copyright file="ServiceRequestStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ServiceRequests
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Describes service request status.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum ServiceRequestStatus
    {
        /// <summary>
        /// Default Service Request Status.
        /// </summary>
        None = 0,

        /// <summary>
        /// An opened service request.
        /// </summary>
        Open = 1,

        /// <summary>
        /// A closed service request.
        /// </summary>
        Closed = 2,

        /// <summary>
        /// A service request that needs attention.
        /// </summary>
        AttentionNeeded = 3
    }
}