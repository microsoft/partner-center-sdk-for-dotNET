// -----------------------------------------------------------------------
// <copyright file="OfferLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Offers
{
    using Newtonsoft.Json;

    /// <summary>
    /// Bundles offer links.
    /// </summary>
    public sealed class OfferLinks : StandardResourceLinks
    {
        /// <summary>
        /// Gets or sets the learn more link.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link LearnMore { get; set; }
    }
}
