// -----------------------------------------------------------------------
// <copyright file="Constraint.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    /// <summary>
    /// The Constraint model.
    /// </summary>
    public class Constraint
    {
        /// <summary>
        /// Gets or sets the constraint name.
        /// </summary>
        public string ConstraintName { get; set; }

        /// <summary>
        /// Gets or sets the constraint value.
        /// </summary>
        public double ConstraintValue { get; set; }

        /// <summary>
        /// Gets or sets the constraint type.
        /// </summary>
        public string ConstraintType { get; set; }
    }
}