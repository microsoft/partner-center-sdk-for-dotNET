// -----------------------------------------------------------------------
// <copyright file="CountQuery.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Query
{
    using System;

    /// <summary>
    /// A query that returns the number of entities that may optionally fit a filter.
    /// </summary>
    internal class CountQuery : BaseQuery
    {
        /// <summary>
        /// Gets the query type.
        /// </summary>
        public override QueryType Type
        {
            get
            {
                return QueryType.Count;
            }
        }

        /// <summary>
        /// Gets or sets the query filter.
        /// </summary>
        public override FieldFilter Filter { get; set; }

        /// <summary>
        /// Returns a string representation of the query.
        /// </summary>
        /// <returns>A string representation of the query.</returns>
        public override string ToString()
        {
            return (this.Filter != null) ? this.Filter.ToString() : base.ToString();
        }
    }
}