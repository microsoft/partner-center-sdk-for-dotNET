// <copyright file="InvoiceLineItemsPaging.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Invoices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Models.Invoices;
    using RequestContext;

    /// <summary>
    /// Showcases invoice line items paging.
    /// </summary>
    internal class InvoiceLineItemsPaging : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Invoice Line Items Paging"; }
        }

        /// <summary>
        /// Prints an invoice line item properties.
        /// </summary>
        /// <param name="item">the invoice line item.</param>
        public static void PrintProperties(InvoiceLineItem item)
        {
            Type t = null;

            if (item is LicenseBasedLineItem)
            {
                t = typeof(LicenseBasedLineItem);
                Console.Out.WriteLine(" ");
                Console.Out.WriteLine("\tLicense Based Line Items: ");
            }
            else if (item is UsageBasedLineItem)
            {
                t = typeof(OneTimeInvoiceLineItem);
                Console.Out.WriteLine(" ");
                Console.Out.WriteLine("\tUsage Based Line Items: ");
            }
            else if (item is OneTimeInvoiceLineItem)
            {
                t = typeof(OneTimeInvoiceLineItem);
                Console.Out.WriteLine(" ");
                Console.Out.WriteLine("\tOneTime Invoice Line Item: ");
            }

            PropertyInfo[] properties = t.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Console.Out.WriteLine(string.Format("\t{0,-30}|{1,-50}", property.Name, property.GetValue(item, null)?.ToString()));
            }
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
            Console.Out.WriteLine("\t/v1/invoices/{invoiceId}/lineitems/OneTime/BillingLineItems");
            Console.Out.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Response: ");
            Console.ForegroundColor = ConsoleColor.White;

            // read the invoice id from the application state
            var selectedInvoiceId = state[FeatureSamplesApplication.SelectedInvoiceKey] as string;

            // all the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            var invoiceOperations = scopedPartnerOperations.Invoices.ById(selectedInvoiceId);
            var invoice = invoiceOperations.Get();

            Console.Out.WriteLine("\tGetting invoice line items for invoice: " + invoice.Id);

            if (invoice.InvoiceDetails == null)
            {
                Console.Out.WriteLine("\tInvoice {0} does not have any invoice line items.", invoice.Id);
                return;
            }

            var allInvoiceLineItems = new List<InvoiceLineItem>();

            foreach (var invoiceDetail in invoice.InvoiceDetails)
            {
                Console.Out.WriteLine(
                    "\tInvoice line item for product: '{0}' and line item type: '{1}'", 
                    invoiceDetail.BillingProvider, 
                    invoiceDetail.InvoiceLineItemType);

                var invoiceLineItemsCollection = invoiceOperations.By(invoiceDetail.BillingProvider, invoiceDetail.InvoiceLineItemType).Get();
                var enumerator = scopedPartnerOperations.Enumerators.InvoiceLineItems.Create(invoiceLineItemsCollection);
                ConsoleKeyInfo keyInfo;

                var itemNumber = 1;
                while (enumerator.HasValue)
                {
                    Console.Out.WriteLine("\tInvoice line items count: " + enumerator.Current.TotalCount);
                    enumerator.Current.Items.Take(2).ToList().ForEach(i =>
                    {
                        Console.Out.WriteLine("\t----------------------------------------------");
                        Console.Out.WriteLine("\tLine Item # {0}", itemNumber);
                        PrintProperties(i);
                        itemNumber++;
                    });
                    allInvoiceLineItems.AddRange(invoiceLineItemsCollection.Items);
                    Console.Out.WriteLine("\tPress any key to fetch next data. Press the Escape (Esc) key to quit: \n");
                    keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        break;
                    }

                    enumerator.Next();
                }
            }
        }
    }
}
