﻿// -----------------------------------------------------------------------
// <copyright file="ValidationCode.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ValidationCodes
{
    /// <summary>
    /// Represents validation codes. Used to create Government Community Cloud (GCC) accounts.
    /// </summary>
    public class ValidationCode
    {
        /// <summary>
        /// Gets or sets the partner's ID.
        /// </summary>
        public string PartnerId { get; set; }

        /// <summary>
        /// Gets or sets the organization name.
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets the validation ID.
        /// </summary>
        public string ValidationId { get; set; }

        /// <summary>
        /// Gets or sets number of maximum customer creates for this code.
        /// </summary>
        public int? MaxCreates { get; set; }

        /// <summary>
        /// Gets or sets the number of remaining customer creates for this code.
        /// </summary>
        public int? RemainingCreates { get; set; }

        /// <summary>
        /// Gets or sets the objects ETag
        /// </summary>
        public string ETag { get; set; }
    }
}
