// -----------------------------------------------------------------------
// <copyright file="InvoiceDocumentsOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using Utilities;

    /// <summary>
    /// Defines the operations available for an invoice document.
    /// </summary>
    internal class InvoiceDocumentsOperations : BasePartnerComponent, IInvoiceDocuments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceDocumentsOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="invoiceId">The invoice id.</param>
        public InvoiceDocumentsOperations(IPartner rootPartnerOperations, string invoiceId)
            : base(rootPartnerOperations, invoiceId)
        {
            ParameterValidator.Required(invoiceId, "invoiceId has to be set.");
        }

        /// <summary>
        /// Gets an invoice statement operations.
        /// </summary>
        public IInvoiceStatement Statement
        {
            get
            {
                return new InvoiceStatementOperations(this.Partner, this.Context);
            }
        }
    }
}
