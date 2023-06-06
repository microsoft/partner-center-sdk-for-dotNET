// -----------------------------------------------------------------------
// <copyright file="TransitionEligibilityDetail.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System.Collections.Generic;

    /// <summary>
    /// Describes the behavior of an individual transition eligibility details resource.
    /// </summary>
    public class TransitionEligibilityDetail
    {
        /// <summary>
        /// Gets or sets a value indicating whether the transition is eligible.
        /// </summary>
        /// <value>
        /// The transition eligibility.
        /// </value>
        public bool IsEligible { get; set; }

        /// <summary>
        /// Gets or sets the transition type.
        /// </summary>
        /// <value>
        /// The transition type
        /// Possible values :
        ///     none
        ///     transition_only
        ///     transition_with_license_transfer.
        /// </value>
        public string TransitionType { get; set; }

        /// <summary>
        /// Gets or sets the reasons the transition cannot be performed, if applicable.
        /// </summary>
        /// <value>
        /// The reasons the transition cannot be performed, if applicable.
        /// </value>
        public IEnumerable<TransitionError> Errors { get; set; }
    }
}