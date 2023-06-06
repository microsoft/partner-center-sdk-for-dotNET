// -----------------------------------------------------------------------
// <copyright file="ResourceAttributes.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines common resource attributes.
    /// </summary>
    public sealed class ResourceAttributes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceAttributes"/> class.
        /// </summary>
        /// <param name="type">The resource type.</param>
        public ResourceAttributes(Type type)
        {
            if (type != null)
            {
                this.ObjectType = type.Name;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceAttributes"/> class.
        /// </summary>
        public ResourceAttributes()
        {
        }

        /// <summary>
        /// Gets or sets the etag.
        /// the object version in providers
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Etag { get; set; }

        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        public string ObjectType { get; set; }
    }
}