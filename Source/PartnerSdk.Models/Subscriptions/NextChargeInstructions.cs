// <copyright file="NextChargeInstructions.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    /// <summary>
    /// The next charge instructions to be applied next charge cycle.
    /// </summary>
    public class NextChargeInstructions
    {
        /// <summary>
        /// Gets or sets the future product and term ID.
        /// * Required.
        /// </summary>
        /// <value>The future product and term ID.</value>
        public ProductTerm Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity to convert to at the next charge cycle.
        /// * Required.
        /// </summary>
        /// <value>The quantity to convert to at the next charge cycle.</value>
        public int Quantity { get; set; }
    }
}