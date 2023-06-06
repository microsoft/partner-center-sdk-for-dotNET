// -----------------------------------------------------------------------
// <copyright file="OrderLineItem.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// An order line item associates order information to a specific offer of a product.
    /// </summary>
    public sealed class OrderLineItem : ResourceBaseWithLinks<OrderLineItemLinks>
    {
        #region Order information

        /// <summary>
        /// Gets or sets the line item number.
        /// </summary>
        public int LineItemNumber { get; set; }

        #endregion

        #region Offer information

        /// <summary>
        /// Gets or sets the offer identifier.
        /// </summary>
        public string OfferId { get; set; }

        /// <summary>
        /// Gets or sets the resulting subscription identifier.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the parent subscription identifier.
        /// This parameter should only be set for add-on offer purchase. This applies to Order updates only.
        /// </summary>
        public string ParentSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the term duration.
        /// </summary>
        public string TermDuration { get; set; }

        /// <summary>
        /// Gets or sets the RenewsTo.
        /// </summary>
        /// <value>
        /// The RenewsTo.
        /// </value>
        public RenewsTo RenewsTo { get; set; }

        /// <summary>
        /// Gets or sets the transaction type.
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the promotion id.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PromotionId { get; set; }

        /// <summary>
        /// Gets or sets the custom term end date.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CustomTermEndDate { get; set; }

        #endregion

        #region Offer purchase information

        /// <summary>
        /// Gets or sets the friendly name for the result contract (subscription).
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the product quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the pricing details.
        /// </summary>
        /// <value>ListPrice, DiscountedPrice, ProratedPrice, Price, ExtendedPrice details.</value>
        public Pricing Pricing { get; set; }

        /// <summary>
        /// Gets or sets the partner identifier on record.
        /// </summary>
        public string PartnerIdOnRecord { get; set; }

        /// <summary>
        /// Gets or sets the additional partner identifiers on record.
        /// </summary>
        /// <value>
        /// The partner identifier on record.
        /// </value>
        public IEnumerable<string> AdditionalPartnerIdsOnRecord { get; set; }

        /// <summary>
        /// Gets or sets the provisioning context for the offer.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> ProvisioningContext { get; set; }

        /// <summary>
        /// Gets or sets the acceptance of an attestation required by an offer prior to checking out an order.
        /// </summary>
        /// <value>The acceptance of the attestation.</value>
        public bool? AttestationAccepted { get; set; }

        #endregion
    }
}