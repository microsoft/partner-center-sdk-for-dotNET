// -----------------------------------------------------------------------
// <copyright file="Link.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a URI and the HTTP method which indicates the desired action for accessing the resource.
    /// </summary>
    public sealed class Link
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public Link(Uri uri) : this(uri, "GET")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="headers">Optional HTTP headers.</param>
        [JsonConstructor]
        public Link(Uri uri, string method, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            this.Uri = uri;
            this.Method = method;
            this.Headers = headers ?? new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        public Uri Uri { get; private set; }

        /// <summary>
        /// Gets the method.
        /// </summary>
        public string Method { get; private set; }

        /// <summary>
        /// Gets the link headers.
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Headers { get; private set; }
    }
}