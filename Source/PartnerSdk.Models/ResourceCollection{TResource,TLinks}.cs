// -----------------------------------------------------------------------
// <copyright file="ResourceCollection{TResource,TLinks}.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains a collection of resources.
    /// </summary>
    /// <typeparam name="TResource">Type of resources in collection.</typeparam>
    /// <typeparam name="TLinks">The type of the resource collection links.</typeparam>
    [JsonObject]
    public class ResourceCollection<TResource, TLinks> : ResourceBaseWithLinks<TLinks> where TLinks : new()
    {
        /// <summary>
        /// The collection items.
        /// </summary>
        private readonly ICollection<TResource> internalItems = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollection{TResource,TLinks}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public ResourceCollection(ICollection<TResource> items)
            : base("Collection")
        {
            this.internalItems = items ?? new List<TResource>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollection{TResource,TLinks}" /> class.
        /// </summary>
        /// <param name="resourceCollection">The resource collection to copy from.</param>
        protected ResourceCollection(ResourceCollection<TResource, TLinks> resourceCollection)
            : base("Collection")
        {
            if (resourceCollection == null)
            {
                throw new ArgumentNullException("resourceCollection");
            }

            this.internalItems = resourceCollection.internalItems;
        }

        /// <summary>
        /// Gets the total count of the elements in the resource collection.
        /// </summary>
        [JsonProperty]
        public int TotalCount
        {
            get
            {
                return this.internalItems.Count;
            }
        }

        /// <summary>
        /// Gets the collection items.
        /// </summary>
        public IEnumerable<TResource> Items
        {
            get
            {
                return this.internalItems;
            }
        }

        /// <summary>
        /// Returns a summary of the resource collection.
        /// </summary>
        /// <returns>A summary of the resource collection.</returns>
        public override string ToString()
        {
            StringBuilder collectionDescription = new StringBuilder();

            collectionDescription.AppendFormat("Count: {0}", this.TotalCount).AppendLine();

            if (this.Items != null)
            {
                collectionDescription.AppendLine("Items:");

                foreach (var item in this.Items)
                {
                    collectionDescription.AppendLine(item.ToString());
                }
            }

            return collectionDescription.ToString();
        }
    }
}