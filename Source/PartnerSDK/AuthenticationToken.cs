// -----------------------------------------------------------------------
// <copyright file="AuthenticationToken.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    using System;

    /// <summary>
    /// Represents an authentication token for a resource.
    /// </summary>
    public sealed class AuthenticationToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationToken"/> class.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <param name="expiryTime">The token expiry time.</param>
        public AuthenticationToken(string token, DateTimeOffset expiryTime)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("token can't be empty.");
            }

            this.Token = token;
            this.ExpiryTime = expiryTime;
            this.ExpiryBuffer = TimeSpan.FromSeconds(PartnerService.Instance.Configuration.DefaultAuthenticationTokenExpiryBufferInSeconds);
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Gets the token expiry time.
        /// </summary>
        public DateTimeOffset ExpiryTime { get; private set; }

        /// <summary>
        /// Gets or sets the amount of time to deduct from the actual expiry time. This is used to ensure that the token does
        /// not expire while processing is still in progress.
        /// </summary>
        public TimeSpan ExpiryBuffer { get; set; }

        /// <summary>
        /// Indicates whether the token has expired or not.
        /// </summary>
        /// <returns>True if token has expired. False if not.</returns>
        public bool IsExpired()
        {
            return DateTimeOffset.UtcNow > (this.ExpiryTime.UtcDateTime - this.ExpiryBuffer);
        }
    }
}