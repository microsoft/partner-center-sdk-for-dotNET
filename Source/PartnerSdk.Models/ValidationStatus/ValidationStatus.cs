// <copyright file="ValidationStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.ValidationStatus
{
    using System;
    using Microsoft.Store.PartnerCenter.Models.ValidationStatus.Enums;

    /// <summary>
    /// The class which represents the validation status.
    /// </summary>
    public class ValidationStatus
    {
        /// <summary>
        /// Gets or sets the type of validation.
        /// </summary>
        public ValidationType Type { get; set; }

        /// <summary>
        /// Gets or sets the validation status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the last update date time.
        /// </summary>
        public string LastUpdateDateTime { get; set; }
    }
}
