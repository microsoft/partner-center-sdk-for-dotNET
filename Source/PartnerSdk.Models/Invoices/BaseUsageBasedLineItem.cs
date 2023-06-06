// -----------------------------------------------------------------------
// <copyright file="BaseUsageBasedLineItem.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    using System;

    /// <summary>
    /// Groups common properties for usage based invoice line items.
    /// </summary>
    public abstract class BaseUsageBasedLineItem : InvoiceLineItem
    {
        /// <summary>
        /// Gets or sets the partner's azure active directory tenant Id.
        /// </summary>
        public string PartnerId { get; set; }

        /// <summary>
        /// Gets or sets the partner's Name.
        /// </summary>
        public string PartnerName { get; set; }

        /// <summary>
        /// Gets or sets the partner billable account Id.
        /// </summary>
        public string PartnerBillableAccountId { get; set; }

        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer company name.
        /// </summary>
        public string CustomerCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the partner's Microsoft Partner Network Id.
        /// For direct resellers, this is the MPN Id of the reseller.
        /// For indirect resellers, this is the MPN Id of the VAR (Value Added Reseller).
        /// </summary>
        public int MpnId { get; set; }

        /// <summary>
        /// Gets or sets the Tier 2 Partner's Microsoft Partner Network Id.
        /// </summary>
        public int Tier2MpnId { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the Domain name.
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Gets or sets the Billing cycle type.
        /// </summary>
        public string BillingCycleType { get; set; }

        /// <summary>
        /// Gets or sets the subscription Id.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the subscription name.
        /// </summary>
        public string SubscriptionName { get; set; }

        /// <summary>
        /// Gets or sets the description of the subscription.
        /// </summary>
        public string SubscriptionDescription { get; set; }

        /// <summary>
        /// Gets or sets the order Id.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the service name. Example: Azure Data Service.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the service type. Example: Azure SQL Azure DB.
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        public string ResourceGuid { get; set; }

        /// <summary>
        /// Gets or sets the resource name. Example: Database (GB/month)
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the region associated with the resource instance.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the total units consumed.
        /// </summary>
        public decimal ConsumedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the date charge begins.
        /// </summary>
        public DateTime ChargeStartDate { get; set; }

        /// <summary>
        /// Gets or sets the date charge ends.
        /// </summary>
        public DateTime ChargeEndDate { get; set; }

        /// <summary>
        /// Gets or sets the unit of measure for azure usage.
        /// </summary>        
        public string Unit { get; set; }

        /// <summary>
        /// Returns the billing provider.
        /// </summary>
        /// <returns>The billing provider.</returns>
        public override BillingProvider BillingProvider
        {
            get { return BillingProvider.Azure; }
        }
    }
}
