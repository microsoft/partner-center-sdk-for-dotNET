// <copyright file="CancellationRefundOption.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    /// <summary>
    /// Class that represents a refund policy.
    /// </summary>
    public class CancellationRefundOption
    {
        /// <summary>
        /// Gets or sets the id that represents the position of this refund option (relative to other sibling refund options).
        /// </summary>
        public int SequenceId { get; set; }

        /// <summary>
        /// Gets or sets the type of refund ("Full, Partial")
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the ISO standard representation of the duration allowed before this refund option expires.
        /// </summary>
        public string ExpiresAfter { get; set; }
    }
}
