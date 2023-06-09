﻿// -----------------------------------------------------------------------
// <copyright file="InvoiceSummaryDetail.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    /// <summary>
    /// Represent a summary of individual details for an invoice type. for example recurring, perpetual.
    /// </summary>
    public class InvoiceSummaryDetail
    {
        /// <summary>
        /// Gets or sets the invoice type. example, recurring, perpetual.
        /// </summary>
        public string InvoiceType { get; set; }

        /// <summary>
        /// Gets or sets summary on Partner's monthly bills for a particular invoice type.
        /// </summary>
        public InvoiceSummary Summary { get; set; }
    }
}