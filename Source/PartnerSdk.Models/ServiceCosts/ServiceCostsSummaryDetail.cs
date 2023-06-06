// -----------------------------------------------------------------------
// <copyright file="ServiceCostsSummaryDetail.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ServiceCosts
{
    /// <summary>
    /// Represent a service cost summary of individual details for an invoice type. for example recurring, onetime.
    /// </summary>
    public class ServiceCostsSummaryDetail
    {
        /// <summary>
        /// Gets or sets the invoice type that the service cost are attached to.
        /// </summary>
        public string InvoiceType { get; set; }

        /// <summary>
        /// Gets or sets the service cost summary.
        /// </summary>
        public ServiceCostsSummary Summary { get; set; }
    }
}
