﻿// -----------------------------------------------------------------------
// <copyright file="InvoiceLineItem.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a line item on an invoice.
    /// </summary>
    [JsonConverter(typeof(InvoiceLineItemConverter))]
    public abstract class InvoiceLineItem : ResourceBase
    {
        /// <summary>
        /// Gets the type of invoice line item.
        /// </summary>
        public abstract InvoiceLineItemType InvoiceLineItemType { get; }

        /// <summary>
        /// Gets the billing provider.
        /// </summary>
        public abstract BillingProvider BillingProvider { get; }
    }
}
