// -----------------------------------------------------------------------
// <copyright file="DefaultPartnerServiceErrorHandler.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ErrorHandling
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Exceptions;
    using Logging;
    using Models;
    using Newtonsoft.Json;
    using RequestContext;

    /// <summary>
    /// The default handling policy for failed partner service responses.
    /// </summary>
    internal class DefaultPartnerServiceErrorHandler : IFailedPartnerServiceResponseHandler
    {
        /// <summary>
        /// Handles failed partner service responses.
        /// </summary>
        /// <param name="response">The partner service response.</param>
        /// <param name="context">An optional partner context.</param>
        /// <returns>The exception to throw.</returns>
        public async Task<PartnerException> HandleFailedResponse(HttpResponseMessage response, IRequestContext context = null)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }

            if (response.IsSuccessStatusCode)
            {
                throw new ArgumentException("DefaultPartnerServiceErrorHandler: response is successful.");
            }

            string responsePayload = response.Content != null ? await response.Content.ReadAsStringAsync() : string.Empty;
            ApiFault apiFault = null;
            PartnerException partnerException = null;

            // log the failed response
            LogManager.Instance.Error(string.Format(CultureInfo.InvariantCulture, "Partner service failed response: {0}", responsePayload));

            try
            {
                // attempt to deserialize the response into an ApiFault object as this is what the partner service is expected to do when it errors out
                apiFault = JsonConvert.DeserializeObject<ApiFault>(responsePayload);
            }
            catch (Exception deserializationProblem)
            {
                if (deserializationProblem.IsFatalException())
                {
                    throw;
                }

                LogManager.Instance.Error("Could not parse error response: " + deserializationProblem.ToString());
            }

            PartnerErrorCategory errorCategory = ToPartnerErrorCategory(response.StatusCode);
            if (!string.IsNullOrEmpty(apiFault?.ErrorCode) || !string.IsNullOrEmpty(apiFault?.ErrorMessage) || apiFault?.ErrorData != null)
            {
                partnerException = new PartnerException(apiFault, context, errorCategory);
            }
            else
            {
                // Handle TooManyRequests Error
                if (response.StatusCode == (HttpStatusCode)429)
                {
                    partnerException = new PartnerException(string.IsNullOrWhiteSpace(responsePayload) ? response.ReasonPhrase : responsePayload, context, errorCategory, retryAfter: response.Headers.RetryAfter.Delta);
                }
                else
                {

                    partnerException = new PartnerException(string.IsNullOrWhiteSpace(responsePayload) ? response.ReasonPhrase : responsePayload, context, errorCategory);
                }
            }

            return partnerException;
        }

        /// <summary>
        /// Generates the partner error category based on the HTTP response code.
        /// </summary>
        /// <param name="statusCode">The HTTP response code.</param>
        /// <returns>The partner error category.</returns>
        private static PartnerErrorCategory ToPartnerErrorCategory(HttpStatusCode statusCode)
        {
            PartnerErrorCategory errorCategory = PartnerErrorCategory.NotSpecified;

            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    errorCategory = PartnerErrorCategory.BadInput;
                    break;
                case HttpStatusCode.Unauthorized:
                    errorCategory = PartnerErrorCategory.Unauthorized;
                    break;
                case HttpStatusCode.Forbidden:
                    errorCategory = PartnerErrorCategory.Forbidden;
                    break;
                case HttpStatusCode.NotFound:
                    errorCategory = PartnerErrorCategory.NotFound;
                    break;
                case HttpStatusCode.MethodNotAllowed:
                    errorCategory = PartnerErrorCategory.InvalidOperation;
                    break;
                case HttpStatusCode.NotAcceptable:
                    errorCategory = PartnerErrorCategory.UnsupportedDataFormat;
                    break;
                case HttpStatusCode.Conflict:
                    errorCategory = PartnerErrorCategory.AlreadyExists;
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    errorCategory = PartnerErrorCategory.ServerBusy;
                    break;
                case (HttpStatusCode)429:
                    errorCategory = PartnerErrorCategory.TooManyRequests;
                    break;
                case HttpStatusCode.RequestTimeout:
                    errorCategory = PartnerErrorCategory.RequestTimeout;
                    break;
                case HttpStatusCode.GatewayTimeout:
                    errorCategory = PartnerErrorCategory.GatewayTimeout;
                    break;
                default:
                    errorCategory = PartnerErrorCategory.ServerError;
                    break;
            }

            return errorCategory;
        }
    }
}