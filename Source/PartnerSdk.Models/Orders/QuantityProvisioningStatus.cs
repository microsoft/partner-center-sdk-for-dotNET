// -----------------------------------------------------------------------
// <copyright file="QuantityProvisioningStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Orders
{
    /// <summary>
    /// Quantity Provisioning Status.
    /// </summary>
    public class QuantityProvisioningStatus : ResourceBase
    {
        /// <summary>
        /// Gets or sets Quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets provisioning status.
        /// </summary>
        public string Status { get; set; }
    }
}