// -----------------------------------------------------------------------
// <copyright file="OverageLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using Newtonsoft.Json;

    /// <summary>
    /// Bundles links associated with an overage.
    /// </summary>
    public sealed class OverageLinks : StandardResourceLinks
    {
        /// <summary>
        /// Gets or sets the overage link.
        /// </summary>
        /// <value>
        /// The overage.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Overage { get; set; }
    }
}
