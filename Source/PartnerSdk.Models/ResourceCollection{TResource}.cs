// -----------------------------------------------------------------------
// <copyright file="ResourceCollection{TResource}.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Holds a collection of resources with the default collection resource links.
    /// </summary>
    /// <typeparam name="TResource">Type of resources in collection.</typeparam>
    [JsonObject]
    public class ResourceCollection<TResource> : ResourceCollection<TResource, StandardResourceCollectionLinks>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollection{TResource}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public ResourceCollection(ICollection<TResource> items)
            : base(items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollection{TResource}" /> class.
        /// </summary>
        /// <param name="resourceCollection">The resource collection to copy from.</param>
        protected ResourceCollection(ResourceCollection<TResource> resourceCollection)
            : base(resourceCollection)
        {
        }
    }
}