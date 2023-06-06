//----------------------------------------------------------------
// <copyright file="InvoiceLineItemType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Lists invoice line item types.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum InvoiceLineItemType
    {
        /// <summary>
        /// Default value.
        /// </summary>
        None,

        /// <summary>
        /// Daily usage information associated with an invoice. This information does not contain data about cost per use.
        /// </summary>
        UsageLineItems,

        /// <summary>
        /// Billing line items associated with an invoice. Contains information such as cost per use, tax charged, etc.
        /// </summary>
        BillingLineItems
    }
}
