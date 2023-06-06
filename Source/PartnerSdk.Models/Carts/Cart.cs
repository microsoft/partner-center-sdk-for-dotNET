// -----------------------------------------------------------------------
// <copyright file="Cart.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Carts
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class represents a model of a cart object.
    /// </summary>
    public class Cart : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets a unique cart identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the creation timestamp.
        /// </summary>
        public DateTime? CreationTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the last modified timestamp.
        /// </summary>
        public DateTime? LastModifiedTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the expiration timestamp.
        /// </summary>
        public DateTime ExpirationTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the last modified user or application.
        /// </summary>
        public string LastModifiedUser { get; set; }

        /// <summary>
        /// Gets or sets a collection of cart line items.
        /// </summary>
        public IEnumerable<CartLineItem> LineItems { get; set; }

        /// <summary>
        /// Gets or sets the cart status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the PartnerOnRecord attestation was accepted.
        /// </summary>
        /// <value>The value of the PartnerOnRecord attestation acceptance.</value>
        public bool? PartnerOnRecordAttestationAccepted { get; set; }
    }
}
