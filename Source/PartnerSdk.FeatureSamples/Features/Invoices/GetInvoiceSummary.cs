// <copyright file="GetInvoiceSummary.cs" company="Microsoft Corporation">
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
    internal class GetInvoiceSummary : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Billing: Get Invoice Summary"; }
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
            Console.Out.WriteLine("\t/v1/invoices/summary");
            Console.Out.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Response: ");
            Console.ForegroundColor = ConsoleColor.White;

            // all the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            var invoiceSummary = scopedPartnerOperations.Invoices.Summary.Get();

            Console.Out.WriteLine("\tBalance as of:            {0:D}", invoiceSummary.AccountingDate);
            Console.Out.WriteLine("\tCurrent Account Balance:  {0:C}", invoiceSummary.BalanceAmount);
            Console.Out.WriteLine("\tCurrency Code:            {0}", invoiceSummary.CurrencyCode);
            Console.Out.WriteLine("\tCurrency Symbol:          {0}", invoiceSummary.CurrencySymbol);
            Console.Out.WriteLine("\tFirst Invoice Date:       {0}", invoiceSummary.FirstInvoiceCreationDate);
            Console.Out.WriteLine("\tLast Payment Date:        {0:D}", invoiceSummary.LastPaymentDate);
            Console.Out.WriteLine("\tLast Payment Amount:      {0:C}", invoiceSummary.LastPaymentAmount);
            
            Console.Out.WriteLine();

            // Add the currency code in the application state
            state[FeatureSamplesApplication.SelectedCurrencyCodeKey] = invoiceSummary.CurrencyCode;
        }
    }
}
