// -----------------------------------------------------------------------
// <copyright file="PartnerExtensions.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using Network;
    using Newtonsoft.Json;

    /// <summary>
    /// Holds useful extension methods used across the partner SDK.
    /// </summary>
    public static class PartnerExtensions
    {
        /// <summary>
        /// Checks if an exception is fatal.
        /// </summary>
        /// <param name="ex">The exception to check.</param>
        /// <returns>True if Exception is fatal and process should die.</returns>
        public static bool IsFatalException(this Exception ex)
        {
            return ex != null && (ex is OutOfMemoryException || ex is AppDomainUnloadedException || ex is BadImageFormatException
                || ex is CannotUnloadAppDomainException || ex is InvalidProgramException || ex is ThreadAbortException || ex is StackOverflowException);
        }

        /// <summary>
        /// Invokes a link with a request body and returns the result.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request body.</typeparam>
        /// <typeparam name="TResponse">The return type of the invocation.</typeparam>
        /// <param name="link">The link to invoke.</param>
        /// <param name="partnerOperations">A partner operations object from which credentials and context are extracted.</param>
        /// <param name="requestBody">The request body to send.</param>
        /// <param name="converter">An optional JSON converter to be used in encoding and decoding requests and responses.</param>
        /// <returns>The link invocation result.</returns>
        public static TResponse Invoke<TRequest, TResponse>(this Link link, IPartner partnerOperations, TRequest requestBody, JsonConverter converter = null)
        {
            return PartnerService.Instance.SynchronousExecute<TResponse>(() => link.InvokeAsync<TRequest, TResponse>(partnerOperations, requestBody, converter));
        }

        /// <summary>
        /// Asynchronously invokes a link with a request body and returns the result.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request body.</typeparam>
        /// <typeparam name="TResponse">The return type of the invocation.</typeparam>
        /// <param name="link">The link to invoke.</param>
        /// <param name="partnerOperations">A partner operations object from which credentials and context are extracted.</param>
        /// <param name="requestBody">The request body to send.</param>
        /// <param name="converter">An optional JSON converter to be used in encoding and decoding requests and responses.</param>
        /// <returns>The link invocation result.</returns>
        public static async Task<TResponse> InvokeAsync<TRequest, TResponse>(this Link link, IPartner partnerOperations, TRequest requestBody, JsonConverter converter = null)
        {
            if (link == null)
            {
                throw new ArgumentNullException("link should not be null");
            }

            if (partnerOperations == null)
            {
                throw new ArgumentNullException("partnerOperations should not be null");
            }

            var partnerServiceProxy = new PartnerServiceProxy<TRequest, TResponse>(
                partnerOperations,
                link.Uri.ToString(),
                jsonConverter: converter);

            // the links already contains the query parameters, let's not replicate them
            partnerServiceProxy.IsUrlPathAlreadyBuilt = true;

            // add the headers
            foreach (var header in link.Headers)
            {
                partnerServiceProxy.AdditionalRequestHeaders.Add(new KeyValuePair<string, string>(header.Key, header.Value));
            }

            Task<TResponse> linkInvocationOperation = null;

            // invoke the right HTTP method
            switch (link.Method.ToUpper())
            {
                case "POST":
                    linkInvocationOperation = partnerServiceProxy.PostAsync(requestBody);
                    break;
                case "PUT":
                    linkInvocationOperation = partnerServiceProxy.PutAsync(requestBody);
                    break;
                case "PATCH":
                    linkInvocationOperation = partnerServiceProxy.PatchAsync(requestBody);
                    break;
                case "DELETE":
                    linkInvocationOperation = partnerServiceProxy.DeleteAsync().ContinueWith<TResponse>((task) => default(TResponse));
                    break;
                case "HEAD":
                    linkInvocationOperation = partnerServiceProxy.HeadAsync().ContinueWith<TResponse>((task) => default(TResponse));
                    break;
                default:
                    linkInvocationOperation = partnerServiceProxy.GetAsync();
                    break;
            }

            return await linkInvocationOperation;
        }

        /// <summary>
        /// Invokes a link and returns the result.
        /// </summary>
        /// <typeparam name="TResponse">The return type of the invocation.</typeparam>
        /// <param name="link">The link to invoke.</param>
        /// <param name="partnerOperations">A partner operations object from which credentials and context are extracted.</param>
        /// <param name="converter">An optional JSON converter to be used in encoding and decoding requests and responses.</param>
        /// <returns>The link invocation result.</returns>
        public static TResponse Invoke<TResponse>(this Link link, IPartner partnerOperations, JsonConverter converter = null)
        {
            return PartnerService.Instance.SynchronousExecute<TResponse>(() => link.InvokeAsync<TResponse>(partnerOperations, converter));
        }

        /// <summary>
        /// Asynchronously invokes a link and returns the result.
        /// </summary>
        /// <typeparam name="TResponse">The return type of the invocation.</typeparam>
        /// <param name="link">The link to invoke.</param>
        /// <param name="partnerOperations">A partner operations object from which credentials and context are extracted.</param>
        /// <param name="converter">An optional JSON converter to be used in encoding and decoding requests and responses.</param>
        /// <returns>The link invocation result.</returns>
        public static async Task<TResponse> InvokeAsync<TResponse>(this Link link, IPartner partnerOperations, JsonConverter converter = null)
        {
            return await link.InvokeAsync<TResponse, TResponse>(partnerOperations, default(TResponse), converter);
        }
    }
}
