// -----------------------------------------------------------------------
// <copyright file="AggregatePartnerOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    using System;
    using RequestContext;
    
    /// <summary>
    /// Aggregate partner implementation.
    /// </summary>
    internal class AggregatePartnerOperations : PartnerOperations, IAggregatePartner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregatePartnerOperations"/> class.
        /// </summary>
        /// <param name="credentials">The partner credentials.</param>
        public AggregatePartnerOperations(IPartnerCredentials credentials)
            : base(credentials, RequestContextFactory.Instance.Create())
        {
        }

        /// <summary>
        /// Returns a partner operations object which uses the provided context when executing operations.
        /// </summary>
        /// <param name="context">An operation context object.</param>
        /// <returns>A partner operations object which uses the provided operation context.</returns>
        public IPartner With(IRequestContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return new PartnerOperations(this.Credentials, context);
        }
    }
}
