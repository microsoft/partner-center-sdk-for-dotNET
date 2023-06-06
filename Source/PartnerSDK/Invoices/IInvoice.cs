// -----------------------------------------------------------------------
// <copyright file="IInvoice.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Invoices;

    /// <summary>
    /// Represents all the operations that can be done on an invoice.
    /// </summary>
    public interface IInvoice : IPartnerComponent, IEntityGetOperations<Invoice>
    {
        /// <summary>
        /// Gets the invoice's documents operations.
        /// </summary>
        IInvoiceDocuments Documents { get; }

        /// <summary>
        /// Obtains the Receipts behavior of the invoice.
        /// </summary>
        IReceiptCollection Receipts { get; }

        /// <summary>
        /// Creates an invoice line item collection operations given a billing provider and invoice line item type.
        /// </summary>
        /// <param name="billingProvider">The billing provider.</param>
        /// <param name="invoiceLineItemType">The invoice line item type.</param>
        /// <returns>The invoice line item collection operations.</returns>
        IInvoiceLineItemCollection By(BillingProvider billingProvider, InvoiceLineItemType invoiceLineItemType);

        /// <summary>
        /// Creates an invoice line item collection operations given a billing provider and invoice line item type.
        /// </summary>
        /// <param name="provider">The provider type. example: All, Marketplace.</param>
        /// <param name="invoiceLineItemType">The invoice line item. example: BillingLineItems, UsageLineItems.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="period">The period for unbilled reconciliation. example: current, previous.</param>
        /// <param name="size">The page size.</param>
        /// <returns>The reconciliation line item collection operations.</returns>
        IReconciliationLineItemCollection By(string provider, string invoiceLineItemType, string currencyCode, string period, int? size);

        /// <summary>
        /// Retrieves the invoice. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <returns>The invoice.</returns>
        new Invoice Get();

        /// <summary>
        /// Asynchronously retrieves the invoice. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <returns>The invoice.</returns>
        new Task<Invoice> GetAsync();
    }
}
