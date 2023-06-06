// <copyright file="NewCommerceMigrationSchedule.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.NewCommerceMigrationSchedules
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a New-Commerce migration entity.
    /// </summary>
    public class NewCommerceMigrationSchedule : ResourceBase
    {
        /// <summary>
        /// Gets or sets the ID of the New-Commerce migration schedule.
        /// </summary>
        /// <value>
        /// The ID of the New-Commerce migration schedule.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the current subscription identifier.
        /// </summary>
        /// <value>
        /// The Legacy subscription identifier.
        /// </value>
        public string CurrentSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the subscription end date.
        /// </summary>
        /// <value>
        /// subscription end date.
        /// </value>
        public DateTime? SubscriptionEndDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of add-on migration schedule.
        /// </summary>
        /// <value>
        /// The collection of add-on migration schedule.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<NewCommerceMigrationSchedule> AddOnMigrationSchedules { get; set; }

        /// <summary>
        /// Gets or sets the migration schedule status.
        /// </summary>
        /// <value>
        /// The migration schedule status.
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
        /// This is set when schedule is created/updated but during migration it is possible that
        /// we might get a new availability id.
        /// </summary>
        /// <value>
        /// The Modern catalog item identifier in the form of {ProductId}:{SkuId}:{AvailbilityId}.
        /// </value>
        public string CatalogItemId { get; set; }

        /// <summary>
        /// Gets or sets the quantity (seat count).
        /// <value>
        /// The quantity (seat count).
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the term duration for the New Commerce subscription.
        /// <value>
        /// The term duration for the New Commerce subscription.
        /// </value>
        public string TermDuration { get; set; }

        /// <summary>
        /// Gets or sets the billing cycle for the New Commerce subscription.
        /// <value>
        /// The billing cycle for the New Commerce subscription.
        /// </value>
        public string BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the New-Commerce subscription should be created with a full term.
        /// <value>
        /// Whether the New-Commerce subscription should be created with a full term.
        /// </value>
        public bool PurchaseFullTerm { get; set; }

        /// <summary>
        /// Gets or sets the custom term end date for the New-Commerce subscription.
        /// </summary>
        /// <value>
        /// The custom term end date for the New-Commerce subscription.
        /// </value>
        public DateTime? CustomTermEndDate { get; set; }

        /// <summary>
        /// Gets or sets the external reference id. This is used as an indicator for a batchid when migrations are done in bulk.
        /// </summary>
        public string ExternalReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the target date for migration.
        /// </summary>
        public DateTime? TargetDate { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating whether to schedule the migration on renewal.
        /// </summary>
        public bool? MigrateOnRenewal { get; set; }

        /// <summary>
        /// Gets or sets the migration schedule created date time.
        /// </summary>
        public DateTimeOffset? CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the migration schedule last modified date time.
        /// </summary>
        public DateTimeOffset? LastModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the ID of the New-Commerce migration created after processing the schedule.
        /// </summary>
        public string NewCommerceMigrationId { get; set; }

        /// <summary>
        /// Gets or sets the description of the error that prevented the migration schedule from getting processed.
        /// </summary>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Gets or sets the code of the error that prevented the migration schedule from getting processed.
        /// </summary>
        public int? ErrorCode { get; set; }
    }
}