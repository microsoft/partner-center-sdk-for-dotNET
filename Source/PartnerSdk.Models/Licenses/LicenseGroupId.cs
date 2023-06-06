// -----------------------------------------------------------------------
// <copyright file="LicenseGroupId.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Licenses
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// License Group Id.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum LicenseGroupId
    {
        /// <summary>
        /// Indicates nothing - no type, used as an initializer.
        /// If any invalid value is passed, then it will be defaulted to this.
        /// </summary>
        None,

        /// <summary>
        /// Azure Active Directory
        /// This indicates that product SKU is managed by Azure AD.
        /// </summary>
        Group1,

        /// <summary>
        /// Store for Business
        /// This indicates that product SKU is not managed by Azure AD.
        /// </summary>
        Group2
    }
}
