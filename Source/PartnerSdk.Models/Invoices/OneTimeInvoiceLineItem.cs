// -----------------------------------------------------------------------
// <copyright file="OneTimeInvoiceLineItem.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an invoice billing line item for licensed based subscriptions
    /// </summary>
    public class OneTimeInvoiceLineItem : InvoiceLineItem
    {
        /// <summary>
        /// Gets or sets the partner commerce account Id.
        /// </summary>
        public string PartnerId { get; set; }

        /// <summary>
        /// Gets or sets the customer commerce account Id.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer domain name.
        /// </summary>
        public string CustomerDomainName { get; set; }

        /// <summary>
        /// Gets or sets the customer country.
        /// </summary>
        public string CustomerCountry { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the MPN Id associated to this line item.
        /// </summary>
        public string MpnId { get; set; }

        /// <summary>
        /// Gets or sets the Reseller MPN Id of the Tier 2 partner associated to this line item.
        /// </summary>
        public int ResellerMpnId { get; set; }

        /// <summary>
        /// Gets or sets the order unique identifier.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the date when order created
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the product unique identifier
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the sku unique identifier
        /// </summary>
        public string SkuId { get; set; }

        /// <summary>
        /// Gets or sets the availability unique identifier
        /// </summary>
        public string AvailabilityId { get; set; }

        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the sku name
        /// </summary>
        public string SkuName { get; set; }

        /// <summary>
        /// Gets the purchase types.
        /// </summary>
        public List<string> ProductQualifiers { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the type of charge
        /// Examples: Purchase Fee, Cycle Fee, Prorate Fees when Purchase
        /// </summary>
        public string ChargeType { get; set; }

        /// <summary>
        /// Gets or sets the unit price
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the effective unit price
        /// </summary>
        public decimal EffectiveUnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the unit type
        /// </summary>
        public string UnitType { get; set; }

        /// <summary>
        /// Gets or sets the number of units associated with this line item
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the amount after discount
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// Gets or sets the taxes charged.
        /// </summary>
        public decimal TaxTotal { get; set; }

        /// <summary>
        /// Gets or sets the total amount after discount and tax
        /// </summary>
        public decimal TotalForCustomer { get; set; }

        /// <summary>
        /// Gets or sets the currency used for this line item.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the publisher name associated with this purchase
        /// </summary>
        public string PublisherName { get; set; }

        /// <summary>
        /// Gets or sets the publisher id associated with this purchase
        /// </summary>
        public string PublisherId { get; set; }

        /// <summary>
        /// Gets or sets the subscription description associated with this purchase
        /// </summary>
        public string SubscriptionDescription { get; set; }

        /// <summary>
        /// Gets or sets the subscription id associated with this purchase
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the subscription start date.
        /// </summary>
        public DateTime? SubscriptionStartDate { get; set; }

        /// <summary>
        /// Gets or sets the subscription end date.
        /// </summary>
        public DateTime? SubscriptionEndDate { get; set; }

        /// <summary>
        /// Gets or sets the charge start date associated with this purchase
        /// </summary>
        public DateTime ChargeStartDate { get; set; }

        /// <summary>
        /// Gets or sets the charge end date associated with this purchase
        /// </summary>
        public DateTime ChargeEndDate { get; set; }

        /// <summary>
        /// Gets or sets the term and billing cycle associated with this purchase
        /// </summary>
        public string TermAndBillingCycle { get; set; }

        /// <summary>
        /// Gets or sets the Alternate Id (quote id).
        /// </summary>
        public string AlternateId { get; set; }

        /// <summary>
        /// Gets or sets the reference id.
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the price adjustment description.
        /// </summary>
        public string PriceAdjustmentDescription { get; set; }

        /// <summary>
        /// Gets or sets the discount details associated with this purchase - To be deprecated in a future release
        /// </summary>
        [Obsolete]
        public string DiscountDetails { get; set; }

        /// <summary>
        /// Gets or sets the pricing currency code.
        /// </summary>
        public string PricingCurrency { get; set; }

        /// <summary>
        /// Gets or sets the pricing currency to billing currency exchange rate.
        /// </summary>
        public decimal PCToBCExchangeRate { get; set; }

        /// <summary>
        /// Gets or sets the exchange rate date at which the pricing currency to billing currency exchange rate was determined.
        /// </summary>
        public DateTime? PCToBCExchangeRateDate { get; set; }

        /// <summary>
        /// Gets or sets the units purchased. Per design column named as BillableQuantity.
        /// </summary>
        public decimal BillableQuantity { get; set; }

        /// <summary>
        /// Gets or sets the meter description for consumption line item.
        /// </summary>
        public string MeterDescription { get; set; }

        /// <summary>
        /// Gets or sets the billing frequency.
        /// </summary>
        public string BillingFrequency { get; set; }

        /// <summary>
        /// Gets or sets the reservation order identifier for an Azure RI Purchase.
        /// </summary>
        public string ReservationOrderId { get; set; }

        /// <summary>
        /// Gets or sets the credit reason code.
        /// </summary>
        public string CreditReasonCode { get; set; }

        /// <summary>
        /// Returns the type of invoice line item
        /// </summary>
        /// <returns>The type of invoice line item.</returns>
        public override InvoiceLineItemType InvoiceLineItemType
        {
            get { return InvoiceLineItemType.BillingLineItems; }
        }

        /// <summary>
        /// Returns the billing provider
        /// </summary>
        /// <returns>The billing provider.</returns>
        public override BillingProvider BillingProvider
        {
            get { return BillingProvider.OneTime; }
        }

        /// <summary>
        /// Gets or sets the promotion id.
        /// </summary>
        /// <returns>The Promotion Id.</returns>
        public string PromotionId { get; set; }
    }
}