// -----------------------------------------------------------------------
// <copyright file="OrderLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    using Newtonsoft.Json;

    /// <summary>
    /// Bundles the links for an order.
    /// </summary>
    public sealed class OrderLinks : StandardResourceLinks
    {
        /// <summary>
        /// Gets or sets the link to the provisioning status of an order.
        /// </summary>
        /// <value>
        /// The provisioning status of an order.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link ProvisioningStatus { get; set; }

        /// <summary>
        /// Gets or sets the link to the patch operation.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link PatchOperation { get; set; }
    }
}