// -----------------------------------------------------------------------
// <copyright file="UserState.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Users
{
    using Newtonsoft.Json;
    using PartnerCenter.Models.JsonConverters;

    /// <summary>
    /// User state.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum UserState
    {
        /// <summary>
        /// Active user.
        /// </summary>
        Active = 0,

        /// <summary>
        /// Inactive user.
        /// </summary>
        Inactive
    }
}
