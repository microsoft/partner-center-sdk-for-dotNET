// -----------------------------------------------------------------------
// <copyright file="OrderLineitemLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    using Newtonsoft.Json;

    /// <summary>
    /// Bundles the links for an order line item.
    /// </summary>
    public sealed class OrderLineItemLinks
    {
        /// <summary>
        /// Gets or sets the subscription link for the order line item.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Subscription { get; set; }

        /// <summary>
        /// Gets or sets the Product link for the order line item.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Product { get; set; }

        /// <summary>
        /// Gets or sets the SKU link for the order line item.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Sku { get; set; }

        /// <summary>
        /// Gets or sets the availability link for the order line item.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Availability { get; set; }

        /// <summary>
        /// Gets or sets the provisioning status link for the order line item.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link ProvisioningStatus { get; set; }

        /// <summary>
        /// Gets or sets the subscription link for the order line item.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link ActivationLinks { get; set; }
    }
}
