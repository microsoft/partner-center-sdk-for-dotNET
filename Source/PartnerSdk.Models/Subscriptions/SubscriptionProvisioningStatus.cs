// -----------------------------------------------------------------------
// <copyright file="SubscriptionProvisioningStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System;

    /// <summary>
    /// The subscription provisioning status details.
    /// </summary>
    public sealed class SubscriptionProvisioningStatus : ResourceBase
    {
        /// <summary>
        /// Gets or sets the subscription s.k.u id.
        /// </summary>
        public Guid SkuId { get; set; }

        /// <summary>
        /// Gets or sets the subscription provisioning status.
        /// </summary>
        public ProvisioningStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// Latest Seat number or subscription quantity after provisioning.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the end  date.
        /// Renewal or end date after provisioning.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
