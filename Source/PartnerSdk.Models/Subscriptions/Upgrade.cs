﻿// -----------------------------------------------------------------------
// <copyright file="Upgrade.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System.Collections.Generic;
    using Offers;

    /// <summary>
    /// Represents a form of upgrade for a subscription.
    /// </summary>
    public sealed class Upgrade : ResourceBase
    {
        /// <summary>
        /// Gets or sets the offer to upgrade to.
        /// </summary>
        public Offer TargetOffer { get; set; }

        /// <summary>
        /// Gets or sets the type of upgrade.
        /// </summary>
        public UpgradeType UpgradeType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the upgrade can be performed.
        /// </summary>
        public bool IsEligible { get; set; }

        /// <summary>
        /// Gets or sets the quantity to be purchased.  Defaults to the source subscription quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the reasons the upgrade cannot be performed, if applicable.
        /// </summary>
        public IEnumerable<UpgradeError> UpgradeErrors { get; set; }
    }
}