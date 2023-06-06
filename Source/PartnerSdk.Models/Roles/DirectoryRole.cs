// -----------------------------------------------------------------------
// <copyright file="DirectoryRole.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Roles
{
    using PartnerCenter.Models;

    /// <summary>
    /// Represents a customer directory role object.
    /// </summary>
    public sealed class DirectoryRole : ResourceBase
    {
        /// <summary>
        /// Gets or sets the name of the directory role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the id of the directory role.
        /// </summary>
        public string Id { get; set; }
    }
}
