// -----------------------------------------------------------------------
// <copyright file="IPartnerServiceProxy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// This interface acts as a mediator between the SDK and the partner API service. It automatically adds customer HTTP headers and allows
    /// configuring them before executing the requests. It also handles responses in a standard manner.
    /// </summary>
    /// <typeparam name="TRequest">The type of content that will be passed to the partner service API.</typeparam>
    /// <typeparam name="TResponse">The type of response produced from the partner service API.</typeparam>
    internal interface IPartnerServiceProxy<TRequest, TResponse> : IPartnerComponent
    {
        /// <summary>
        /// Gets or sets the assigned Microsoft Id.
        /// </summary>
        Guid RequestId { get; set; }

        /// <summary>
        /// Gets or sets the assigned Microsoft correlation Id.
        /// </summary>
        Guid CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the assigned Locale.
        /// </summary>
        string Locale { get; set; }

        /// <summary>
        /// Gets or sets the e-tag used for concurrency control.
        /// </summary>
        string IfMatch { get; set; }

        /// <summary>
        /// Gets or sets the request content type.
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the accepted response type.
        /// </summary>
        string Accept { get; set; }

        /// <summary>
        /// Gets or sets the continuation token.
        /// </summary>
        string ContinuationToken { get; set; }

        /// <summary>
        /// Gets or sets the Customer User UPN for license assignment.
        /// </summary>
        string CustomerUserUpn { get; set; }

        /// <summary>
        /// Gets the additional request headers.
        /// </summary>
        ICollection<KeyValuePair<string, string>> AdditionalRequestHeaders { get; }

        /// <summary>
        /// Gets a collection of Uri parameters which will be added to the request query string. You can add your own uri parameters to this collection.
        /// </summary>
        ICollection<KeyValuePair<string, string>> UriParameters { get; }

        /// <summary>
        /// Gets or sets the resource path which will be appended to the root URL.
        /// </summary>
        string ResourcePath { get; set; }

        /// <summary>
        /// Executes a GET request against the partner service.
        /// </summary>
        /// <returns>The GET response.</returns>
        Task<TResponse> GetAsync();

        /// <summary>
        /// Executes a file content request against the partner service.
        /// </summary>
        /// <returns>The file content stream.</returns>
        Task<Stream> GetFileContentAsync();

        /// <summary>
        /// Executes a POST request against the partner service.
        /// </summary>
        /// <param name="content">The request body content.</param>
        /// <returns>The POST response.</returns>
        Task<TResponse> PostAsync(TRequest content);

        /// <summary>
        /// Executes a PATCH request against the partner service.
        /// </summary>
        /// <param name="content">The request body content.</param>
        /// <returns>The PATCH response.</returns>
        Task<TResponse> PatchAsync(TRequest content);

        /// <summary>
        /// Executes a PUT request against the partner service.
        /// </summary>
        /// <param name="content">The request body content.</param>
        /// <returns>The PUT response.</returns>
        Task<TResponse> PutAsync(TRequest content);

        /// <summary>
        /// Executes a DELETE request against the partner service.
        /// </summary>
        /// <returns>The DELETE response task.</returns>
        Task DeleteAsync();

        /// <summary>
        /// Executes a HEAD request against the partner service.
        /// </summary>
        /// <returns>The HEAD response.</returns>
        Task HeadAsync();
    }
}
