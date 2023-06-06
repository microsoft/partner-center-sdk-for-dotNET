// <copyright file="AssetOwnershipLimit.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class represnts asset ownership limit.
    /// </summary>
    public class AssetOwnershipLimit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetOwnershipLimit"/> class.
        /// </summary>
        public AssetOwnershipLimit()
        {
        }

        /// <summary>
        /// Gets or sets MinSeats.
        /// </summary>
        public int MinAssets { get; set; }

        /// <summary>
        /// Gets or sets MaxSeats.
        /// </summary>
        public int MaxAssets { get; set; }

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Will not change name due to a currently existing contract.")]
        public string Type { get; set; }
    }
}
