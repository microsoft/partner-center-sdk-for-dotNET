// <copyright file="CancellationPolicy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents a cancellation policy.
    /// </summary>
    public class CancellationPolicy
    {
        /// <summary>
        /// Gets or sets the refund options that can apply to this cancellation policy.
        /// </summary>
        public IEnumerable<CancellationRefundOption> RefundOptions { get; set; }
    }
}
