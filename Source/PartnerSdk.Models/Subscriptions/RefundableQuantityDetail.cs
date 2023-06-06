// <copyright file="RefundableQuantityDetail.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System;

    /// <summary>
    /// The number of seats that can be reduced within the return window.
    /// </summary>
    public class RefundableQuantityDetail
    {
        /// <summary>
        /// Gets or sets the number of seats that are eligible for reduction for the given window.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the date and time until which the seats are eligible for reduction.
        /// </summary>
        public DateTime AllowedUntilDateTime { get; set; }
    }
}