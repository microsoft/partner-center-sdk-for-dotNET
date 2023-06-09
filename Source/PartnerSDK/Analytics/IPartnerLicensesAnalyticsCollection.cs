﻿// -----------------------------------------------------------------------
// <copyright file="IPartnerLicensesAnalyticsCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
namespace Microsoft.Store.PartnerCenter.Analytics
{
    /// <summary>
    /// Encapsulates collection of partner level licenses' analytics.
    /// </summary>
    public interface IPartnerLicensesAnalyticsCollection : IPartnerComponent
    {
        /// <summary>
        /// Gets the partner's licenses' deployment analytics.
        /// </summary>
        IPartnerLicensesDeploymentInsightsCollection Deployment { get; }

        /// <summary>
        /// Gets the partner's licenses' usage analytics.
        /// </summary>
        IPartnerLicensesUsageInsightsCollection Usage { get; }
    }
}
