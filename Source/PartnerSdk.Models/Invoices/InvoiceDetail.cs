// -----------------------------------------------------------------------
// <copyright file="InvoiceDetail.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    /// <summary>
    /// Represents the detailed information of an invoice.
    /// </summary>
    public sealed class InvoiceDetail : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets the type of invoice detail.
        /// </summary>
        public InvoiceLineItemType InvoiceLineItemType { get; set; }

        /// <summary>
        /// Gets or sets the billing provider.
        /// </summary>
        public BillingProvider BillingProvider { get; set; }
    }
}
