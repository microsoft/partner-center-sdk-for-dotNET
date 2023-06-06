// -----------------------------------------------------------------------
// <copyright file="ScheduledNextTermInstructions.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System;

    /// <summary>
    /// The next term instructions to be applied on renewal.
    /// </summary>
    public class ScheduledNextTermInstructions
    {
        /// <summary>
        /// Gets or sets the future product and term ID.
        /// * Required.
        /// </summary>
        /// <value>The future product and term ID.</value>
        public ProductTerm Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity to convert to at the next term.
        /// * Required.
        /// </summary>
        /// <value>The quantity to convert to at the next term.</value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the custom term end date if applicable.
        /// *Optional.
        /// </summary>
        /// <value>
        /// The custom term end date if applicable.
        /// </value>
        public DateTime? CustomTermEndDate { get; set; }
    }
}