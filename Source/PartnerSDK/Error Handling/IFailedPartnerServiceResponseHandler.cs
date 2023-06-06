// -----------------------------------------------------------------------
// <copyright file="IFailedPartnerServiceResponseHandler.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ErrorHandling
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Exceptions;
    using RequestContext;
    
    /// <summary>
    /// Defines behavior for handling non successful responses from the partner service.
    /// </summary>
    internal interface IFailedPartnerServiceResponseHandler
    {
        /// <summary>
        /// Handles failed partner service responses.
        /// </summary>
        /// <param name="response">The partner service response.</param>
        /// <param name="context">An optional partner context.</param>
        /// <returns>The exception to throw.</returns>
        Task<PartnerException> HandleFailedResponse(HttpResponseMessage response, IRequestContext context = null);
    }
}
