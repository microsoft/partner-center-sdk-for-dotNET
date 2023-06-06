// -----------------------------------------------------------------------
// <copyright file="IReceiptCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using GenericOperations;

    /// <summary>
    /// Represents the operations that can be done on Partner's invoice receipts.
    /// </summary>
    public interface IReceiptCollection : IPartnerComponent, IEntitySelector<IReceipt>
    {
        /// <summary>
        /// Gets a specific Invoice receipt behavior.
        /// </summary>
        /// <param name="receiptId">The receipt Id.</param>
        /// <returns>Invoice receipt operations.</returns>
        new IReceipt this[string receiptId] { get; }

        /// <summary>
        /// Obtains a specific Invoice receipt behavior.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>Invoice receipt operations.</returns>
        new IReceipt ById(string receiptId);
    }
}