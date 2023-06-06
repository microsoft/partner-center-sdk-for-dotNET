// -----------------------------------------------------------------------
// <copyright file="ResourceBaseWithLinks{TLinks}.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Base class for resources that have links.
    /// </summary>
    /// <typeparam name="TLinks">The type of the links the class holds.</typeparam>
    public abstract class ResourceBaseWithLinks<TLinks> : ResourceBase where TLinks : new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBaseWithLinks{TLinks}"/> class.
        /// </summary>
        /// <param name="objectType">The type of the object.</param>
        protected ResourceBaseWithLinks(string objectType) : base(objectType)
        {
            this.Links = new TLinks();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBaseWithLinks{TLinks}"/> class.
        /// </summary>
        protected ResourceBaseWithLinks() : base()
        {
        }

        /// <summary>
        /// Gets or sets the resource links.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TLinks Links { get; set; }
    }
}
