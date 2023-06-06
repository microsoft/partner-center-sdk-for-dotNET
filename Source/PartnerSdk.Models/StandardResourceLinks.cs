// -----------------------------------------------------------------------
// <copyright file="StandardResourceLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the standard links associated with a resource.
    /// </summary>
    public class StandardResourceLinks
    {
        /// <summary>
        /// Gets or sets the self URI.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Self { get; set; }
    }
}