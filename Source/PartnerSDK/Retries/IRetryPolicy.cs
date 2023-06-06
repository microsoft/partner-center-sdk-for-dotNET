// -----------------------------------------------------------------------
// <copyright file="IRetryPolicy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Retries
{
    using System;

    /// <summary>
    /// Defines a retry policy.
    /// </summary>
    internal interface IRetryPolicy
    {
        /// <summary>
        /// Indicates whether a retry should be performed or not.
        /// </summary>
        /// <param name="attempt">The attempt number.</param>
        /// <returns>True to retry, false to not.</returns>
        bool ShouldRetry(int attempt);

        /// <summary>
        /// Indicates the time to hold off before executing the next retry.
        /// </summary>
        /// <param name="attempt">The attempt number.</param>
        /// <returns>The back off time.</returns>
        TimeSpan BackOffTime(int attempt);
    }
}
