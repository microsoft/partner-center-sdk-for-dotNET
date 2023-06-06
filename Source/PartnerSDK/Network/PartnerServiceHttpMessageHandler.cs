// -----------------------------------------------------------------------
// <copyright file="PartnerServiceHttpMessageHandler.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Network
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Appends the needed HTTP headers to the partner service HTTP requests.
    /// </summary>
    /// <typeparam name="T">The type of content that will be passed to the partner service API.</typeparam>
    /// <typeparam name="TU">The type of response produced from the partner service API.</typeparam>
    internal class PartnerServiceHttpMessageHandler<T, TU> : DelegatingHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerServiceHttpMessageHandler{T, TU}"/> class.
        /// </summary>
        /// <param name="partnerServiceProxy">The partner service proxy instance.</param>
        public PartnerServiceHttpMessageHandler(IPartnerServiceProxy<T, TU> partnerServiceProxy)
        {
            if (partnerServiceProxy == null)
            {
                throw new ArgumentNullException("partnerServiceProxy");
            }

            this.PartnerServiceProxy = partnerServiceProxy;
            this.InnerHandler = new HttpClientHandler();
        }

        /// <summary>
        /// Gets or sets the partner service proxy instance this handler will be attached to.
        /// </summary>
        private IPartnerServiceProxy<T, TU> PartnerServiceProxy { get; set; }

        /// <summary>
        /// Injects the headers expected by the partner service.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        internal void InjectHeaders(HttpRequestMessage request)
        {
            // append the headers expected by the partner service APIs
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.PartnerServiceProxy.Partner.Credentials.PartnerServiceToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(this.PartnerServiceProxy.Accept));

            request.Headers.Add("MS-RequestId", this.PartnerServiceProxy.RequestId.ToString());
            request.Headers.Add("MS-CorrelationId", this.PartnerServiceProxy.CorrelationId.ToString());
            request.Headers.Add("X-Locale", this.PartnerServiceProxy.Locale);
            request.Headers.Add("MS-PartnerCenter-Client", PartnerService.Instance.Configuration.PartnerCenterClient);
            request.Headers.Add("MS-SdkVersion", PartnerService.Instance.AssemblyVersion);

            if (!string.IsNullOrWhiteSpace(PartnerService.Instance.ApplicationName))
            {
                request.Headers.Add("MS-PartnerCenter-Application", PartnerService.Instance.ApplicationName);
            }

            if (!string.IsNullOrWhiteSpace(PartnerService.Instance.ApiSubscriptionKey))
            {
                request.Headers.Add("ocp-apim-subscription-key", PartnerService.Instance.ApiSubscriptionKey);
            }

            if (!string.IsNullOrWhiteSpace(this.PartnerServiceProxy.ContinuationToken))
            {
                request.Headers.Add("MS-ContinuationToken", this.PartnerServiceProxy.ContinuationToken);
            }

            if (!string.IsNullOrWhiteSpace(this.PartnerServiceProxy.CustomerUserUpn))
            {
                request.Headers.Add("x-ms-customeruser-upn", this.PartnerServiceProxy.CustomerUserUpn);
            }

            if (PartnerService.Instance.AdditionalHeaders != null && PartnerService.Instance.AdditionalHeaders.Count() > 0)
            {
                foreach (var additionalHeader in PartnerService.Instance.AdditionalHeaders)
                {
                    request.Headers.Add(additionalHeader.Key, additionalHeader.Value);
                }
            }

            if (request.Content != null && !(request.Content.Headers.ContentType?.ToString().IndexOf("multipart/form-data", StringComparison.InvariantCultureIgnoreCase) >= 0))
            {
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(this.PartnerServiceProxy.ContentType);
            }

            if (!string.IsNullOrWhiteSpace(this.PartnerServiceProxy.IfMatch))
            {
                request.Headers.IfMatch.Add(new EntityTagHeaderValue(this.PartnerServiceProxy.IfMatch));
            }

            if (this.PartnerServiceProxy.AdditionalRequestHeaders != null && this.PartnerServiceProxy.AdditionalRequestHeaders.Any())
            {
                foreach (KeyValuePair<string, string> header in this.PartnerServiceProxy.AdditionalRequestHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// Sends the HTTP request.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The HTTP response.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.InjectHeaders(request);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
