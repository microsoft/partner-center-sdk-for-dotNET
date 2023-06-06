// -----------------------------------------------------------------------
// <copyright file="OrderLineItemProvisioningStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    using System.Collections.Generic;

    /// <summary>
    /// Order line item provisioning status
    /// </summary>
    public class OrderLineItemProvisioningStatus : ResourceBase
    {
        /// <summary>
        /// Gets or sets the line item number.
        /// </summary>
        public int LineItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the aggregated state of an order line item.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets Quantity Provisioning Information
        /// </summary>
        public IEnumerable<QuantityProvisioningStatus> QuantityProvisioningInformation { get; set; }
    }
}