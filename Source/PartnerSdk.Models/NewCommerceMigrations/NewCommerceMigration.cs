// <copyright file="NewCommerceMigration.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a New-Commerce migration entity.
    /// </summary>
    public class NewCommerceMigration : ResourceBase
    {
        /// <summary>
        /// Gets or sets the ID of the New-Commerce migration.
        /// </summary>
        /// <value>
        /// Tthe ID of the New-Commerce migration.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the time when the migration was started.
        /// </summary>
        /// <value>
        /// The completed time.
        /// </value>
        public DateTime? StartedTime { get; set; }

        /// <summary>
        /// Gets or sets time when the migration was completed.
        /// </summary>
        /// <value>
        /// The completed time.
        /// </value>
        public DateTime? CompletedTime { get; set; }

        /// <summary>
        /// Gets or sets the current subscription identifier.
        /// </summary>
        /// <value>
        /// The Legacy subscription identifier.
        /// </value>
        public string CurrentSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the collection of add-on migrations.
        /// </summary>
        /// <value>
        /// The collection of add-on migrations.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<NewCommerceMigration> AddOnMigrations { get; set; }

        /// <summary>
        /// Gets or sets the migration status.
        /// </summary>
        /// <value>
        /// The migration status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the customer tenant identifier.
        /// </summary>
        /// <value>
        /// The customer tenant identifier.
        /// </value>
        public string CustomerTenantId { get; set; }

        /// <summary>
        /// Gets or sets the Modern catalog item identifier in the form of {ProductId}:{SkuId}:{AvailbilityId}.
        /// </summary>
        /// <value>
        /// The Modern catalog item identifier in the form of {ProductId}:{SkuId}:{AvailbilityId}.
        /// </value>
        public string CatalogItemId { get; set; }

        /// <summary>
        /// Gets or sets the New Commerce subscription identifier.
        /// </summary>
        /// <value>
        /// The New Commerce subscription identifier.
        /// </value>
        public string NewCommerceSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the New Commerce order identifier.
        /// </summary>
        /// <value>
        /// The New Commerce order identifier.
        /// </value>
        public string NewCommerceOrderId { get; set; }

        /// <summary>
        /// Gets or sets the subscription end date.
        /// </summary>
        /// <value>
        /// The subscription end date.
        /// </value>
        public DateTime? SubscriptionEndDate { get; set; }

        /// <summary>
        /// Gets or sets the custom term end date for the New-Commerce subscription.
        /// </summary>
        /// <value>
        /// The custom term end date for the New-Commerce subscription.
        /// </value>
        public DateTime? CustomTermEndDate { get; set; }

        /// <summary>
        /// Gets or sets the quantity (seat count).
        /// </summary>
        /// <value>
        /// The quantity (seat count).
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the term duration for the New Commerce subscription.
        /// </summary>
        /// <value>
        /// The term duration for the New Commerce subscription.
        /// </value>
        public string TermDuration { get; set; }

        /// <summary>
        /// Gets or sets the billing cycle for the New Commerce subscription.
        /// </summary>
        /// <value>
        /// The billing cycle for the New Commerce subscription.
        /// </value>
        public string BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the New-Commerce subscription should be created with a full term.
        /// </summary>
        /// <value>
        /// Whether the New-Commerce subscription should be created with a full term.
        /// </value>
        public bool PurchaseFullTerm { get; set; }

        /// <summary>
        /// Gets or sets the external reference id. This is used as an indicator for a batch Id when migrations are done in bulk.
        /// </summary>
        public string ExternalReferenceId { get; set; }
    }
}
