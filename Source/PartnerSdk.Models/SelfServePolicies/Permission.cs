// -----------------------------------------------------------------------
// <copyright file="Permission.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.SelfServePolicies
{
    /// <summary>
    /// Represents Permission.
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Gets or sets the Resource.
        /// Ex: AzureReservedInstances
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets the Action.
        /// Ex: Purchase
        /// </summary>
        public string Action { get; set; }
    }
}
