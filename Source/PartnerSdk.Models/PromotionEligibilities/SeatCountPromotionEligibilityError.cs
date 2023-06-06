// -----------------------------------------------------------------------
// <copyright file="SeatCountPromotionEligibilityError.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.PromotionEligibilities
{
    /// <summary>
    ///  This class represents a model of a seat count promotion eligibility error object.
    /// </summary>
    public class SeatCountPromotionEligibilityError : PromotionEligibilityError
    {
        /// <summary>
        /// Gets or sets the minimum required seats of the promotion. If there is no minimum this will be null.
        /// </summary>
        public int? MinimumRequiredSeats { get; set; }

        /// <summary>
        /// Gets or sets the maximum required seats of the promotion. If there is no maximum this will be null.
        /// </summary>
        public int? MaximumRequiredSeats { get; set; }

        /// <summary>
        /// Gets or sets the available seats which can be purchased by the customer while still being eligible for the promotion.
        /// This number is determined by looking at the maximum seat quantity constraint specified on the promotion and the seats already purchased (if any) for the product by the customer under the promotion.
        /// This value will be present in the response only if there is a maximum seat quantity constraint specified on the promotion.
        /// </summary>
        public int? AvailableSeats { get; set; }
    }
}
