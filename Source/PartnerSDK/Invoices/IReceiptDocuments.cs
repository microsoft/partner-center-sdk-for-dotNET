// -----------------------------------------------------------------------
// <copyright file="IReceiptDocuments.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System;

    /// <summary>
    /// Defines the operations available on receipt documents.
    /// </summary>
    public interface IReceiptDocuments : IPartnerComponent<Tuple<string, string>>
    {
        /// <summary>
        /// Gets an invoice statement operations.
        /// </summary>
        IReceiptStatement Statement { get; }
    }
}
