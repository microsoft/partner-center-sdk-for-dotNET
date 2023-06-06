// <copyright file="NewCommerceMigrationEvent.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations
{
    using System;

    /// <summary>
    /// Represents a New-Commerce migration event.
    /// </summary>
    public class NewCommerceMigrationEvent : ResourceBase
    {
        /// <summary>
        /// Gets or sets the ID of the New-Commerce migration event.
        /// </summary>
        /// <value>
        /// The ID of the New-Commerce migration event.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the New-Commerce migration that the event is related to.
        /// </summary>
        /// <value>
        /// The ID of the New-Commerce migration that the event is related to.
        /// </value>
        public string NewCommerceMigrationId { get; set; }

        /// <summary>
        /// Gets or sets the current subscription identifier.
        /// </summary>
        /// <value>
        /// The current subscription identifier.
        /// </value>
        public string CurrentSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the customer tenant identifier.
        /// </summary>
        /// <value>
        /// The customer tenant identifier.
        /// </value>
        public string CustomerTenantId { get; set; }

        /// <summary>
        /// Gets or sets the time when the event was created.
        /// </summary>
        /// <value>
        /// The time when the event was created.
        /// </value>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        /// <value>
        /// The event name.
        /// </value>
        public string EventName { get; set; }
    }
}
