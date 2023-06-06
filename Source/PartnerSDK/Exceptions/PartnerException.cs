// -----------------------------------------------------------------------
// <copyright file="PartnerException.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Text;
    using Models;
    using RequestContext;

    /// <summary>
    /// The standard exception thrown by the partner SDK. This pertains to errors accessing the partner service. Other standard exceptions
    /// such as null argument exceptions will also be thrown in case of malformed input.
    /// </summary>
    [Serializable]
    public class PartnerException : Exception
    {
        /// <summary>
        /// The error category field name used in serialization.
        /// </summary>
        private const string ErrorCategoryFieldName = "ErrorCategory";

        /// <summary>
        /// The service error payload field name used in serialization.
        /// </summary>
        private const string ServiceErrorPayloadFieldName = "ServiceErrorCode";

        /// <summary>
        /// The partner context field name used in serialization.
        /// </summary>
        private const string PartnerContextFieldName = "PartnerContext";

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerException"/> class.
        /// </summary>
        public PartnerException()
            : base()
        {
            this.ErrorCategory = PartnerErrorCategory.NotSpecified;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public PartnerException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public PartnerException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorCategory = PartnerErrorCategory.NotSpecified;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="context">The partner context.</param>
        /// <param name="errorCategory">An optional error category.</param>
        /// <param name="innerException">An optional inner exception.</param>
        /// <param name="retryAfter">An optional retry after recommendation.</param>
        public PartnerException(string message, IRequestContext context, PartnerErrorCategory errorCategory = PartnerErrorCategory.NotSpecified, Exception innerException = null, TimeSpan? retryAfter = null)
            : base(message, innerException)
        {
            this.ErrorCategory = errorCategory;
            this.Context = context;
            this.RetryAfter = retryAfter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerException"/> class.
        /// </summary>
        /// <param name="apiFault">The API fault object returned by the partner service.</param>
        /// <param name="context">The partner context.</param>
        /// <param name="errorCategory">An optional error category.</param>
        /// <param name="innerException">An optional inner exception.</param>
        public PartnerException(ApiFault apiFault, IRequestContext context, PartnerErrorCategory errorCategory = PartnerErrorCategory.NotSpecified, Exception innerException = null)
            : this(apiFault != null ? apiFault.ErrorMessage : string.Empty, context, errorCategory, innerException)
        {
            this.ServiceErrorPayload = apiFault;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerException"/> class.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected PartnerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            this.ErrorCategory = (PartnerErrorCategory)info.GetInt32(ErrorCategoryFieldName);
            this.ServiceErrorPayload = info.GetValue(ServiceErrorPayloadFieldName, typeof(ApiFault)) as ApiFault;
            this.Context = info.GetValue(PartnerContextFieldName, typeof(IRequestContext)) as IRequestContext;
        }

        /// <summary>
        /// Gets the error classification that resulted in this exception.
        /// </summary>
        public PartnerErrorCategory ErrorCategory { get; protected set; }

        /// <summary>
        /// Gets the retry after recommendation.
        /// </summary>
        public TimeSpan? RetryAfter { get; protected set; }

        /// <summary>
        /// Gets the service error payload.
        /// </summary>
        public ApiFault ServiceErrorPayload { get; protected set; }

        /// <summary>
        /// Gets the partner context associated with the exception.
        /// </summary>
        public IRequestContext Context { get; protected set; }

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

            info.AddValue(ErrorCategoryFieldName, this.ErrorCategory);
            info.AddValue(ServiceErrorPayloadFieldName, this.ServiceErrorPayload);
            info.AddValue(PartnerContextFieldName, this.Context);

            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Displays the partner exception details.
        /// </summary>
        /// <returns>A string representing the partner exception including the base and extended properties.</returns>
        public override string ToString()
        {
            StringBuilder exceptionDescription = new StringBuilder();
            exceptionDescription.AppendLine("Partner Exception:");
            exceptionDescription.AppendFormat("Error Category: {0}", this.ErrorCategory.ToString()).AppendLine();
            exceptionDescription.AppendFormat("Service Error Payload: {0}", this.ServiceErrorPayload != null ? this.ServiceErrorPayload.ToString() : "null").AppendLine();
            exceptionDescription.AppendFormat("Context: {0}", this.Context).AppendLine();
            exceptionDescription.AppendFormat("Base Description: {0}", base.ToString()).AppendLine();

            return exceptionDescription.ToString();
        }
    }
}
