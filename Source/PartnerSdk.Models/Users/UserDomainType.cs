//----------------------------------------------------------------
// <copyright file="UserDomainType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Users
{
    using Newtonsoft.Json;
    using PartnerCenter.Models.JsonConverters;

    /// <summary>
    /// Types of user domains supported.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum UserDomainType
    {
        /// <summary>
        /// Default - None.
        /// </summary>
        None,

        /// <summary>
        /// Password sync directories support mixed mode i.e. users that are cloud only or directory synced from an on premises directory.
        /// </summary>
        Managed,

        /// <summary>
        /// Federated domains don’t support mixed mode. Users created have to be linked to an on premises directory account with their immutable ids.
        /// </summary>
        Federated
    }
}
