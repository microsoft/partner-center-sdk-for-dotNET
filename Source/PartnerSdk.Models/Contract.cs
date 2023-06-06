// -----------------------------------------------------------------------
// <copyright file="Contract.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models
{
    /// <summary>
    /// Base Contract.
    /// </summary>
    public abstract class Contract : ResourceBase
    {
        /// <summary>
        /// Gets or sets the  order identifier.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets the contract type.
        /// </summary>
        public abstract ContractType ContractType { get; }
    }
}