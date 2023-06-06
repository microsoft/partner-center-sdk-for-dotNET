//----------------------------------------------------------------
// <copyright file="InvoiceSearchField.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Lists the supported invoice search fields.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum InvoiceSearchField
    {
        /// <summary>
        /// The invoice date.
        /// </summary>
        InvoiceDate
    }
}
