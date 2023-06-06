// -----------------------------------------------------------------------
// <copyright file="PartnerResponseParseException.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using Network;
    using RequestContext;

    /// <summary>
    /// This exception is thrown by <see cref="IPartnerServiceProxy{TRequest,TResponse}"/> objects when they fail to parse the response according to the
    /// caller's expectations.
    /// </summary>
    [Serializable]
    public class PartnerResponseParseException : PartnerException
    {
        /// <summary>
        /// The response field name used in serialization.
        /// </summary>
        private const string ResponseFieldName = "Response";

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerResponseParseException"/> class.
        /// </summary>
        public PartnerResponseParseException()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerResponseParseException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public PartnerResponseParseException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerResponseParseException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public PartnerResponseParseException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorCategory = PartnerErrorCategory.ResponseParsing;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerResponseParseException"/> class.
        /// </summary>
        /// <param name="response">The HTTP response payload which could not be parsed.</param>
        /// <param name="context">The partner context.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public PartnerResponseParseException(string response, IRequestContext context, string message = "", Exception innerException = null)
            : this(message, innerException)
        {
            this.Response = response;
            this.Context = context;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerResponseParseException"/> class.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected PartnerResponseParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            this.Response = info.GetString(ResponseFieldName);
        }

        /// <summary>
        /// Gets the HTTP response payload which could not be parsed.
        /// </summary>
        public string Response { get; private set; }

        /// <summary>
        /// Required override to add in the serialized parameters.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue(ResponseFieldName, this.Response);

            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Displays the partner API network exception details.
        /// </summary>
        /// <returns>A string representing the network exception including the base and extended properties.</returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "PartnerApiParsingException: Response: {0}, Base Description: {1}",
                this.Response.ToString(),
                base.ToString());
        }
    }
}
