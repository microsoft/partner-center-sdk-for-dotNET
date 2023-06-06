// -----------------------------------------------------------------------
// <copyright file="GetInvoiceSummaries.cs" company="Microsoft Corporation">
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
    /// Showcases get invoice summary.
    /// </summary>
    internal class GetInvoiceSummaries : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Billing: Get Invoice Summaries"; }
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
            Console.Out.WriteLine("\t/v1/invoices/summaries");
            Console.Out.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Response: ");
            Console.ForegroundColor = ConsoleColor.White;

            // all the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            var invoiceSummaries = scopedPartnerOperations.Invoices.Summaries.Get();

            foreach (var summary in invoiceSummaries.Items)
            {
                foreach (var detail in summary.Details)
                {
                    Console.Out.WriteLine("\tBilling Type: " + detail.InvoiceType + "-" + summary.CurrencyCode);
                    Console.Out.WriteLine("\t---------------------------");
                    Console.Out.WriteLine("\tBalance as of:               {0:D}", summary.AccountingDate);
                    Console.Out.WriteLine("\tCurrent Account Balance:     {0:C}", summary.BalanceAmount);
                    Console.Out.WriteLine("\tCurrency Code:               {0}", summary.CurrencyCode);
                    Console.Out.WriteLine("\tCurrency Symbol:             {0}", summary.CurrencySymbol);
                    Console.Out.WriteLine("\tFirst Invoice Date:          {0:D}", summary.FirstInvoiceCreationDate);
                    Console.Out.WriteLine("\tLast Payment Date:           {0:D}", summary.LastPaymentDate);
                    Console.Out.WriteLine("\tLast Payment Amount:         {0:C}", summary.LastPaymentAmount);

                    Console.Out.WriteLine(" ");
                    Console.Out.WriteLine("\tInvoice Details:");
                    Console.Out.WriteLine("\t----------------");
                    Console.Out.WriteLine("     \tInvoice Type:              {0}", detail.InvoiceType);
                    Console.Out.WriteLine("     \tBalance as of:             {0:D}", detail.Summary.AccountingDate);
                    Console.Out.WriteLine("     \tCurrent Account Balance:   {0:C}", detail.Summary.BalanceAmount);
                    Console.Out.WriteLine("     \tCurrency Code:             {0}", detail.Summary.CurrencyCode);
                    Console.Out.WriteLine("     \tCurrency Symbol:           {0}", detail.Summary.CurrencySymbol);
                    Console.Out.WriteLine("     \tFirst Invoice Date:        {0:D}", detail.Summary.FirstInvoiceCreationDate);
                    Console.Out.WriteLine("     \tLast Payment Date:         {0:D}", detail.Summary.LastPaymentDate);
                    Console.Out.WriteLine("     \tLast Payment Amount:       {0:C}", detail.Summary.LastPaymentAmount);

                    Console.Out.WriteLine();
                }
            }
        }
    }
}
