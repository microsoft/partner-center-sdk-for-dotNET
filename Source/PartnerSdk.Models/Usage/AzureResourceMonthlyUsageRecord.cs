// -----------------------------------------------------------------------
// <copyright file="AzureResourceMonthlyUsageRecord.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Usage
{
    /// <summary>
    /// Defines the monthly usage record for an Azure subscription resource.
    /// </summary>
    public sealed class AzureResourceMonthlyUsageRecord : UsageRecordBase
    {
        /// <summary>
        /// Gets or sets the azure resource unique identifier.
        /// </summary>
        public new string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the azure resource.
        /// </summary>
        public new string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the estimated total cost of usage for the azure resource.
        /// </summary>
        public new decimal TotalCost { get; set; }

        /// <summary>
        /// Gets or sets the azure resource category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the azure resource sub-category.
        /// </summary>
        public string Subcategory { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the Azure resource used.
        /// </summary>
        public decimal QuantityUsed { get; set; }

        /// <summary>
        /// Gets or sets the unit of measure for the Azure resource.
        /// </summary>
        public string Unit { get; set; }
    }
}
