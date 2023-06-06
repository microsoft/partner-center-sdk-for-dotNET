// -----------------------------------------------------------------------
// <copyright file="OrderAttachment.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    using System.Collections.Generic;

    /// <summary>
    /// Order attachment details.
    /// </summary>
    public class OrderAttachment : ResourceBase
    {
        /// <summary>
        /// Gets or sets the attachment id.
        /// </summary>
        public string AttachmentId { get; set; }

        /// <summary>
        /// Gets or sets file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets file size in kb.
        /// </summary>
        public int SizeInKB { get; set; }

        /// <summary>
        /// Gets or sets the attachment type.
        /// </summary>
        public string AttachmentType { get; set; }
    }
}