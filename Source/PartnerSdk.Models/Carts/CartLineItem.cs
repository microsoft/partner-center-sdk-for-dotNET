// -----------------------------------------------------------------------
// <copyright file="CartLineItem.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Carts
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.Models.Carts.Enums;
    using Microsoft.Store.PartnerCenter.Models.Products;

    /// <summary>
    /// Represents a line item on a cart.
    /// </summary>
    public class CartLineItem
    {
        /// <summary>
        /// Gets or sets a unique identifier of a cart line item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the catalog item identifier.
        /// </summary>
        public string CatalogItemId { get; set; }

        /// <summary>
        /// Gets or sets the friendly name for the result contract (subscription)
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the product quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the type of billing cycle for the selected catalog item.
        /// </summary>
        public BillingCycleType BillingCycle { get; set; }

        /// <summary>
        /// Gets or sets the term duration if applicable.
        /// </summary>
        /// <value>
        /// The term duration if applicable.
        /// </value>
        public string TermDuration { get; set; }

        /// <summary>
        /// Gets or sets the promotionId.
        /// </summary>
        public string PromotionId { get; set; }

        /// <summary>
        /// Gets or sets a collection of participants on this purchase.
        /// </summary>
        public IEnumerable<KeyValuePair<ParticipantType, string>> Participants { get; set; }

        /// <summary>
        /// Gets or sets a context that will be used for provisioning of the catalog item.
        /// </summary>
        public Dictionary<string, string> ProvisioningContext { get; set; }

        /// <summary>
        /// Gets or sets the order group which indicates which items can be place in a single order.
        /// </summary>
        public string OrderGroup { get; set; }

        /// <summary>
        /// Gets or sets the which purchase system to place the order.
        /// </summary>
        public string PurchaseSystem { get; set; }

        /// <summary>
        /// Gets or sets a list of items that depend on this one, so they have to be purchased subsequently.
        /// </summary>
        public IEnumerable<CartLineItem> AddonItems { get; set; }

        /// <summary>
        /// Gets or sets the RenewsTo.
        /// </summary>
        /// <value>
        /// The RenewsTo.
        /// </value>
        public RenewsTo RenewsTo { get; set; }

        /// <summary>
        /// Gets or sets an error associated to this cart line item.
        /// </summary>
        public CartError Error { get; set; }

        /// <summary>
        /// Gets or sets the custom term end date for the subscription.
        /// If provided, the first term will be prorated to end on this date.
        /// </summary>
        /// <value>
        /// The custom term end date.
        /// </value>
        public DateTime? CustomTermEndDate { get; set; }

        /// <summary>
        /// Gets or sets the acceptance of an attestation required by an offer prior to checking out.
        /// </summary>
        /// <value>The acceptance of the attestation.</value>
        public bool? AttestationAccepted { get; set; }
    }
}
