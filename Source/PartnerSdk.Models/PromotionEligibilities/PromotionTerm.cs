// -----------------------------------------------------------------------
// <copyright file="PromotionTerm.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.PromotionEligibilities
{
    using Microsoft.Store.PartnerCenter.Models.PromotionEligibilities.Enums;

    /// <summary>
    ///  This class represents a model of a promotion term object.
    /// </summary>
    public class PromotionTerm
    {
        /// <summary>
        /// Gets or sets the billing cycle of the promotion term.
        /// </summary>
        public BillingCycleType BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets the duration of the proimotion term.
        /// </summary>
        public string Duration { get; set; }
    }
}
