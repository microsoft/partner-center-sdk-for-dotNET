// -----------------------------------------------------------------------
// <copyright file="ReceiptDocumentsOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System;
    using Utilities;

    /// <summary>
    /// Defines the operations available for an invoice document.
    /// </summary>
    internal class ReceiptDocumentsOperations : BasePartnerComponent<Tuple<string, string>>, IReceiptDocuments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptDocumentsOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="invoiceId">The invoice id</param>
        /// <param name="receiptId">The receipt id</param>
        public ReceiptDocumentsOperations(IPartner rootPartnerOperations, string invoiceId, string receiptId)
            : base(rootPartnerOperations, new Tuple<string, string>(invoiceId, receiptId))
        {
            ParameterValidator.Required(invoiceId, "invoiceId has to be set.");
            ParameterValidator.Required(receiptId, "receiptId has to be set.");
        }

        /// <summary>
        /// Gets an invoice statement operations.
        /// </summary>
        public IReceiptStatement Statement
        {
            get
            {
                return new ReceiptStatementOperations(this.Partner, this.Context.Item1, this.Context.Item2);
            }
        }
    }
}
