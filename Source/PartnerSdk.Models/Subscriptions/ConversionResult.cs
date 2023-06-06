// -----------------------------------------------------------------------
// <copyright file="ConversionResult.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    /// <summary>
    /// Represents the result of performing a subscription conversion.
    /// </summary>
    public sealed class ConversionResult
    {
        /// <summary>
        /// Gets or sets the subscription id.
        /// </summary>     
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the original offer id.
        /// </summary>
        public string OfferId { get; set; }

        /// <summary>
        /// Gets or sets the target offer id.
        /// </summary>
        public string TargetOfferId { get; set; }

        /// <summary>
        /// Gets or sets the error encountered while attempting to perform the conversion, if applicable.
        /// </summary>
        public ConversionError Error { get; set; }
    }
}
