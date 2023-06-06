// -----------------------------------------------------------------------
// <copyright file="Pricing.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    using Newtonsoft.Json;

    /// <summary>
    /// Pricing details.
    /// </summary>
    public class Pricing
    {
        /// <summary>
        /// Gets or sets the list price.
        /// </summary>
        /// <value>
        /// The single quantity sales price listed in the catalog without applying any price modifiers. This price is in the currency of the line item. This is the full term price when an availabilityTermId is specified.
        /// * This amount is before taxation. In tax inclusive country, this price includes the tax; otherwise not. * Required. Must be greater than or equal to zero.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? ListPrice { get; set; }

        /// <summary>
        /// Gets or sets the discounted price.
        /// </summary>
        /// <value>
        /// The single quantity discounted price after applying all applicable price modifiers (e.g.promo code, customer earned price, negotiated discount) on top of the list price. This is the discounted full term price when an availabilityTermId is specified.
        /// This price is in the currency of the line item. * This amount is before taxation. In tax inclusive country, this price includes the tax; otherwise not. * Required. Must be greater than or equal to zero.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? DiscountedPrice { get; set; }

        /// <summary>
        /// Gets or sets the prorated price.
        /// </summary>
        /// <value>
        /// The single quantity discounted price after proration. If no proration is performed this price will match the discounted price. See targetEndDate and purchaseTermUnits for details on proration. This price is in the currency of the line item.
        /// * This amount is before taxation. In tax inclusive country, this price includes the tax; otherwise not. * Required. Must be greater than or equal to zero.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? ProratedPrice { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The single quantity price in the target billing currency after all discounts have been applied and any proration has been performed.
        /// * This amount is before taxation. In tax inclusive country, this price includes the tax; otherwise not. * Required. Must be greater than or equal to zero.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }

        /// <summary>
        /// Gets or sets the extended price.
        /// </summary>
        /// <value>
        /// The price of the line item with quantity, discounts and proration applied, in the target billing currency. It will represent Price * Quantity – Refund Amount
        /// * This amount is before taxation. In tax inclusive country, this price includes the tax; otherwise not. * Required. Must be greater than or equal to zero.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? ExtendedPrice { get; set; }
    }
}
