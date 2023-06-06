// -----------------------------------------------------------------------
// <copyright file="RetryableHttpCall.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Network
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Logging;
    using Retries;

    /// <summary>
    /// Implements retryable HTTP calls. Use this class with the retry policy you need to implement HTTP call retries.
    /// </summary>
    internal class RetryableHttpCall : IRetryableOperation<Task<HttpResponseMessage>>
    {
        /// <summary>
        /// Responses with codes listed here will not be retried.
        /// </summary>
        private readonly HttpStatusCode[] nonRetryableHttpCodes =
        {
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Forbidden,
            HttpStatusCode.NotFound,
            HttpStatusCode.Conflict,
            HttpStatusCode.ExpectationFailed,
            (HttpStatusCode)429,
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryableHttpCall"/> class.
        /// </summary>
        /// <param name="retryPolicy">An optional retry policy. The default policy will be used if not provided.</param>
        public RetryableHttpCall(IRetryPolicy retryPolicy = null)
        {
            this.RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Gets or set the retry policy.
        /// </summary>
        public IRetryPolicy RetryPolicy { get; set; }

        /// <summary>
        /// Executes an HTTP operation with the configured retry policy.
        /// </summary>
        /// <param name="httpCall">The HTTP call.</param>
        /// <returns>The HTTP response.</returns>
        public async Task<HttpResponseMessage> Execute(Func<Task<HttpResponseMessage>> httpCall)
        {
            if (httpCall == null)
            {
                throw new ArgumentNullException("httpCall");
            }

            if (this.RetryPolicy == null)
            {
                // use the default retry policy
                this.RetryPolicy = PartnerService.Instance.RetryPolicy;
            }

            int attempts = 0;
            HttpResponseMessage response = null;
            Exception operationException = null;

            while (attempts == 0 || this.RetryPolicy.ShouldRetry(attempts))
            {
                try
                {
                    // invoke the HTTP call
                    response = await httpCall.Invoke();
                    operationException = null;

                    if (response.IsSuccessStatusCode)
                    {
                        break;
                    }
                    else
                    {
                        // TODO: log the failed response
                        if (this.nonRetryableHttpCodes.Contains(response.StatusCode))
                        {
                            // the response code should not be retried
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e.IsFatalException())
                    {
                        // we don't want to mask a fatal exception, but rather we should let the runtime handle it
                        throw;
                    }

                    operationException = e;
                    LogManager.Instance.Error(string.Format(CultureInfo.InvariantCulture, "RetryableHttpCall: Attempt {0} failed: {1}", attempts, e));
                }

                // this attempt has failed, increment
                ++attempts;

                if (this.RetryPolicy.ShouldRetry(attempts))
                {
                    // delay according to the retry policy
                    await Task.Delay(this.RetryPolicy.BackOffTime(attempts));
                }
                else
                {
                    // we have exhausted our retries as per the policy
                    break;
                }
            }

            // retry logic is done, throw the operation exception if set, otherwise return the response
            if (operationException != null)
            {
                throw operationException;
            }
            else
            {
                return response;
            }
        }
    }
}