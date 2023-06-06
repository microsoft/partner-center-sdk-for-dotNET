// <copyright file="SeatConstraint.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class represents seat constraint.
    /// </summary>
    public class SeatConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeatConstraint"/> class.
        /// </summary>
        public SeatConstraint()
        {
        }

        /// <summary>
        /// Gets or sets minimum seats.
        /// </summary>
        public int MinSeats { get; set; }

        /// <summary>
        /// Gets or sets maximum seats
        /// </summary>
        public int MaxSeats { get; set; }

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Will not change name due to a currently existing contract.")]
        public string Type { get; set; }
    }
}
