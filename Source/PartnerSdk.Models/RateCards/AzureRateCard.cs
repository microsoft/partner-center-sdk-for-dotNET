﻿// -----------------------------------------------------------------------
// <copyright file="AzureRateCard.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.RateCards
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides real-time prices for Azure offers.
    /// </summary>
    public class AzureRateCard : ResourceBase
    {
        /// <summary>
        /// Gets or sets the locale for the localizable properties in the rate card meters.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the currency for the the meter rates.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the tax is included or not.
        /// </summary>
        public bool IsTaxIncluded { get; set; }

        /// <summary>
        /// Gets or sets a collection of meters.
        /// </summary>
        public IEnumerable<AzureMeter> Meters { get; set; }

        /// <summary>
        /// Gets or sets a collection of offer terms.
        /// </summary>
        public IEnumerable<AzureOfferTerm> OfferTerms { get; set; }
    }
}
