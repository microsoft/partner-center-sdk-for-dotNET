// -----------------------------------------------------------------------
// <copyright file="TransitionError.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    /// <summary>
    /// Represents an error for subscription transfer eligibility.
    /// Provides a reason why an transition cannot be performed.
    /// </summary>
    public class TransitionError
    {
        /// <summary>
        /// Gets or sets the error code associated with the issue.
        /// </summary>
        /// <value>
        /// The error code associated with the issue.
        /// </value>
        public TransitionErrorCode Code { get; set; }

        /// <summary>
        /// Gets or sets friendly text describing the error.
        /// </summary>
        /// <value>
        /// Friendly text describing the error.
        /// </value>
        public string Description { get; set; }
    }
}