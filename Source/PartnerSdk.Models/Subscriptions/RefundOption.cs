// -----------------------------------------------------------------------
// <copyright file="RefundOption.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System;

    /// <summary>
    /// Class that represents a refund option.
    /// </summary>
    public class RefundOption
    {
        /// <summary>
        /// Gets or sets the type of refund ("Full, Partial")
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when this policy expires if applicable, null otherwise.
        /// </summary>
        public DateTime? ExpiresAt { get; set; }
    }
}
