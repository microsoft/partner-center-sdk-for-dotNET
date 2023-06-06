// -----------------------------------------------------------------------
// <copyright file="ParticipantType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Carts.Enums
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Types of Participants
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum ParticipantType
    {
        /// <summary>
        /// Default value if not known
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// An indirect reseller with a transaction role.
        /// </summary>
        TransactionReseller = 1,

        /// <summary>
        /// An indirect reseller with a consumption role.
        /// </summary>
        ConsumptionReseller = 2,

        /// <summary>
        /// An indirect reseller with a secondary transaction role.
        /// </summary>
        AdditionalTransactionReseller = 3,
    }
}
