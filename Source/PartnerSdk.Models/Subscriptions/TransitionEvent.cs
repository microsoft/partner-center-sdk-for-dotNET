// -----------------------------------------------------------------------
// <copyright file="TransitionEvent.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Describes the behavior of an individual subscription transition event.
    /// </summary>
    public sealed class TransitionEvent : ResourceBase
    {
        /// <summary>
        /// Gets or sets the name of the transition event.
        /// Possible values "Conversion", "SeatReassignment".
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the status of transition event.
        /// Possible values - InProgress, Completed, Failed
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the UTC timestamp of the event.
        /// </summary>
        /// <value>
        /// The UTC timestamp of the event.
        /// </value>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the transition errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IEnumerable<TransitionError> Errors { get; set; }
    }
}