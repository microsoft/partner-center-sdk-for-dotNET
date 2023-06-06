// -----------------------------------------------------------------------
// <copyright file="Invoice.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a monthly billing statement issued to a partner.
    /// </summary>
    public sealed class Invoice : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets the invoice unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the date the invoice was generated.
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets the billing period start date in UTC.
        /// </summary>
        public DateTime BillingPeriodStartDate { get; set; }

        /// <summary>
        /// Gets or sets the billing period end date in UTC.
        /// </summary>
        public DateTime BillingPeriodEndDate { get; set; }

        /// <summary>
        /// Gets or sets the total charges in this invoice.
        /// Total charges includes the transactions charges and any adjustments.
        /// </summary>
        public decimal TotalCharges { get; set; }

        /// <summary>
        /// Gets or sets the amount paid by the partner.
        /// Paid amount is negative if a payment is received.
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// Gets or sets the currency used for all invoice item amounts and totals.
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the currency symbol used for all invoice item amounts and totals.
        /// </summary>
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Gets or sets the link to download the invoice PDF document.
        /// This value is not returned as part of the search results, and will only  
        /// get populated if invoice is accessed by Id.
        /// This link auto expires in 30 minutes.
        /// </summary>
        public Uri PdfDownloadLink { get; set; }

        /// <summary>
        /// Gets or sets the invoice tax receipts
        /// </summary>
        public IEnumerable<TaxReceipt> TaxReceipts { get; set; }

        /// <summary>
        /// Gets or sets the invoice details.
        /// </summary>
        public IEnumerable<InvoiceDetail> InvoiceDetails { get; set; }

        /// <summary>
        /// Gets or sets the amendments.
        /// </summary>
        public IEnumerable<Invoice> Amendments { get; set; }

        /// <summary>
        /// Gets or sets the Document type of the invoice (CreditNote, Invoice).
        /// </summary>
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// Gets or sets The ref number of the document which this doc amends of.
        /// </summary>
        public string AmendsOf { get; set; }

        /// <summary>
        /// Gets or sets Invoice Type. This will be used to set invoice type to Recurring, OneTime for UI to differentiate the types of invoices.
        /// </summary>
        public string InvoiceType { get; set; }
    }
}
