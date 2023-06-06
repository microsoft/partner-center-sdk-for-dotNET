//-----------------------------------------------------------------------
// <copyright file="LicenseWarning.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Licenses
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model for license warnings.
    /// </summary>
    public sealed class LicenseWarning
    {
        /// <summary>
        /// Gets or sets the warning code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the warning message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the list of service plan names.
        /// </summary>
        public IEnumerable<string> ServicePlans { get; set; }
    }
}
