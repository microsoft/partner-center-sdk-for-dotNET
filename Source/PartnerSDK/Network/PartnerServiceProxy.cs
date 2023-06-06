// -----------------------------------------------------------------------
// <copyright file="PartnerServiceProxy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Network
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using ErrorHandling;
    using Exceptions;
    using Logging;
    using Models.JsonConverters;
    using Newtonsoft.Json;
    using RequestContext;

    /// <summary>
    /// An implementation of the partner service proxy which automatically serializes request content into JSON payload and deserializes the response from JSON
    /// into the given response type.
    /// </summary>
    /// <typeparam name="TRequest">The type of content that will be passed to the partner service API.</typeparam>
    /// <typeparam name="TResponse">The type of response produced from the partner service API.</typeparam>
    internal class PartnerServiceProxy<TRequest, TResponse> : BasePartnerComponent, IPartnerServiceProxy<TRequest, TResponse>
    {
        /// <summary>
        /// The URI path separator.
        /// </summary>
        private const string UriPathSeparator = @"/";

        /// <summary>
        /// The timeout in minutes for the HttpClient.
        /// </summary>
        private const double TimeoutInMinutes = 3;

        /// <summary>
        /// The request context the proxy will use in executing the network calls.
        /// </summary>
        private IRequestContext requestContext;

        /// <summary>
        /// Setting used on JSON serialization.
        /// </summary>
        private JsonSerializerSettings jsonSerializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerServiceProxy{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="resourcePath">The resource path which will be appended to the root URL.</param>
        /// <param name="errorHandler">An optional handler for failed responses. The default will be used if not provided.</param>
        /// <param name="jsonConverter">An optional JSON response converter. The default will be used if not provided.</param>
        public PartnerServiceProxy(IPartner rootPartnerOperations, string resourcePath, IFailedPartnerServiceResponseHandler errorHandler = null, JsonConverter jsonConverter = null)
            : base(rootPartnerOperations)
        {
            if (this.Partner.RequestContext.RequestId == Guid.Empty)
            {
                // there is not request id assigned, generate one and stick to it (consider retries)
                this.requestContext = RequestContextFactory.Instance.Create(this.Partner.RequestContext.CorrelationId, Guid.NewGuid(), this.Partner.RequestContext.Locale);
            }
            else
            {
                this.requestContext = this.Partner.RequestContext;
            }

            this.ContentType = this.Accept = "application/json";
            this.UriParameters = new List<KeyValuePair<string, string>>();
            this.ResourcePath = resourcePath;
            this.AdditionalRequestHeaders = new List<KeyValuePair<string, string>>();
            this.ErrorHandler = errorHandler ?? new DefaultPartnerServiceErrorHandler();
            this.JsonConverter = jsonConverter;

            this.IsUrlPathAlreadyBuilt = false;

            this.jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new InternalPropertySetterJsonResolver(),
                CheckAdditionalContent = true
            };

            if (this.JsonConverter != null)
            {
                this.jsonSerializerSettings.Converters.Add(this.JsonConverter);
            }
        }

        /// <summary>
        /// Gets or sets the assigned Microsoft Id.
        /// </summary>
        public Guid RequestId
        {
            get
            {
                return this.requestContext.RequestId;
            }

            set
            {
                this.requestContext = RequestContextFactory.Instance.Create(this.requestContext.CorrelationId, value == Guid.Empty ? Guid.NewGuid() : value);
            }
        }

        /// <summary>
        /// Gets or sets the assigned Microsoft correlation Id.
        /// </summary>
        public Guid CorrelationId
        {
            get
            {
                return this.requestContext.CorrelationId;
            }

            set
            {
                this.requestContext = RequestContextFactory.Instance.Create(value, this.requestContext.RequestId);
            }
        }

        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        public string Locale
        {
            get
            {
                return this.requestContext.Locale;
            }

            set
            {
                this.requestContext = RequestContextFactory.Instance.Create(this.requestContext.CorrelationId, this.requestContext.RequestId, value);
            }
        }

        /// <summary>
        /// Gets or sets the e-tag used for concurrency control.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Gets or sets the request content type.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the accepted response type.
        /// </summary>
        public string Accept { get; set; }

        /// <summary>
        /// Gets or sets the continuation token.
        /// </summary>
        public string ContinuationToken { get; set; }

        /// <summary>
        /// Gets or sets the Customer User UPN for license assignment.
        /// </summary>
        public string CustomerUserUpn { get; set; }

        /// <summary>
        /// Gets or sets whether the proxy should build the URL or the URL has already been built.
        /// </summary>
        public bool IsUrlPathAlreadyBuilt { get; set; }

        /// <summary>
        /// Gets the additional request headers.
        /// </summary>
        public ICollection<KeyValuePair<string, string>> AdditionalRequestHeaders { get; private set; }

        /// <summary>
        /// Gets a collection of Uri parameters which will be added to the request query string. You can add your own uri parameters to this collection.
        /// </summary>
        public ICollection<KeyValuePair<string, string>> UriParameters { get; private set; }

        /// <summary>
        /// Gets or sets the resource path which will be appended to the root URL.
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// Gets an optional JSON converter to use.
        /// </summary>
        public JsonConverter JsonConverter { get; private set; }

        /// <summary>
        /// Gets or sets the error handler for non successful responses.
        /// </summary>
        protected virtual IFailedPartnerServiceResponseHandler ErrorHandler { get; set; }

        /// <summary>
        /// Executes a GET request against the partner service.
        /// </summary>
        /// <returns>The GET response.</returns>
        public async Task<TResponse> GetAsync()
        {
            return await this.SendAsync(partnerServiceClient => partnerServiceClient.GetAsync(this.BuildPartnerServiceApiUri()));
        }

        /// <summary>
        /// Executes a file content request against the partner service.
        /// </summary>
        /// <returns>The file content stream.</returns>
        public async Task<Stream> GetFileContentAsync()
        {
            try
            {
                // ensure the credentials are not expired
                await this.ValidateCredentialsAsync();
                var retryableHttpCall = new RetryableHttpCall();

                using (var partnerServiceClient = this.BuildHttpClient())
                {
                    // invoke the HTTP operation
                    var response = await retryableHttpCall.Execute(() => partnerServiceClient.GetAsync(this.BuildPartnerServiceApiUri()));

                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content == null ? null : await response.Content.ReadAsStreamAsync();
                    }
                    else
                    {
                        // handle the failure according to the configured policy
                        throw await this.ErrorHandler.HandleFailedResponse(response, this.requestContext);
                    }
                }
            }
            catch (Exception error)
            {
                if (error.IsFatalException() || error is PartnerException)
                {
                    // if the exception was deliberately thrown or was fatal, let it pass as is
                    throw;
                }

                // otherwise, wrap it
                throw new PartnerException(error.Message, this.requestContext, innerException: error);
            }
        }

        /// <summary>
        /// Executes a POST request against the partner service.
        /// </summary>
        /// <param name="content">The request body content.</param>
        /// <returns>The POST response.</returns>
        public async Task<TResponse> PostAsync(TRequest content)
        {
            return await this.SendAsync(partnerServiceClient => partnerServiceClient.PostAsync(this.BuildPartnerServiceApiUri(), new StringContent(JsonConvert.SerializeObject(content, this.jsonSerializerSettings))));
        }

        /// <summary>
        /// Executes a POST request against the partner service without serializing content.
        /// </summary>
        /// <param name="content">The request body content.</param>
        /// <returns>The POST response.</returns>
        public async Task<TResponse> PostFormDataAsync(MultipartFormDataContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, this.BuildPartnerServiceApiUri()) { Content = content, };

            return await this.SendAsync(partnerServiceClient => partnerServiceClient.SendAsync(request));
        }

        /// <summary>
        /// Executes a PATCH request against the partner service.
        /// </summary>
        /// <param name="content">The request body content.</param>
        /// <returns>The PATCH response.</returns>
        public async Task<TResponse> PatchAsync(TRequest content)
        {
            return await this.SendAsync((HttpClient partnerServiceClient) =>
            {
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), this.BuildPartnerServiceApiUri())
                {
                    Content = new StringContent(JsonConvert.SerializeObject(content, this.jsonSerializerSettings))
                };

                return partnerServiceClient.SendAsync(request);
            });
        }

        /// <summary>
        /// Executes a PUT request against the partner service.
        /// </summary>
        /// <param name="content">The request body content.</param>
        /// <returns>The PUT response.</returns>
        public async Task<TResponse> PutAsync(TRequest content)
        {
            return await this.SendAsync(partnerServiceClient => partnerServiceClient.PutAsync(this.BuildPartnerServiceApiUri(), new StringContent(JsonConvert.SerializeObject(content, this.jsonSerializerSettings))));
        }

        /// <summary>
        /// Executes a DELETE request against the partner service.
        /// </summary>
        /// <returns>The DELETE response task.</returns>
        public async Task DeleteAsync()
        {
            await this.SendAsync(partnerServiceClient => partnerServiceClient.DeleteAsync(this.BuildPartnerServiceApiUri()));
        }

        /// <summary>
        /// Executes a HEAD request against the partner service.
        /// </summary>
        /// <returns>The HEAD response task.</returns>
        public async Task HeadAsync()
        {
            await this.SendAsync(partnerServiceClient => partnerServiceClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, this.BuildPartnerServiceApiUri())));
        }

        /// <summary>
        /// Builds the partner service API Uri based on the configured properties.
        /// </summary>
        /// <returns>The complete partner service API Uri needed to invoke the current operation.</returns>
        protected virtual Uri BuildPartnerServiceApiUri()
        {
            string relativePath = this.ResourcePath ?? string.Empty;
            string versionPathSegment = UriPathSeparator + PartnerService.Instance.ApiVersion;

            // check if the relative path has version included, if not add current version to make it complete
            if (!string.IsNullOrWhiteSpace(relativePath) &&
                !relativePath.StartsWith(versionPathSegment, StringComparison.OrdinalIgnoreCase))
            {
                relativePath = string.Join(UriPathSeparator, versionPathSegment, relativePath.TrimStart(UriPathSeparator.ToCharArray()));
            }

            if (this.IsUrlPathAlreadyBuilt)
            {
                string[] urlParts = relativePath.Split('?');

                return new UriBuilder(PartnerService.Instance.ApiRootUrl)
                {
                    Path = urlParts[0],
                    Query = urlParts.Length > 1 ? urlParts[1] : string.Empty
                }.Uri;
            }
            else
            {
                return new UriBuilder(PartnerService.Instance.ApiRootUrl)
                {
                    Path = relativePath,
                    Query = this.BuildQueryString()
                }.Uri;
            }
        }

        /// <summary>
        /// Builds the HTTP client needed to perform network calls. This method aids in mocking the HttpClient in unit tests
        /// and hence was implemented as protected in order not to expose it to other SDK classes.
        /// </summary>
        /// <returns>A configured HTTP client.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The caller will take care of that.")]
        protected virtual HttpClient BuildHttpClient()
        {
            return new HttpClient(new PartnerServiceHttpMessageHandler<TRequest, TResponse>(this))
            {
                Timeout = TimeSpan.FromMinutes(TimeoutInMinutes)
            };
        }

        /// <summary>
        /// Sends an HTTP request to the partner service after checking that the credentials are not
        /// expired. It will also handle the response.
        /// </summary>
        /// <param name="httpOperation">The HTTP operation to execute.</param>
        /// <returns>A deserialized HTTP response.</returns>
        private async Task<TResponse> SendAsync(Func<HttpClient, Task<HttpResponseMessage>> httpOperation)
        {
            try
            {
                // ensure the credentials are not expired
                await this.ValidateCredentialsAsync();
                var retryableHttpCall = new RetryableHttpCall();

                using (var partnerServiceClient = this.BuildHttpClient())
                {
                    // invoke the HTTP operation
                    var response = await retryableHttpCall.Execute(() => httpOperation(partnerServiceClient));

                    // handle its response
                    return await this.HandleResponseAsync(response);
                }
            }
            catch (Exception error)
            {
                if (error.IsFatalException() || error is PartnerException)
                {
                    // if the exception was deliberately thrown or was fatal, let it pass as is
                    throw;
                }

                // otherwise, wrap it
                throw new PartnerException(error.Message, this.requestContext, innerException: error);
            }
        }

        /// <summary>
        /// Builds the query string part of the REST API URL.
        /// </summary>
        /// <returns>The query string.</returns>
        private string BuildQueryString()
        {
            StringBuilder queryStringBuilder = new StringBuilder();

            foreach (var queryParameter in this.UriParameters)
            {
                if (queryStringBuilder.Length > 0)
                {
                    // this is not the first query parameter
                    queryStringBuilder.Append("&");
                }

                queryStringBuilder.AppendFormat("{0}={1}", queryParameter.Key, queryParameter.Value);
            }

            return queryStringBuilder.ToString();
        }

        /// <summary>
        /// Handles partner service responses.
        /// </summary>
        /// <param name="response">The partner service response.</param>
        /// <returns>The configured response result.</returns>
        private async Task<TResponse> HandleResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string responsePayload = response.Content == null ? string.Empty : await response.Content.ReadAsStringAsync();

                try
                {
                    if (typeof(TResponse) == typeof(HttpResponseMessage))
                    {
                        return (TResponse)Convert.ChangeType(response, typeof(TResponse));
                    }

                    // operation successful, automatically deserialize the response into the expected type and return it
                    return JsonConvert.DeserializeObject<TResponse>(responsePayload, this.jsonSerializerSettings);
                }
                catch (Exception deserializationProblem)
                {
                    if (deserializationProblem.IsFatalException())
                    {
                        // fail over
                        throw;
                    }
                    else
                    {
                        // throw a parsing error and enable the caller to treat the response themselves
                        throw new PartnerResponseParseException(responsePayload, this.requestContext, "Could not deserialize response", deserializationProblem);
                    }
                }
            }
            else
            {
                // handle the failure according to the configured policy
                throw await this.ErrorHandler.HandleFailedResponse(response, this.requestContext);
            }
        }

        /// <summary>
        /// Ensures that the partner credentials are up to date.
        /// </summary>
        /// <returns>A task that is complete when the verification is done.</returns>
        private async Task ValidateCredentialsAsync()
        {
            if (this.Partner.Credentials.IsExpired())
            {
                if (PartnerService.Instance.RefreshCredentials != null)
                {
                    try
                    {
                        // attempt to refresh the credentials
                        await PartnerService.Instance.RefreshCredentials(this.Partner.Credentials, this.requestContext);
                    }
                    catch (Exception refreshProblem)
                    {
                        if (refreshProblem.IsFatalException())
                        {
                            throw;
                        }

                        LogManager.Instance.Error(string.Format(CultureInfo.InvariantCulture, "Refreshing the credentials has failed: {0}.", refreshProblem));
                        throw new PartnerException("Refreshing the credentials has failed.", this.requestContext, PartnerErrorCategory.Unauthorized, refreshProblem);
                    }

                    // ensure the new credentials are not expired
                    if (this.Partner.Credentials.IsExpired())
                    {
                        LogManager.Instance.Error("The credential refresh mechanism provided expired credentials.");
                        throw new PartnerException("The credential refresh mechanism provided expired credentials.", this.requestContext, PartnerErrorCategory.Unauthorized);
                    }
                }
                else
                {
                    // we can't refresh the credentials silently, fail with unauthorized
                    LogManager.Instance.Error("The partner credentials have expired.");
                    throw new PartnerException("The partner credentials have expired. Please provide updated credentials.", this.requestContext, PartnerErrorCategory.Unauthorized);
                }
            }
        }
    }
}