﻿// -----------------------------------------------------------------------
// <copyright file="AzureOfferTerm.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.RateCards
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an offer term tied to an Azure rate card.
    /// </summary>
    public class AzureOfferTerm
    {
        /// <summary>
        /// Gets or sets the offer name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the applied discount, if any.
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// Gets or sets the excluded meter IDs from the offer term, if any.
        /// </summary>
        public IEnumerable<string> ExcludedMeterIds { get; set; }

        /// <summary>
        /// Gets or sets the effective start date of the offer term (in UTC).
        /// </summary>
        public DateTime EffectiveDate { get; set; }
    }
}
