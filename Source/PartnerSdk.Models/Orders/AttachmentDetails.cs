// -----------------------------------------------------------------------
// <copyright file="AttachmentDetails.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    /// <summary>
    /// Information needed for the attachment of an order line item.
    /// </summary>
    public class AttachmentDetails
    {
        /// <summary>
        /// Gets or sets metadata information.
        /// </summary>
        public AttachmentMetadata Metadata { get; set; }

        /// <summary>
        /// Gets or sets PO files.
        /// </summary>
        public List<IFormFile> POFiles { get; set; }

        /// <summary>
        /// Gets or sets Tender files.
        /// </summary>
        public List<IFormFile> TenderFiles { get; set; }
    }
}