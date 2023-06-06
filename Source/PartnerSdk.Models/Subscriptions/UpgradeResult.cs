// -----------------------------------------------------------------------
// <copyright file="UpgradeResult.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the result of performing a subscription Upgrade.
    /// </summary>
    public sealed class UpgradeResult : ResourceBase
    {
        /// <summary>
        /// Gets or sets the source subscription id.
        /// </summary>
        public string SourceSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the target subscription id.
        /// </summary>
        public string TargetSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the type of upgrade.
        /// </summary>
        public UpgradeType UpgradeType { get; set; }

        /// <summary>
        /// Gets or sets the errors encountered while attempting to perform the upgrade, if applicable.
        /// </summary>
        public IEnumerable<UpgradeError> UpgradeErrors { get; set; }

        /// <summary>
        /// Gets or sets the errors encountered while attempting to migrate user licenses, if applicable.
        /// </summary>
        public IEnumerable<UserLicenseError> LicenseErrors { get; set; }
    }
}