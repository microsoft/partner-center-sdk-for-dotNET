// -----------------------------------------------------------------------
// <copyright file="IInvoiceDocuments.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    /// <summary>
    /// Defines the operations available on invoice documents.
    /// </summary>
    public interface IInvoiceDocuments : IPartnerComponent
    {
        /// <summary>
        /// Gets an invoice statement operations.
        /// </summary>
        IInvoiceStatement Statement { get; }
    }
}
