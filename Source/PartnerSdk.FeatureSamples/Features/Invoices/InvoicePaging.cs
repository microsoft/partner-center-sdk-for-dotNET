// <copyright file="InvoicePaging.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Invoices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Invoices;
    using Models.Query;
    using RequestContext;

    /// <summary>
    /// Showcases invoice paging.
    /// </summary>
    internal class InvoicePaging : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Invoice Paging"; }
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
            Console.Out.WriteLine("\t/v1/invoices");
            Console.Out.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Response: ");
            Console.ForegroundColor = ConsoleColor.White;

            var allInvoices = new List<Invoice>();

            // all the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            var pagedInvoiceCollection = scopedPartnerOperations.Invoices.Query(QueryFactory.Instance.BuildIndexedQuery(5, 0));
            var invoiceEnumerator = scopedPartnerOperations.Enumerators.Invoices.Create(pagedInvoiceCollection);

            while (invoiceEnumerator.HasValue)
            {
                Console.Out.WriteLine("\tInvoice count: " + invoiceEnumerator.Current.TotalCount);

                foreach (var invoice in invoiceEnumerator.Current.Items)
                {
                    Console.Out.WriteLine("\tInvoice Id:         {0}", invoice.Id);
                    Console.Out.WriteLine("\tBilling Period:     {0:MMM}", invoice.InvoiceDate);
                    Console.Out.WriteLine("\tCurrency Code:      {0:C}", invoice.CurrencyCode);
                    Console.Out.WriteLine("\tCurrency Symbol:    {0:C}", invoice.CurrencySymbol);
                    Console.Out.WriteLine("\tTotal Charges:      {0:C}", invoice.TotalCharges);
                    Console.Out.WriteLine("\tPaid Amount:        {0:C}", invoice.PaidAmount);                    
                    Console.Out.WriteLine("\tPdf Download Link:  {0:C}", invoice.PdfDownloadLink.ToString());
                    
                    if (invoice.InvoiceDetails != null)
                    {
                        Console.Out.WriteLine(" ");
                        Console.Out.WriteLine("\tInvoice details for Invoice Id: " + invoice.Id);
                        Console.Out.WriteLine("\t-------------------------------------------");

                        foreach (var invoiceDetail in invoice.InvoiceDetails)
                        {
                            Console.Out.WriteLine("     \tBilling Provider:             {0}", invoiceDetail.BillingProvider);
                            Console.Out.WriteLine("     \tInvoice Line Item Type:       {0}", invoiceDetail.InvoiceLineItemType);
                            Console.Out.WriteLine("     \tReconciliation Download Link:          {0}", invoiceDetail.Links.Self.Uri);
                        }
                    }

                    Console.Out.WriteLine();
                }

                allInvoices.AddRange(pagedInvoiceCollection.Items);
                invoiceEnumerator.Next();
            }

            // add the customers to the application state
            state[FeatureSamplesApplication.SelectedInvoiceKey] = allInvoices.Where(i => i.InvoiceDetails != null && i.InvoiceType == "OneTime").First().Id;
        }
    }
}
