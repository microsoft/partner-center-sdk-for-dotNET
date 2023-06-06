// -----------------------------------------------------------------------
// <copyright file="RedemptionLimitPromotionEligibilityError.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.PromotionEligibilities
{
    /// <summary>
    ///  This class represents a model of a redemption limit promotion eligibility error object.
    /// </summary>
    public class RedemptionLimitPromotionEligibilityError : PromotionEligibilityError
    {
        /// <summary>
        /// Gets or sets the maximum number of promotions which can be redeemed by the customer.
        /// </summary>
        public int MaxPromotionRedemptionCount { get; set; }

        /// <summary>
        /// Gets or sets the remaining number of promotions which can be redeemed by the customer.
        /// </summary>
        public int RemainingPromotionRedemptionCount { get; set; }
    }
}
