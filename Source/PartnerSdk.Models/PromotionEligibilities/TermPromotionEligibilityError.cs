// -----------------------------------------------------------------------
// <copyright file="TermPromotionEligibilityError.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.PromotionEligibilities
{
    using System.Collections.Generic;

    /// <summary>
    ///  This class represents a model of a term promotion eligibility error object.
    /// </summary>
    public class TermPromotionEligibilityError : PromotionEligibilityError
    {
        /// <summary>
        /// Gets or sets the eligibile terms of the promotion.
        /// </summary>
        public IEnumerable<PromotionTerm> EligibleTerms { get; set; }
    }
}
