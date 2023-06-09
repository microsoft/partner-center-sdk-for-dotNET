﻿// -----------------------------------------------------------------------
// <copyright file="Sku.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents a sku.
    /// </summary>
    public class Sku : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the product id
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the minimum order quantity.
        /// </summary>
        public int MinimumQuantity { get; set; }

        /// <summary>
        /// Gets or sets the maximum order quantity.
        /// </summary>
        public int MaximumQuantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a trial sku or not.
        /// </summary>
        public bool IsTrial { get; set; }

        /// <summary>
        /// Gets or sets the uri for term of use.
        /// </summary>
        public string TermsOfUseUri { get; set; }

        /// <summary>
        /// Gets or sets the billing cycles supported for the offer.
        /// </summary>
        /// <value>
        /// A collection of billing cycles supported for this offer.
        /// </value>
        public IEnumerable<BillingCycleType> SupportedBillingCycles { get; set; }

        /// <summary>
        /// Gets or sets the purchase prerequisites.
        /// </summary>
        public IEnumerable<string> PurchasePrerequisites { get; set; }

        /// <summary>
        /// Gets or sets the variables needed for inventory check.
        /// </summary>
        public IEnumerable<string> InventoryVariables { get; set; }

        /// <summary>
        /// Gets or sets the provisioning variables.
        /// </summary>
        public IEnumerable<string> ProvisioningVariables { get; set; }

        /// <summary>
        /// Gets or sets a list of constrain that can be apply to transact or manage sku.
        /// </summary>
        public IEnumerable<Constraint> Constraints { get; set; }

        /// <summary>
        /// Gets or sets the dynamic attributes.
        /// </summary>
        public Dictionary<string, object> DynamicAttributes { get; set; }

        /// <summary>
        /// Gets or sets the attestation properties.
        /// </summary>
        public SkuAttestationProperties AttestationProperties { get; set; }

        /// <summary>
        /// Gets or sets the consumption type
        /// </summary>
        public string ConsumptionType { get; set; }

        /// <summary>
        /// Gets or sets the properties for this specialized offer if applicable
        /// </summary>
        public SpecializedOfferProperties SpecializedOfferProperties { get; set; }
    }
}
