﻿// -----------------------------------------------------------------------
// <copyright file="Entitlement.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Entitlements
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents an entitlement
    /// </summary>
    public class Entitlement : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets included entitlements.
        /// </summary>
        /// <value>
        /// Collection of included entitlements.
        /// </value>
        public IEnumerable<Entitlement> IncludedEntitlements { get; set; }

        /// <summary>
        /// Gets or sets reference order related to the entitlement.
        /// </summary>
        public ReferenceOrder ReferenceOrder { get; set; }

        /// <summary>
        /// Gets or sets product id.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the quantity details (quantity - state).
        /// </summary>
        public IEnumerable<QuantityDetail> QuantityDetails { get; set; }

        /// <summary>
        /// Gets or sets collection of entitled artifacts.
        /// </summary>
        public IEnumerable<Artifact> EntitledArtifacts { get; set; }

        /// <summary>
        /// Gets or sets skuId.
        /// </summary>
        public string SkuId { get; set; }

        /// <summary>
        /// Gets or sets entitlement type.
        /// </summary>
        public string EntitlementType { get; set; }

        /// <summary>
        /// Gets or sets the fulfillment state for the current entitlement.
        /// </summary>
        public string FulfillmentState { get; set; }

        /// <summary>
        /// Gets or sets the entitlement expiry date.
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
    }
}
