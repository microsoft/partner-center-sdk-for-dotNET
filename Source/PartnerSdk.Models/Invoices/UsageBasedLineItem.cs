﻿// -----------------------------------------------------------------------
// <copyright file="UsageBasedLineItem.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    /// <summary>
    /// Billing line items for usage based subscriptions.
    /// </summary>
    public sealed class UsageBasedLineItem : BaseUsageBasedLineItem
    {
        /// <summary>
        /// Gets or sets the  detail line item Id.
        /// Uniquely identifies the line items for cases where calculation is different for units consumed.
        /// Example: Total consumed = 1338, 1024 is charged with one rate, 314 is charge with a different rate.
        /// </summary>
        public int DetailLineItemId { get; set; }

        /// <summary>
        /// Gets or sets the service SKU.
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the units included in the order.
        /// </summary>
        public decimal IncludedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the quantity consumed above allowed usage.
        /// </summary>
        public decimal OverageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the price of each unit.
        /// </summary>
        public decimal ListPrice { get; set; }

        /// <summary>
        /// Gets or sets the price of quantity consumed.
        /// </summary>
        public decimal ConsumptionPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount on consumption.
        /// </summary>
        public decimal ConsumptionDiscount { get; set; }

        /// <summary>
        /// Gets or sets the currency associated with the prices.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the price charged before taxes.
        /// </summary>
        public decimal PretaxCharges { get; set; }

        /// <summary>
        /// Gets or sets the amount of tax charged.
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the total charges after tax.
        /// Pretax Charges + Tax Amount.
        /// </summary>
        public decimal PostTaxTotal { get; set; }

        /// <summary>
        /// Gets or sets the effective price before taxes.
        /// </summary>
        public decimal PretaxEffectiveRate { get; set; }

        /// <summary>
        /// Gets or sets the effective price after taxes.
        /// </summary>
        public decimal PostTaxEffectiveRate { get; set; }

        /// <summary>
        /// Gets or sets the charge type.
        /// </summary>
        public string ChargeType { get; set; }

        /// <summary>
        /// Returns the type of invoice line item.
        /// </summary>
        /// <returns>The type of invoice line item.</returns>
        public override InvoiceLineItemType InvoiceLineItemType
        {
            get { return InvoiceLineItemType.BillingLineItems; }
        }
    }
}
