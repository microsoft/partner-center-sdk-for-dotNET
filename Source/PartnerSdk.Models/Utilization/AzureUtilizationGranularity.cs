// -----------------------------------------------------------------------
// <copyright file="AzureUtilizationGranularity.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Utilizations
{
    /// <summary>
    /// Lists the time granularity options for retrieving A subscription's azure utilization.
    /// </summary>
    public enum AzureUtilizationGranularity
    {
        /// <summary>
        /// Daily utilization.
        /// </summary>
        Daily,

        /// <summary>
        /// Hourly utilization.
        /// </summary>
        Hourly
    }
}
