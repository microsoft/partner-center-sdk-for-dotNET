// -----------------------------------------------------------------------
// <copyright file="ReceiptOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System;
    using Utilities;

    /// <summary>
    /// Represents the operations that can be done on Partner's invoice receipts.
    /// </summary>
    internal class ReceiptOperations : BasePartnerComponent<Tuple<string, string>>, IReceipt
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="invoiceId">The invoice id.</param>
        /// <param name="receiptId">The receipt id.</param>
        public ReceiptOperations(IPartner rootPartnerOperations, string invoiceId, string receiptId)
            : base(rootPartnerOperations, new Tuple<string, string>(invoiceId, receiptId))
        {
            ParameterValidator.Required(invoiceId, "invoiceId has to be set.");
            ParameterValidator.Required(receiptId, "receiptId has to be set.");
        }

        /// <summary>
        /// Gets an invoice documents operations.
        /// </summary>
        public IReceiptDocuments Documents
        {
            get { return new ReceiptDocumentsOperations(this.Partner, this.Context.Item1, this.Context.Item2); }
        }
    }
}
