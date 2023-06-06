// -----------------------------------------------------------------------
// <copyright file="BillingProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Partners
{
    /// <summary>
    /// Represents a partner's billing profile.
    /// </summary>
    public sealed class BillingProfile : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BillingProfile"/> class.
        /// </summary>
        public BillingProfile()
        {
            this.Address = new Address();
            this.PrimaryContact = new Contact();
        }

        /// <summary>
        /// Gets or sets the billing company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the billing primary contact.
        /// </summary>
        public Contact PrimaryContact { get; set; }

        /// <summary>
        /// Gets or sets the purchase order number.
        /// </summary>
        public string PurchaseOrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the tax Id.
        /// </summary>
        public string TaxId { get; set; }

        /// <summary>
        /// Gets or sets the billing day.
        /// </summary>
        public int? BillingDay { get; set; }

        /// <summary>
        /// Gets or sets the billing currency.
        /// </summary>
        public string BillingCurrency { get; set; }
    }
}