// -----------------------------------------------------------------------
// <copyright file="GetAzureRateCard.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.RateCards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Showcases getting Azure rate card.
    /// </summary>
    internal class GetAzureRateCard : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Azure rate card"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var azureRateCard = partnerOperations.RateCards.Azure.Get("USD", "US");

            foreach (var meter in azureRateCard.Meters.Take(5))
            {
                Console.WriteLine("Meter Name: {0}", meter.Name);
                Console.WriteLine("Meter Category: {0}", meter.Category);
                Console.WriteLine("Meter Unit: {0}", meter.Unit);
                Console.WriteLine();
            }

            foreach (var term in azureRateCard.OfferTerms.Take(5))
            {
                Console.WriteLine("Term: {0}", term.Name);
                Console.WriteLine();
            }
        }
    }
}
