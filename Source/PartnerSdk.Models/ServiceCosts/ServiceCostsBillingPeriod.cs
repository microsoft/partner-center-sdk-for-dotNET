// -----------------------------------------------------------------------
// <copyright file="ServiceCostsBillingPeriod.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ServiceCosts
{
    /// <summary>
    /// Represents service costs billing periods.
    /// </summary>
    public enum ServiceCostsBillingPeriod
    {
        /// <summary>
        /// Default billing period, does not mean anything.
        /// </summary>
        None = 0,

        /// <summary>
        /// The most recently completed billing period.
        /// </summary>
        MostRecent,

        /// <summary>
        /// The current billing period that is ongoing.
        /// </summary>
        Current
    }
}
