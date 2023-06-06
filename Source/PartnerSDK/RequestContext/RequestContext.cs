// -----------------------------------------------------------------------
// <copyright file="RequestContext.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.RequestContext
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Request context implementation.
    /// </summary>
    internal class RequestContext : IRequestContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestContext"/> class. Correlation Id will be generated. The request Id will be automatically generated
        /// for each service API call.
        /// </summary>
        public RequestContext()
            : this(Guid.NewGuid(), Guid.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestContext"/> class with provided locale. Correlation Id will be generated. The request Id will be automatically generated
        /// for each service API call.
        /// </summary>
        /// <param name="locale">The locale.</param>
        public RequestContext(string locale)
            : this(Guid.NewGuid(), Guid.Empty, locale)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestContext"/> class with a correlation Id. The request Id will be automatically generated
        /// and a default Locale is set for each service API call.
        /// </summary>
        /// <param name="correlationId">The correlation Id. This Id is used to group logical operations together.</param>
        public RequestContext(Guid correlationId)
            : this(correlationId, Guid.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestContext"/> class with a correlation Id and the provided locale. The request Id will be automatically generated
        /// for each service API call.
        /// </summary>
        /// <param name="correlationId">The correlation Id. This Id is used to group logical operations together.</param>
        /// <param name="locale">The locale.</param>
        public RequestContext(Guid correlationId, string locale)
            : this(correlationId, Guid.Empty, locale)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestContext"/> class with the given correlation, request Ids and locale.
        /// </summary>
        /// <param name="correlationId">The correlation Id. This Id is used to group logical operations together.</param>
        /// <param name="requestId">The request Id. Uniquely identifies the operation.</param>
        /// <param name="locale">The locale.</param>
        public RequestContext(Guid correlationId, Guid requestId, string locale = null)
        {
            if (string.IsNullOrWhiteSpace(locale))
            {
                locale = PartnerService.Instance.Configuration.DefaultLocale;
            }

            this.CorrelationId = correlationId;
            this.RequestId = requestId;
            this.Locale = locale;
        }

        /// <summary>
        /// Gets the request Id. Uniquely identifies the operation.
        /// </summary>
        public Guid RequestId { get; private set; }

        /// <summary>
        /// Gets the correlation Id. This Id is used to group logical operations together.
        /// </summary>
        public Guid CorrelationId { get; private set; }

        /// <summary>
        /// Gets the Locale.
        /// </summary>
        public string Locale { get; private set; }

        /// <summary>
        /// Returns a string representation of the request context.
        /// </summary>
        /// <returns>A string representation of the request context.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Request Id: {0}, Correlation Id: {1}, Locale: {2}", this.RequestId, this.CorrelationId, this.Locale);
        }
    }
}
