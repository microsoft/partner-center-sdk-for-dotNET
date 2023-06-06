// <copyright file="NewCommerceEligibilityError.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations
{

    /// <summary>
    /// Represents an error that makes a subscription ineligible for New-Commerce migration.
    /// </summary>
    public class NewCommerceEligibilityError : ResourceBase
    {
        /// <summary>
        /// Gets or sets the error code associated with the issue.
        /// </summary>
        /// <value>
        /// The error code associated with the issue.
        /// </value>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets friendly text describing the error.
        /// </summary>
        /// <value>
        /// Friendly text describing the error.
        /// </value>
        public string Description { get; set; }
    }
}
