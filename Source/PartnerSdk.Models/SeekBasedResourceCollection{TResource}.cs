// -----------------------------------------------------------------------
// <copyright file="SeekBasedResourceCollection{TResource}.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A resource collection with a continuation token that enables callers to seek through the collection pages.
    /// </summary>
    /// <typeparam name="TResource">The type of the objects in collection.</typeparam>
    [JsonObject]
    public sealed class SeekBasedResourceCollection<TResource> : ResourceCollection<TResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeekBasedResourceCollection{TResource}"/> class.
        /// </summary>
        /// <param name="items">The items collection.</param>
        public SeekBasedResourceCollection(ICollection<TResource> items)
            : base(items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeekBasedResourceCollection{TResource}"/> class.
        /// </summary>
        /// <param name="items">The items collection.</param>
        /// <param name="continuationToken">The continuation token.</param>
        [JsonConstructor]
        public SeekBasedResourceCollection(ICollection<TResource> items, string continuationToken)
            : this(items)
        {
            this.ContinuationToken = continuationToken;
        }

        /// <summary>
        /// Gets the continuation token.
        /// </summary>
        public string ContinuationToken { get; private set; }
    }
}