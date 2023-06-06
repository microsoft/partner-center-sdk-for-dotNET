//----------------------------------------------------------------
// <copyright file="AvailabilityTerm.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents an availability term.
    /// </summary>
    public class AvailabilityTerm
    {
        /// <summary>
        /// Gets or sets the internal id of the term.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the ISO standard representation of this term's duration.
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the term description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the billing cycle of the AvailabilityTerm.
        /// </summary>
        public string BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets the renewal options.
        /// </summary>
        public IEnumerable<RenewalOption> RenewalOptions { get; set; }

        /// <summary>
        /// Gets or sets the cancellation policies that can apply to this term.
        /// </summary>
        public IEnumerable<CancellationPolicy> CancellationPolicies { get; set; }
    }
}
