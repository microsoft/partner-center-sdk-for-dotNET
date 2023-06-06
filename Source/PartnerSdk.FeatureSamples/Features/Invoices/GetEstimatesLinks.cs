// <copyright file="GetEstimatesLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Invoices
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using RequestContext;

    /// <summary>
    /// Showcases get invoice statement
    /// </summary>
    internal class GetEstimatesLinks : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Billing: Get Estimates Links"; }
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
            Console.Out.WriteLine("\t/v1/invoices/estimates/links");
            Console.Out.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Response: ");
            Console.ForegroundColor = ConsoleColor.White;

            var selectedCurencyCode = state.ContainsKey(FeatureSamplesApplication.SelectedCurrencyCodeKey) ? state[FeatureSamplesApplication.SelectedCurrencyCodeKey] as string : "usd";

            // all the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            // read estimate links for currencycode
            var estimateLinks = scopedPartnerOperations.Invoices.Estimates.Links.ByCurrency(selectedCurencyCode).Get();

            if (estimateLinks != null && estimateLinks.Items != null && estimateLinks.Items.Any())
            {
                foreach (var estimateLink in estimateLinks.Items)
                {
                    Console.Out.WriteLine("\t--------------------------------------------------------------------------------------------");
                    Console.Out.WriteLine("     \tBilling Provider:             {0}", estimateLink.Title);
                    Console.Out.WriteLine("     \tDescription:                  {0}", estimateLink.Description);
                    Console.Out.WriteLine("     \tPeriod:                       {0}", estimateLink.Period);
                    Console.Out.WriteLine("     \tUri:                          {0}", estimateLink.Link.Uri);
                }
            }
            else
            {
                Console.Out.WriteLine("\tNo estimate links found.");
            }

            Console.Out.WriteLine();
        }
    }
}