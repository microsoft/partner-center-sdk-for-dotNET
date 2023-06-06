// -----------------------------------------------------------------------
// <copyright file="Order.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Offers;

    /// <summary>
    /// An order is the mean of purchasing offers. Offers are represented by line items within the order.
    /// </summary>
    public sealed class Order : ResourceBase
    {
        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the alternate order identifier.
        /// </summary>
        /// <value>
        /// The alternate order identifier (Quote Identifier).
        /// </value>
        public string AlternateId { get; set; }

        /// <summary>
        /// Gets or sets the reference customer identifier.
        /// </summary>
        public string ReferenceCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the type of billing cycle for the selected offers.
        /// </summary>
        /// <value>
        /// The type of billing cycle set for the offers.</value>
        public BillingCycleType BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>The currency code.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the currency symbol.
        /// </summary>
        /// <value>The currency symbol.</value>
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Gets or sets the Order line items. Each order line item refers to one offer's purchase data.
        /// </summary>
        public IEnumerable<OrderLineItem> LineItems { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the order.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the transaction type.
        /// </summary>
        /// <value>
        /// The order transaction type - 'UserPurchase' / 'SystemPurchase' / 'SystemBilling'.
        /// </value>
        public string TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the links corresponding to the order.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        public OrderLinks Links { get; set; }

        /// <summary>
        /// Gets or sets the total price for the order.
        /// </summary>
        /// <value>Order price (will not be returned unless explicitly asked for) Note: this information is PRE-TAX.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the PartnerOnRecord attestation was accepted.
        /// </summary>
        /// <value>The value of the PartnerOnRecord attestation acceptance.</value>
        public bool? PartnerOnRecordAttestationAccepted { get; set; }
    }
}