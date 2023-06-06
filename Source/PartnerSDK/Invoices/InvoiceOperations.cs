// -----------------------------------------------------------------------
// <copyright file="InvoiceOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Invoices;
    using Network;
    using Utilities;

    /// <summary>
    /// Operations available for the reseller's invoice.
    /// </summary>
    internal class InvoiceOperations : BasePartnerComponent, IInvoice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="invoiceId">The invoice id.</param>
        public InvoiceOperations(IPartner rootPartnerOperations, string invoiceId)
            : base(rootPartnerOperations, invoiceId)
        {
            ParameterValidator.Required(invoiceId, "invoiceId has to be set.");
        }

        /// <summary>
        /// Gets an invoice documents operations.
        /// </summary>
        public IInvoiceDocuments Documents
        {
            get { return new InvoiceDocumentsOperations(this.Partner, this.Context); }
        }

        /// <summary>
        /// Obtains the Receipts behavior of the invoice.
        /// </summary>
        public IReceiptCollection Receipts
        {
            get { return new ReceiptCollectionOperations(this.Partner, this.Context); }
        }

        /// <summary>
        /// Creates an invoice line item collection operation given a billing provider and invoice line item type.
        /// </summary>
        /// <param name="billingProvider">The billing provider.</param>
        /// <param name="invoiceLineItemType">The invoice line item type.</param>
        /// <returns>The invoice line item collection operations.</returns>
        public IInvoiceLineItemCollection By(BillingProvider billingProvider, InvoiceLineItemType invoiceLineItemType)
        {
            return new InvoiceLineItemCollectionOperations(this.Partner, this.Context, billingProvider, invoiceLineItemType);
        }

        /// <summary>
        /// Creates an invoice line item collection operations given a billing provider and invoice line item type.
        /// </summary>
        /// <param name="provider">The provider type. example: All, Marketplace.</param>
        /// <param name="invoiceLineItemType">The invoice line item. example: BillingLineItems, UsageLineItems.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="period">The period for unbilled reconciliation. example: current, previous.</param>
        /// <param name="size">The page size.</param>
        /// <returns>The reconciliation line item collection operations.</returns>
        public IReconciliationLineItemCollection By(string provider, string invoiceLineItemType, string currencyCode, string period, int? size)
        {
            return new ReconciliationLineItemCollectionOperations(this.Partner, this.Context, provider, invoiceLineItemType, currencyCode, period, size);
        }
  
        /// <summary>
        /// Retrieves information about a specific invoice.
        /// </summary>
        /// <returns>The invoice.</returns>
        public Invoice Get()
        {
            return PartnerService.Instance.SynchronousExecute<Invoice>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves information about a specific invoice.
        /// </summary>
        /// <returns>The invoice.</returns>
        public async Task<Invoice> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Invoice, Invoice>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetInvoice.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
