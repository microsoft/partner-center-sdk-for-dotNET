﻿// -----------------------------------------------------------------------
// <copyright file="PartnerLicensesUsageInsights.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Analytics
{
    using System;

    /// <summary>
    /// Holds the analytics information about the usage of all the customers' licenses of the given partner.
    /// </summary>
    public class PartnerLicensesUsageInsights : LicensesInsightsBase
    {
        /// <summary>
        /// Gets or sets the Workload name. For example: Exchange.
        /// </summary>
        public string WorkloadName { get; set; }

        /// <summary>
        /// Gets or sets the usage percentage of the given workload out of qualified licenses of all customers of the partner.
        /// </summary>
        public double ProratedLicensesUsagePercent { get; set; }
    }
}
