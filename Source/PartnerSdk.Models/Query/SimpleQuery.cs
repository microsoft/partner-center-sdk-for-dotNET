// -----------------------------------------------------------------------
// <copyright file="SimpleQuery.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Query
{
    using System.Text;

    /// <summary>
    /// A standard query that returns entities according to sort and filter options (Does not do paging).
    /// </summary>
    internal class SimpleQuery : BaseQuery
    {
        /// <summary>
        /// Gets the query type.
        /// </summary>
        public override QueryType Type
        {
            get
            {
                return QueryType.Simple;
            }
        }

        /// <summary>
        /// Gets or sets the query filter.
        /// </summary>
        public override FieldFilter Filter { get; set; }

        /// <summary>
        /// Gets or sets the query sorting options.
        /// </summary>
        public override Sort Sort { get; set; }

        /// <summary>
        /// Returns a string representation of the query.
        /// </summary>
        /// <returns>A string representation of the query.</returns>
        public override string ToString()
        {
            StringBuilder stringRepresentation = new StringBuilder();

            if (this.Sort != null)
            {
                stringRepresentation.AppendLine(this.Sort.ToString());
            }

            if (this.Filter != null)
            {
                stringRepresentation.AppendLine(this.Filter.ToString());
            }

            string result = stringRepresentation.ToString();
            return string.IsNullOrWhiteSpace(result) ? base.ToString() : result;
        }
    }
}
