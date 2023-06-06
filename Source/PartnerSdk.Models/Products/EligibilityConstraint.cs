// <copyright file="EligibilityConstraint.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class represents eligibility constraint.
    /// </summary>
    public class EligibilityConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EligibilityConstraint"/> class.
        /// </summary>
        public EligibilityConstraint()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether constraint is applicable.
        /// </summary>
        public bool IsApplicable { get; set; }

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Will not change name due to a currently existing contract.")]
        public string Type { get; set; }
    }
}
