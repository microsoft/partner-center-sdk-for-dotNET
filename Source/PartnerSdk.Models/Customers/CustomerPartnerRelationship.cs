// -----------------------------------------------------------------------
// <copyright file="CustomerPartnerRelationship.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Customers
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Enumerates partner and customer relationships.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum CustomerPartnerRelationship
    {
        /// <summary>
        /// Unknown. Used for initialization.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A reseller relationship.
        /// </summary>
        Reseller = 1,

        /// <summary>
        /// An advisor relationship.
        /// </summary>
        Advisor = 2,

        /// <summary>
        /// Indicates that the partner is a syndication partner of the customer.
        /// </summary>
        Syndication = 3,

        /// <summary>
        /// Indicates that the partner is a Microsoft Support agent for the customer.
        /// </summary>
        MicrosoftSupport = 4,

        /// <summary>
        /// None. Used to remove reseller relationship between the customer and partner.
        /// </summary>
        None = 5
    }
}