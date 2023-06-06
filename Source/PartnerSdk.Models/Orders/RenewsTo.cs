// <copyright file="RenewsTo.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    /// <summary>
    /// Represents RenewsTo of an order lineItem.
    /// </summary>
    public class RenewsTo
    {
        /// <summary>
        /// Gets or sets the term duration if applicable.
        /// </summary>
        /// <value>
        /// The duration if applicable.
        /// </value>
        public string TermDuration { get; set; }
    }
}
