// -----------------------------------------------------------------------
// <copyright file="Sort.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Query
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Specifies sort field and direction.
    /// </summary>
    public sealed class Sort
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sort"/> class.
        /// </summary>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        public Sort(string sortField, SortDirection sortDirection = SortDirection.Ascending)
        {
            if (string.IsNullOrWhiteSpace(sortField))
            {
                throw new ArgumentException("sortField has to be set");
            }

            this.SortField = sortField;
            this.SortDirection = sortDirection;
        }

        /// <summary>
        /// Gets the sort field.
        /// </summary>
        public string SortField { get; private set; }

        /// <summary>
        /// Gets the sort direction.
        /// </summary>
        public SortDirection SortDirection { get; private set; }

        /// <summary>
        /// Prints the sort details.
        /// </summary>
        /// <returns>The sort details.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Sort: {0}, {1}", this.SortField, this.SortDirection);
        }
    }
}
