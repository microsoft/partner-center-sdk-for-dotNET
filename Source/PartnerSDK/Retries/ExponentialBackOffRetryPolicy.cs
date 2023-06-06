// -----------------------------------------------------------------------
// <copyright file="ExponentialBackOffRetryPolicy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Retries
{
    using System;

    /// <summary>
    /// This class implements binary exponential back off retries. Each time an operation fails, the amount of time
    /// to wait before executing the next retry increases.
    /// </summary>
    internal class ExponentialBackOffRetryPolicy : IRetryPolicy
    {
        /// <summary>
        /// The maximum number of retry attempts.
        /// </summary>
        private int maxRetries;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentialBackOffRetryPolicy"/> class.
        /// </summary>
        /// <param name="maxRetries">The maximum number of retries.</param>
        public ExponentialBackOffRetryPolicy(int maxRetries)
        {
            this.MaxRetries = maxRetries;
        }

        /// <summary>
        /// Gets or sets the maximum number of retries to perform.
        /// </summary>
        public int MaxRetries
        {
            get
            {
                return this.maxRetries;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("MaxRetries must be greater than zero");
                }

                this.maxRetries = value;
            }
        }

        /// <summary>
        /// Indicates whether a retry should be performed or not.
        /// </summary>
        /// <param name="attempt">The attempt number.</param>
        /// <returns>True to retry, false to not.</returns>
        public bool ShouldRetry(int attempt)
        {
            return attempt <= this.MaxRetries;
        }

        /// <summary>
        /// Indicates the time to hold off before executing the next retry.
        /// </summary>
        /// <param name="attempt">The attempt number.</param>
        /// <returns>The back off time.</returns>
        public TimeSpan BackOffTime(int attempt)
        {
            // the first retry will wait for: 0.5 seconds, second retry will wait for 1.5 seconds, third retry will wait for 3.5 seconds and so forth
            double exponentialBackOffTime = (Math.Pow(2, attempt) - 1) / 2;

            return TimeSpan.FromSeconds(exponentialBackOffTime);
        }
    }
}
