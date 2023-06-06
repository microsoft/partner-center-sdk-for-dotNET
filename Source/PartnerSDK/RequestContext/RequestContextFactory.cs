// -----------------------------------------------------------------------
// <copyright file="RequestContextFactory.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.RequestContext
{
    using System;

    /// <summary>
    /// Creates instances of <see cref="IRequestContext"/>.
    /// </summary>
    public sealed class RequestContextFactory
    {
        /// <summary>
        /// A singleton instance of the request context factory.
        /// </summary>
        private static Lazy<RequestContextFactory> instance = new Lazy<RequestContextFactory>(() => new RequestContextFactory());

        /// <summary>
        /// Prevents a default instance of the <see cref="RequestContextFactory"/> class from being created.
        /// </summary>
        private RequestContextFactory()
        {
        }

        /// <summary>
        /// Gets an instance of the request context factory.
        /// </summary>
        public static RequestContextFactory Instance
        {
            get
            {
                return RequestContextFactory.instance.Value;
            }
        }

        /// <summary>
        /// Creates a request context object which will use a randomly generated correlation Id and a unique request Id for each partner API call.
        /// </summary>
        /// <returns>A request context object.</returns>
        public IRequestContext Create()
        {
            return new RequestContext();
        }

        /// <summary>
        /// Creates a request context object which will use a randomly generated correlation Id, a unique request Id and provided locale for each partner API call.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <returns>A request context object.</returns>
        public IRequestContext Create(string locale)
        {
            return new RequestContext(locale);
        }

        /// <summary>
        /// Creates a request context object with the provided correlation Id and a unique request Id for each partner API call.
        /// </summary>
        /// <param name="correlationId">The correlation Id.</param>
        /// <returns>A request context object.</returns>
        public IRequestContext Create(Guid correlationId)
        {
            return new RequestContext(correlationId);
        }

        /// <summary>
        /// Creates a request context object with the provided correlation Id, a unique request Id and provided locale for each partner API call.
        /// </summary>
        /// <param name="correlationId">The correlation Id.</param>
        /// <param name="locale">The locale</param>
        /// <returns>A request context object.</returns>
        public IRequestContext Create(Guid correlationId, string locale)
        {
            return new RequestContext(correlationId, locale);
        }

        /// <summary>
        /// Creates a request context object with the provided correlation and request Ids.
        /// </summary>
        /// <param name="correlationId">The correlation Id.</param>
        /// <param name="requestId">The request Id.</param>
        /// <returns>A request context object.</returns>
        public IRequestContext Create(Guid correlationId, Guid requestId)
        {
            return new RequestContext(correlationId, requestId);
        }

        /// <summary>
        /// Creates a request context object with the provided correlation, request Ids and locale.
        /// </summary>
        /// <param name="correlationId">The correlation Id.</param>
        /// <param name="requestId">The request Id.</param>
        /// <param name="locale">The locale.</param>
        /// <returns>A request context object.</returns>
        public IRequestContext Create(Guid correlationId, Guid requestId, string locale)
        {
            return new RequestContext(correlationId, requestId, locale);
        }
    }
}
