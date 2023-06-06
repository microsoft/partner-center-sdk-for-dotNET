// -----------------------------------------------------------------------
// <copyright file="LinearBackOffRetryPolicy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Retries
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A linear back off policy waits for a constant amount of time between retries.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "We may need to use this.")]
    internal class LinearBackOffRetryPolicy : IRetryPolicy
    {
        /// <summary>
        /// The maximum number of retry attempts.
        /// </summary>
        private int maxRetries;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearBackOffRetryPolicy"/> class.
        /// </summary>
        /// <param name="backOff">The back off time between retries.</param>
        /// <param name="maxRetries">The maximum number of retries.</param>
        public LinearBackOffRetryPolicy(TimeSpan backOff, int maxRetries)
        {
            this.BackOff = backOff;
            this.MaxRetries = maxRetries;
        }

        /// <summary>
        /// Gets or sets the back off time between retries.
        /// </summary>
        public TimeSpan BackOff { get; set; }

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
            return this.BackOff;
        }
    }
}
