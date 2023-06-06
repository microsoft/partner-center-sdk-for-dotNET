// -----------------------------------------------------------------------
// <copyright file="CustomerQualification.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Customers
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Customer Qualification
    /// When a partner creates a new customer by default the customer is assigned "CustomerQualification.None". If the partner validates that the customer 
    /// belongs to Education segment they can set the qualification of the Customer to "CustomerQualification.Education". This operation is irreversible and 
    /// the partner will not be allowed to override the customer qualification once set.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum CustomerQualification
    {
        /// <summary>
        /// No Qualification
        /// </summary>
        None,

        /// <summary>
        /// Education Qualification
        /// </summary>
        Education,

        /// <summary>
        /// Non-Profit / Charity Qualification
        /// </summary>
        Nonprofit,

        /// <summary>
        /// Government Community Cloud (GCC)
        /// </summary>
        GovernmentCommunityCloud,

        /// <summary>
        /// State Owned Entity (SOE)
        /// </summary>
        StateOwnedEntity,
    }
}
