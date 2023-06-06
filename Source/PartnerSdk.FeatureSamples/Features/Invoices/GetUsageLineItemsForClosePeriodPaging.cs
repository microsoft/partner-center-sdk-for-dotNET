// <copyright file="GetUsageLineItemsForClosePeriodPaging.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models.Query;
    using Models.Invoices;
    using RequestContext;

    /// <summary>
    /// Showcases billed consumption line items paging.
    /// Marketplace Usage Consumption
    /// </summary>
    internal class GetUsageLineItemsForClosePeriodPaging : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Billed - Consumption - Reconciliation Line Items Paging"; }
        }

        /// <summary>
        /// Prints an invoice line item properties.
        /// </summary>
        /// <param name="item">the invoice line item.</param>
        public static void PrintProperties(InvoiceLineItem item)
        {
            Type t = null;

            if (item is DailyRatedUsageLineItem)
            {
                t = typeof(DailyRatedUsageLineItem);
                Console.Out.WriteLine(" ");
                Console.Out.WriteLine("\tMarketplace Daily Rated Usage Line Items: ");
            }
            else if (item is OneTimeInvoiceLineItem)
            {
                t = typeof(OneTimeInvoiceLineItem);
                Console.Out.WriteLine(" ");
                Console.Out.WriteLine("\tNon-Consumption First Party And Marketplace Line Items: ");
            }

            PropertyInfo[] properties = t.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Console.Out.WriteLine(string.Format("\t{0,-30}|{1,-50}", property.Name, property.GetValue(item, null).ToString()));
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
            Console.Out.WriteLine("\t/v1/invoices/{InvoiceId}/lineitems?provider=marketplace&invoicelineitemtype=usagelineitems&currencycode={CurrencyCode}&period={Period}&size={Size}");
            Console.Out.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Response: ");
            Console.ForegroundColor = ConsoleColor.White;

            // read the invoice id from the application state
            var invoiceId = state[FeatureSamplesApplication.SelectedInvoiceKey] as string;
            var curencyCode = state.ContainsKey(FeatureSamplesApplication.SelectedCurrencyCodeKey) ? state[FeatureSamplesApplication.SelectedCurrencyCodeKey] as string : "usd";

            var pageMaxSizeReconciliationLineItems = 2000;
            var period = "previous";

            // all the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            var seekBasedResourceCollection = scopedPartnerOperations.Invoices.ById(invoiceId).By("marketplace", "usagelineitems", curencyCode, period, pageMaxSizeReconciliationLineItems).Get();

            var fetchNext = true;

            ConsoleKeyInfo keyInfo;

            var itemNumber = 1;
            while (fetchNext)
            {
                Console.Out.WriteLine("\tReconciliation line items count: " + seekBasedResourceCollection.Items.Count());
                Console.Out.WriteLine("\tPeriod: " + period);

                seekBasedResourceCollection.Items.Take(2).ToList().ForEach(i =>
                {
                    Console.Out.WriteLine("\t----------------------------------------------");
                    Console.Out.WriteLine("\tLine Item # {0}", itemNumber);
                    
                    PrintProperties(i);
                    itemNumber++;
                });
                
                Console.Out.WriteLine("\tPress any key to fetch next data. Press the Escape (Esc) key to quit: \n");
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }

                fetchNext = !string.IsNullOrWhiteSpace(seekBasedResourceCollection.ContinuationToken);

                if (fetchNext)
                {
                    if (seekBasedResourceCollection.Links.Next.Headers != null && seekBasedResourceCollection.Links.Next.Headers.Any())
                    {
                        seekBasedResourceCollection = scopedPartnerOperations.Invoices.ById(invoiceId).By("marketplace", "usagelineitems", curencyCode, period, pageMaxSizeReconciliationLineItems).Seek(seekBasedResourceCollection.ContinuationToken, SeekOperation.Next);
                    }
                }                
            }            
        }
    }
}