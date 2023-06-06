// <copyright file="GetNewCommerceMigrationsResponse.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the response from the Get New-Commerce migrations API.
    /// </summary>
    public class GetNewCommerceMigrationsResponse : ResourceBase
    {
        /// <summary>
        /// Gets or sets the New-Commerce migrations.
        /// </summary>
        /// <value>
        /// The New-Commerce migrations.
        /// </value>
        public IEnumerable<NewCommerceMigration> NewCommerceMigrations { get; set; }

        /// <summary>
        /// Gets or sets the Continuation Token.
        /// </summary>
        /// <value>
        /// The Continuation Token.
        /// </value>
        public string ContinuationToken { get; set; }
    }
}
