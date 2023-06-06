// -----------------------------------------------------------------------
// <copyright file="SelfServeEntity.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.SelfServePolicies
{
    /// <summary>
    /// Represents Entity.
    /// This currently represents the customer as the entity. In the future we could have this be a reseller as well.
    /// </summary>
    public class SelfServeEntity
    {
        /// <summary>
        /// Gets or sets the EntityType.
        /// Ex: Customer
        /// </summary>
        public string SelfServeEntityType { get; set; }

        /// <summary>
        /// Gets or sets the TenantID.
        /// </summary>
        public string TenantID { get; set; }
    }
}
