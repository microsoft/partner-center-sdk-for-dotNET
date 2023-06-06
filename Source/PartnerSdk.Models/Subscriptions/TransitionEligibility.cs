// -----------------------------------------------------------------------
// <copyright file="TransitionEligibility.cs" company="Microsoft Corporation">
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
    public class TransitionEligibility
    {
        /// <summary>
        /// Gets or sets the catalog item id.
        /// </summary>
        /// <value>
        /// The catalog item id.
        /// </value>
        public string CatalogItemId { get; set; }

        /// <summary>
        /// Gets or sets the SKU title.
        /// </summary>
        /// <value>
        /// The SKU title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the SKU description.
        /// </summary>
        /// <value>
        /// The SKU description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the new offer to be purchased.
        /// </summary>
        /// <value>
        /// The quantity of the new offer to be purchased.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the eligibilities.
        /// </summary>
        /// <value>
        /// The eligibilities.
        /// </value>
        public IEnumerable<TransitionEligibilityDetail> Eligibilities { get; set; }
    }
}
