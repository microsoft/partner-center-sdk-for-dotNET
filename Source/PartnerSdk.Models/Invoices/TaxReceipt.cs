// -----------------------------------------------------------------------
// <copyright file="TaxReceipt.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    using System;

    /// <summary>
    /// Represent tax receipt details.
    /// </summary>
    public class TaxReceipt
    {
        /// <summary>
        /// Gets or sets the tax receipt unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the tax receipt download link.
        /// </summary>
        public Uri TaxReceiptPdfDownloadLink { get; set; }
    }
}
