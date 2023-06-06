//----------------------------------------------------------------
// <copyright file="ServiceRequestSearchField.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ServiceRequests
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Lists the supported service request search fields.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum ServiceRequestSearchField
    {
        /// <summary>
        /// Service request status.
        /// </summary>
        Status
    }
}