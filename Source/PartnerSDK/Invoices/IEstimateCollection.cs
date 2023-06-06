// -----------------------------------------------------------------------
// <copyright file="IEstimateCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    /// <summary>
    /// Represents the operations that can be done on the partner's estimate collection.
    /// </summary>
    public interface IEstimateCollection : IPartnerComponent
    {
        /// <summary>
        /// Obtains the estimate links of the reconciliation line items.
        /// </summary>
        IEstimateLink Links { get; }
    }
}