// -----------------------------------------------------------------------
// <copyright file="InvoiceTaxReceiptStatementOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Network;
    using Utilities;

    /// <summary>
    /// Represents the operations available on an invoice tax receipt statement.
    /// </summary>
    internal class InvoiceTaxReceiptStatementOperations : BasePartnerComponent<Tuple<string, string>>, IInvoiceTaxReceiptStatement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceTaxReceiptStatementOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="invoiceId">The invoice id.</param>
        /// <param name="taxReceiptId">The tax receipt id.</param>
        public InvoiceTaxReceiptStatementOperations(IPartner rootPartnerOperations, string invoiceId, string taxReceiptId)
            : base(rootPartnerOperations, new Tuple<string, string>(invoiceId, taxReceiptId))
        {
            ParameterValidator.Required(invoiceId, "invoiceId has to be set.");
            ParameterValidator.Required(taxReceiptId, "taxReceiptId has to be set.");
        }

        /// <summary>
        /// Retrieves the invoice tax receipt statement. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <returns>The invoice tax receipt statement.</returns>
        public Stream Get()
        {
            return PartnerService.Instance.SynchronousExecute<Stream>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the invoice tax receipt statement. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <returns>The invoice tax receipt statement.</returns>
        public async Task<Stream> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Stream, Stream>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetInvoiceTaxReceiptStatement.Path, this.Context.Item1, this.Context.Item2));

            partnerServiceProxy.Accept = "application/pdf";

            return await partnerServiceProxy.GetFileContentAsync();
        }
    }
}
