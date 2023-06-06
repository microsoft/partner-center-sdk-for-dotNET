// -----------------------------------------------------------------------
// <copyright file="IOrderProvisioningStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Models.Orders;

    /// <summary>
    /// Holds operations that apply Order provisioning status.
    /// </summary>
    public interface IOrderProvisioningStatus : IPartnerComponent<Tuple<string, string>>
    {
        /// <summary>
        /// Retrieves the order provisioning status.
        /// </summary>
        /// <returns>The order provisioning status.</returns>
        ResourceCollection<OrderLineItemProvisioningStatus> Get();

        /// <summary>
        /// Asynchronously retrieves the order provisioning status.
        /// </summary>
        /// <returns>The order provisioning status.</returns>
        Task<ResourceCollection<OrderLineItemProvisioningStatus>> GetAsync();
    }
}
