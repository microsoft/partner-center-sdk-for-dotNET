// -----------------------------------------------------------------------
// <copyright file="ValidationType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ValidationStatus.Enums
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Types of validation.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum ValidationType
    {
        /// <summary>
        /// Indicates the account status.
        /// </summary>
        Account,
    }
}
