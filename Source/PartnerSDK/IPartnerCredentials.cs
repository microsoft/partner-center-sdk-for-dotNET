// -----------------------------------------------------------------------
// <copyright file="IPartnerCredentials.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    using System;

    /// <summary>
    /// The credentials needed to access the partner API service.
    /// </summary>
    public interface IPartnerCredentials
    {
        /// <summary>
        /// Gets the token needed to authenticate with the partner API service.
        /// </summary>
        string PartnerServiceToken { get; }

        /// <summary>
        /// Gets the expiry time in UTC for the token.
        /// </summary>
        DateTimeOffset ExpiresAt { get; }

        /// <summary>
        /// Indicates whether the partner credentials have expired or not.
        /// </summary>
        /// <returns>True if credentials have expired. False if not.</returns>
        bool IsExpired();
    }
}
