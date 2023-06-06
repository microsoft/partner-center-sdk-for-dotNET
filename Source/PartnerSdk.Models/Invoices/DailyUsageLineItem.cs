// -----------------------------------------------------------------------
// <copyright file="DailyUsageLineItem.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    using System;

    /// <summary>
    /// Defines the properties of a line item for usage based subscriptions.
    /// </summary>
    public sealed class DailyUsageLineItem : BaseUsageBasedLineItem
    {
        /// <summary>
        /// Gets or sets the customer billable account.
        /// </summary>
        public string CustomerBillableAccount { get; set; }

        /// <summary>
        /// Gets or sets the usage date of the resource.
        /// </summary>
        public DateTime UsageDate { get; set; }

        /// <summary>
        /// Gets or sets the metered service name. Example: Storage.
        /// </summary>
        public string MeteredService { get; set; }

        /// <summary>
        /// Gets or sets the metered service type. Example: External.
        /// </summary>
        public string MeteredServiceType { get; set; }

        /// <summary>
        /// Gets or sets the metered region. Example: West US.
        /// </summary>
        public string MeteredRegion { get; set; }

        /// <summary>
        /// Gets or sets the project or instance name.
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// Gets or sets the additional service information.
        /// </summary>
        public string ServiceInfo { get; set; }

        /// <summary>
        /// Returns the type of invoice line item.
        /// </summary>
        /// <returns>The type of invoice line item.</returns>
        public override InvoiceLineItemType InvoiceLineItemType
        {
            get { return InvoiceLineItemType.UsageLineItems; }
        }
    }
}
