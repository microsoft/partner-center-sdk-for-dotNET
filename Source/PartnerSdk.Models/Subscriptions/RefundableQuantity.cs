// <copyright file="RefundableQuantity.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System.Collections.Generic;

    /// <summary>
    /// The number of seats eligible for reduction at the time of the request.
    /// </summary>
    public class RefundableQuantity
    {
        /// <summary>
        /// Gets or sets the total number of seats that are eligible for reduction at the time of the request.
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// Gets or sets the breakdown of the eligible seats for reduction with their return windows.
        /// </summary>
        public IEnumerable<RefundableQuantityDetail> Details { get; set; }
    }
}