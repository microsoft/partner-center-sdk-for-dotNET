// -----------------------------------------------------------------------
// <copyright file="SortDirection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Query
{
    using System.ComponentModel;

    /// <summary>
    /// Specifies sort direction.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Ascending sort.
        /// </summary>
        [Description("asc")]
        Ascending,

        /// <summary>
        /// Descending sort.
        /// </summary>
        [Description("desc")]
        Descending
    }
}
