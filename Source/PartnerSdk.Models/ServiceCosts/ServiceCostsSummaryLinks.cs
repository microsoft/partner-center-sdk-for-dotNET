// -----------------------------------------------------------------------
// <copyright file="ServiceCostsSummaryLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------

namespace Microsoft.Store.PartnerCenter.Models.ServiceCosts
{
    using Newtonsoft.Json;

    /// <summary>
    /// Bundles links with a service costs summary.
    /// </summary>
    public sealed class ServiceCostsSummaryLinks : StandardResourceLinks
    {
        /// <summary>
        /// Gets or sets the service cost line items.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link ServiceCostLineItems { get; set; }
    }
}
