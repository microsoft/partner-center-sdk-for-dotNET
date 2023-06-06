// -----------------------------------------------------------------------
// <copyright file="PromotionEligibility.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.PromotionEligibilities
{
    using System.Collections.Generic;

    /// <summary>
    ///  This class represents a model of a promotion eligibility object.
    /// </summary>
    public class PromotionEligibility
    {
        /// <summary>
        /// Gets or sets the promotion id.
        /// </summary>
        public string PromotionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Partner/ Customer is eligible for the promotion.
        /// </summary>
        public bool IsEligible { get; set; }

        /// <summary>
        /// Gets or sets the errors if the caller is not eligible for the promotion. This will be null if the caller is eligible.
        /// </summary>
        public List<PromotionEligibilityError> Errors { get; set; }
    }
}