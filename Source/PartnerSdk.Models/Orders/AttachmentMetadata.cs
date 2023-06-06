// -----------------------------------------------------------------------
// <copyright file="AttachmentMetadata.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{

    /// <summary>
    /// Customer purchase order details expressed as metadata for the order attachment.
    /// </summary>
    public class AttachmentMetadata
    {
        /// <summary>
        /// Gets or sets the price to the customer.
        /// </summary>
        public string CustomerPrice { get; set; }

        /// <summary>
        /// Gets or sets foreign exchange rate.
        /// </summary>
        public string FxRate { get; set; }

        /// <summary>
        /// Gets or sets currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is part of a tender.
        /// </summary>
        public bool IsPartOfTender { get; set; }

        /// <summary>
        /// Gets or sets tender link url.
        /// </summary>
        public string TenderLink { get; set; }
    }
}