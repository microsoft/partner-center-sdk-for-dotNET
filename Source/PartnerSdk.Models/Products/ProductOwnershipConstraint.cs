// <copyright file="ProductOwnershipConstraint.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class represent product ownership constraint.
    /// </summary>
    public class ProductOwnershipConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductOwnershipConstraint"/> class.
        /// </summary>
        public ProductOwnershipConstraint()
        {
        }

        /// <summary>
        /// Gets or sets BigId.
        /// </summary>
        public string BigId { get; set; }

        /// <summary>
        /// Gets or sets MinSeats.
        /// </summary>
        public int MinSeats { get; set; }

        /// <summary>
        /// Gets or sets MaxSeats.
        /// </summary>
        public int MaxSeats { get; set; }
    }
}
