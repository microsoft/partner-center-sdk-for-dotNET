// <copyright file="GetInvoiceTaxReceiptStatement.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Invoices
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using RequestContext;

    /// <summary>
    /// Showcases get invoice statement
    /// </summary>
    internal class GetInvoiceTaxReceiptStatement : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Billing: Get Tax Receipt Statement by Id"; }
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
            Console.Out.WriteLine("\t/v1/invoices/{invoiceId}/receipts/{receiptId}/documents/statement");
            Console.Out.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Response: ");
            Console.ForegroundColor = ConsoleColor.White;

            // read the invoice id from the application state
            var selectedInvoiceId = state[FeatureSamplesApplication.SelectedInvoiceKey] as string;

            var selectedReceiptId = state[FeatureSamplesApplication.SelectedReceiptKey] as string;

            // all the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            // read invoice tax receipt statement
            var invoiceTaxReceiptStatement = scopedPartnerOperations.Invoices.ById(selectedInvoiceId).Receipts.ById(selectedReceiptId).Documents.Statement.Get();

            var filePath = "C:\\test";
            var fileName = string.Format("C:\\test\\E-Tax-{0}.pdf", selectedReceiptId);

            bool exists = Directory.Exists(filePath);

            if (!exists)
            {
                Directory.CreateDirectory(filePath);
            }

            using (var fileStream = File.Create(fileName))
            {
                invoiceTaxReceiptStatement.CopyTo(fileStream);
            }

            Console.Out.WriteLine("\tInvoice Id:                                   {0}", selectedInvoiceId);
            Console.Out.WriteLine("\tTax Receipt Id:                               {0}", selectedReceiptId);
            Console.Out.WriteLine("\tStatement Size:                               {0} bytes", invoiceTaxReceiptStatement.Length);
            Console.Out.WriteLine("\tTax Receipt PDF downloaded in this location:  {0}", fileName);

            Console.Out.WriteLine();
        }
    }
}