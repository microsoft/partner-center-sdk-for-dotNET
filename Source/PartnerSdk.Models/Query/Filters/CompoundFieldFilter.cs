// -----------------------------------------------------------------------
// <copyright file="CompoundFieldFilter.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Query
{
    using System;
    using System.Globalization;

    /// <summary>
    /// An aggregated filter. Example: (Year = 1999 AND Model = Nissan).
    /// </summary>
    public sealed class CompoundFieldFilter : FieldFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompoundFieldFilter"/> class.
        /// </summary>
        /// <param name="leftFilter">The left filter.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="rightFilter">The right filter.</param>
        public CompoundFieldFilter(FieldFilter leftFilter, FieldFilterOperation operation, FieldFilter rightFilter)
        {
            if (leftFilter == null)
            {
                throw new ArgumentException("leftFilter can't be null");
            }

            if (rightFilter == null)
            {
                throw new ArgumentException("rightFilter can't be null");
            }

            this.LeftFilter = leftFilter;
            this.RightFilter = rightFilter;
            this.Operator = operation;
        }

        /// <summary>
        /// Gets the left filter.
        /// </summary>
        public FieldFilter LeftFilter { get; private set; }

        /// <summary>
        /// Gets the right filter.
        /// </summary>
        public FieldFilter RightFilter { get; private set; }

        /// <summary>
        /// Prints the compound filter details.
        /// </summary>
        /// <returns>The compound filter details.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "( {0} {1} {2} )", this.LeftFilter, this.Operator, this.RightFilter);
        }
    }
}