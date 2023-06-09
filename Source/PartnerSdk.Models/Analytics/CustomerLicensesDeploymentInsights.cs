﻿// -----------------------------------------------------------------------
// <copyright file="CustomerLicensesDeploymentInsights.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Analytics
{
    using System;

    /// <summary>
    /// Holds analytics information about deployment of licenses at the customer level.
    /// </summary>
    public class CustomerLicensesDeploymentInsights : CustomerLicensesInsightsBase
    {
        /// <summary>
        /// Gets or sets the number of License/seats deployed as of processed date.
        /// </summary>
        public long LicensesDeployed { get; set; }

        /// <summary>
        /// Gets or sets the number of sold seats/licenses as of processed time stamp.
        /// </summary>
        public long LicensesSold { get; set; }

        /// <summary>
        /// Gets or sets the deployment percent of the licenses of the customer.
        /// </summary>
        public double DeploymentPercent { get; set; }
    }
}
