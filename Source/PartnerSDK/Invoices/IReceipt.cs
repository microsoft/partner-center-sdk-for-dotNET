// -----------------------------------------------------------------------
// <copyright file="IReceipt.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System;

    /// <summary>
    /// Represents the operations that can be done on Partner's invoice receipts.
    /// </summary>
    public interface IReceipt : IPartnerComponent<Tuple<string, string>>
    {
        /// <summary>
        /// Obtains the receipt documents behavior of the invoices.
        /// </summary>
        IReceiptDocuments Documents { get; }
    }
}