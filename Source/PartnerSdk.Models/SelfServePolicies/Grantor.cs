// -----------------------------------------------------------------------
// <copyright file="Grantor.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.SelfServePolicies
{
    /// <summary>
    /// Represents Grantor.
    /// </summary>
    public class Grantor
    {
        /// <summary>
        /// Gets or sets the GrantorType.
        /// Ex: BillToPartner
        /// </summary>
        public string GrantorType { get; set; }

        /// <summary>
        /// Gets or sets the TenantID.
        /// </summary>
        public string TenantID { get; set; }
    }
}
