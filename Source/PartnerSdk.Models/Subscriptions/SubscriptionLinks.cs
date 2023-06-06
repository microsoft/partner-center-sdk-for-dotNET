// -----------------------------------------------------------------------
// <copyright file="SubscriptionLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using Newtonsoft.Json;

    /// <summary>
    /// Bundles links associated with a subscription.
    /// </summary>
    public sealed class SubscriptionLinks : StandardResourceLinks
    {
        /// <summary>
        /// Gets or sets the offer link associated with the subscription.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Offer { get; set; }
        
        /// <summary>
        /// Gets or sets the entitlement.
        /// </summary>
        /// <value>
        /// The entitlement.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Entitlement { get; set; }

        /// <summary>
        /// Gets or sets the parent subscription link.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link ParentSubscription { get; set; }

        /// <summary>
        /// Gets or sets the product Link.
        /// </summary>
        /// <value>
        /// The product link.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Product { get; set; }

        /// <summary>
        /// Gets or sets the sku link.
        /// </summary>
        /// <value>
        /// The sku link.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Sku { get; set; }

        /// <summary>
        /// Gets or sets the availability link.
        /// </summary>
        /// <value>
        /// The availability link.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Availability { get; set; }

        /// <summary>
        /// Gets or sets the activation uris.
        /// </summary>
        /// <value>
        /// The Activation links.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link ActivationLinks { get; set; }
    }
}
