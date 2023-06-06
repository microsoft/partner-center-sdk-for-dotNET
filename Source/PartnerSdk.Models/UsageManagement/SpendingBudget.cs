// -----------------------------------------------------------------------
// <copyright file="SpendingBudget.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Partner.Sdk.Models.UsageManagement
{
    using Microsoft.Store.PartnerCenter.Models;

    /// <summary>
    /// The spending budget allocated to the customer by the partner.
    /// </summary>
    public class SpendingBudget : ResourceBase
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal? Amount { get; set; }
    }
}
