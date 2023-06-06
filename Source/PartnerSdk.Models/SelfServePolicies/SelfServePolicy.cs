// -----------------------------------------------------------------------
// <copyright file="SelfServePolicy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.SelfServePolicies
{
    /// <summary>
    /// Represents SelfServePolicy.
    /// </summary>
    public class SelfServePolicy : ResourceBase
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Entity.
        /// </summary>
        public SelfServeEntity SelfServeEntity { get; set; }

        /// <summary>
        /// Gets or sets the Grantor.
        /// </summary>
        public Grantor Grantor { get; set; }

        /// <summary>
        /// Gets or sets the Permissions.
        /// </summary>
        public Permission[] Permissions { get; set; }
    }
}
