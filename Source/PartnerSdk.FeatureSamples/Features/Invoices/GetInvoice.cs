// <copyright file="GetInvoice.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Invoices
{
    using System;
    using System.Collections.Generic;
    using RequestContext;

    /// <summary>
    /// Showcases get invoice
    /// </summary>
    internal class GetInvoice : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Billing: Get Invoice by Id"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            Console.Out.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Request: ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine("\t/v1/invoices/{InvoiceId}");
            Console.Out.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Response: ");
            Console.ForegroundColor = ConsoleColor.White;

            // read the invoice id from the application state
            var selectedInvoiceId = state[FeatureSamplesApplication.SelectedInvoiceKey] as string;

            // all the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            // read customers into chunks of 40s
            var invoice = scopedPartnerOperations.Invoices.ById(selectedInvoiceId).Get();

            Console.Out.WriteLine("\tInvoice Id:           {0}", invoice.Id);
            Console.Out.WriteLine("\tBilling Period:       {0:MMM}", invoice.InvoiceDate);
            Console.Out.WriteLine("\tBilling Period Start Date:       {0:MMM}", invoice.BillingPeriodStartDate);
            Console.Out.WriteLine("\tBilling Period End Date:         {0:MMM}", invoice.BillingPeriodEndDate);
            Console.Out.WriteLine("\tCurrency Code:        {0}", invoice.CurrencyCode);
            Console.Out.WriteLine("\tCurrency Symbol:      {0}", invoice.CurrencySymbol);
            Console.Out.WriteLine("\tTotal Charges:        {0:C}", invoice.TotalCharges);
            Console.Out.WriteLine("\tPaid Amount:          {0:C}", invoice.PaidAmount);
            Console.Out.WriteLine("\tPDF Download Link:    {0:C}", invoice.PdfDownloadLink);
            
            if (invoice.InvoiceDetails != null)
            {
                Console.Out.WriteLine(" ");
                Console.Out.WriteLine("\tInvoice details: ");
                Console.Out.WriteLine("\t----------------");

                foreach (var invoiceDetail in invoice.InvoiceDetails)
                {
                    Console.Out.WriteLine("     \tBilling Provider:             {0}", invoiceDetail.BillingProvider);
                    Console.Out.WriteLine("     \tInvoice Line Item Type:       {0}", invoiceDetail.InvoiceLineItemType);
                    Console.Out.WriteLine("     \tReconciliation Download Link:          {0}", invoiceDetail.Links.Self.Uri);
                }
            }

            if (invoice.TaxReceipts != null)
            {
                Console.Out.WriteLine(" ");
                Console.Out.WriteLine("\tTax Receipt details for Invoice Id: " + invoice.Id);
                Console.Out.WriteLine("\t----------------------------------------------");

                foreach (var taxReceipt in invoice.TaxReceipts)
                {
                    Console.Out.WriteLine("     \tTax Receipt Id:             {0}", taxReceipt.Id);
                    Console.Out.WriteLine("     \tTax Receipt Download Link:  {0}", taxReceipt.TaxReceiptPdfDownloadLink);
                }
            }

            Console.Out.WriteLine();
        }
    }
}
