// <copyright file="NewCommerceEligibility.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the New-Commerce migration eligibility information for a given subscription.
    /// </summary>
    public class NewCommerceEligibility : ResourceBase
    {
        /// <summary>
        /// Gets or sets the current subscription ID.
        /// </summary>
        /// <value>
        /// The current subscription ID.
        /// </value>
        public string CurrentSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the subscription is eligible or not for New-Commerce migration.
        /// </summary>
        /// <value>
        /// Value indicating whether the subscription is eligible or not for New-Commerce migration.
        /// </value>
        public bool IsEligible { get; set; }

        /// <summary>
        /// Gets or sets the eligible term duration.
        /// </summary>
        /// <value>
        /// The eligible term duration.
        /// </value>
        public string TermDuration { get; set; }

        /// <summary>
        /// Gets or sets the eligible billing cycle.
        /// </summary>
        /// <value>
        /// The eligible billing cycle.
        /// </value>
        public string BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets the eligible catalog item ID.
        /// </summary>
        /// <value>
        /// The eligible catalog item ID.
        /// </value>
        public string CatalogItemId { get; set; }

        /// <summary>
        /// Gets or sets the eligible custom term end date for the New-Commerce subscription.
        /// </summary>
        /// <value>
        /// The eligible custom term end date for the New-Commerce subscription.
        /// </value>
        public DateTime? CustomTermEndDate { get; set; }

        /// <summary>
        /// Gets or sets the error preventing the subscription from being migration to New Commerce.
        /// </summary>
        /// <value>
        /// The eligibility errors.
        /// </value>
        public IEnumerable<NewCommerceEligibilityError> Errors { get; set; }

        /// <summary>
        /// Gets or sets the collection of eligibility information for the add-on subscriptions being migrated.
        /// </summary>
        /// <value>
        /// The add-on migration eligibilities.
        /// </value>
        public IEnumerable<NewCommerceEligibility> AddOnMigrations { get; set; }
    }
}
