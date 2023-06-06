//----------------------------------------------------------------
// <copyright file="PolicyCategory.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.DevicesDeployment
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the policy type that can be assigned to a device. 
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum PolicyCategory
    {
        /// <summary>
        /// Default settings for the policy. 
        /// </summary>
        None = 0,

        /// <summary>
        /// OOBE - Out of box experience policy setting. 
        /// </summary>
        OOBE = 1,
    }
}
