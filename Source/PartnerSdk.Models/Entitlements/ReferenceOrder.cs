// -----------------------------------------------------------------------
// <copyright file="ReferenceOrder.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Entitlements
{
    /// <summary>
    /// Class that represents an order linked to the entitlement.
    /// </summary>
    public class ReferenceOrder : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets order id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets order line item id.
        /// </summary>
        public string LineItemId { get; set; }

        /// <summary>
        /// Gets or sets the order alternate Id.
        /// </summary>
        public string AlternateId { get; set; }
    }
}
