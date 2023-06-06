// -----------------------------------------------------------------------
// <copyright file="Transition.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System.Collections.Generic;

    /// <summary>
    /// Describes the behavior of an individual subscription transition resource.
    /// </summary>
    public sealed class Transition : ResourceBase
    {
        /// <summary>
        /// Gets or sets the operation id.
        /// </summary>
        /// <value>
        /// The operation id.
        /// </value>
        public string OperationId { get; set; }

        /// <summary>
        /// Gets or sets the From catalog item id.
        /// *Optional. Mainly used on response.
        /// </summary>
        /// <value>
        /// The catalog item id.
        /// </value>
        public string FromCatalogItemId { get; set; }

        /// <summary>
        /// Gets or sets the from subscription id.
        /// *Optional. Mainly used on response.
        /// </summary>
        /// <value>
        /// The subscription id.
        /// </value>
        public string FromSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the To catalog item id.
        /// </summary>
        /// <value>
        /// The catalog item id.
        /// </value>
        public string ToCatalogItemId { get; set; }

        /// <summary>
        /// Gets or sets the To subscription id.
        /// Legacy Upgrade will populate this in response.
        /// A Modern partial transition will also populate this in response.
        /// Modern Partial/Full transitions use this in request to define existing subscription to transition into.
        /// </summary>
        /// <value>
        /// The subscription id.
        /// </value>
        public string ToSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the quantity being transitioned to the target catalog item.
        /// </summary>
        /// <value>
        /// The quantity being transitioned to the target catalog item.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the term duration.
        /// </summary>
        /// <value>
        /// Examples: P1M, P1Y, P3Y.
        /// </value>
        public string TermDuration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the frequency with which the partner is billed for the target subscription.
        /// </summary>
        /// <value>The billing cycle. </value>
        public string BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets the transition type.
        /// Possible values - transition_only, transition_with_license_transfer.
        /// </summary>
        /// <value>
        /// The transition type.
        /// </value>
        public string TransitionType { get; set; }

        /// <summary>
        /// Gets or sets the promotionId.
        /// </summary>
        /// <value>
        /// The promotionId.
        /// </value>
        public string PromotionId { get; set; }

        /// <summary>
        /// Gets or sets the events of the transtion.
        /// </summary>
        /// <value>
        /// The transition events.
        /// </value>
        public List<TransitionEvent> Events { get; set; }
    }
}