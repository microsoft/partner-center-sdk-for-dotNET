//-----------------------------------------------------------------------
// <copyright file="LicenseUpdate.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Licenses
{
    using System;
    using System.Collections.Generic;
    using PartnerCenter.Models;

    /// <summary>
    /// Model for assigning and removing licenses to user.
    /// </summary>
    public sealed class LicenseUpdate : ResourceBase
    {
        /// <summary>
        /// Gets or sets the list of licenses to be assigned.
        /// </summary>
        public IEnumerable<LicenseAssignment> LicensesToAssign { get; set; }

        /// <summary>
        /// Gets or sets the list of license ids to be removed.
        /// </summary>
        public IEnumerable<string> LicensesToRemove { get; set; }

        /// <summary>
        /// Gets list of warnings that occurred during license assignment. This is a read only property.
        /// </summary>
        public IEnumerable<LicenseWarning> LicenseWarnings
        {
            get; private set;
        }
    }
}