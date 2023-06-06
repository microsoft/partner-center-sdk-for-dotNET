// -----------------------------------------------------------------------
// <copyright file="BasePartnerCredentials.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Extensions
{
    using System;
    using System.Threading.Tasks;
    using RequestContext;

    /// <summary>
    /// A base implementation for partner credentials.
    /// </summary>
    internal abstract class BasePartnerCredentials : IPartnerCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePartnerCredentials"/> class.
        /// </summary>
        /// <param name="clientId">The azure active directory client Id.</param>
        public BasePartnerCredentials(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentException("clientId has to be set");
            }

            this.ClientId = clientId;
        }

        /// <summary>
        /// Gets the partner service token.
        /// </summary>
        public string PartnerServiceToken
        {
            get
            {
                return this.AADToken.Token;
            }
        }

        /// <summary>
        /// Gets the expiry time in UTC for the token.
        /// </summary>
        public DateTimeOffset ExpiresAt
        {
            get
            {
                return this.AADToken.ExpiryTime;
            }
        }

        /// <summary>
        /// Gets the azure active directory client Id.
        /// </summary>
        protected string ClientId { get; private set; }

        /// <summary>
        /// Gets or sets the azure active directory token.
        /// </summary>
        protected AuthenticationToken AADToken { get; set; }

        /// <summary>
        /// Indicates whether the partner credentials have expired or not.
        /// </summary>
        /// <returns>True if credentials have expired. False if not.</returns>
        public bool IsExpired()
        {
            return this.AADToken.IsExpired();
        }

        /// <summary>
        /// Authenticates with the partner service.
        /// </summary>
        /// <param name="requestContext">An optional request context.</param>
        /// <returns>A task that is complete when authentication is finished.</returns>
        public async virtual Task AuthenticateAsync(IRequestContext requestContext = null)
        {
            // Do nothing, leave it to sub classes
            await Task.FromResult(0);
        }
    }
}
