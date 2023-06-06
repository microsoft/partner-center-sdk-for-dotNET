// -----------------------------------------------------------------------
// <copyright file="ISku.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Microsoft.Store.PartnerCenter.Customers.Products;
    using Models.Products;

    /// <summary>
    /// Holds operations that can be performed on a single sku.
    /// </summary>
    public interface ISku : IPartnerComponent<Tuple<string, string, string>>, IEntityGetOperations<Sku>
    {
        /// <summary>
        /// Retrieves the operations for the current sku's availabilities.
        /// </summary>
        IAvailabilityCollection Availabilities { get; }

        /// <summary>
        /// Retrieves the sku information.
        /// </summary>
        /// <returns>The sku information.</returns>
        new Sku Get();

        /// <summary>
        /// Asynchronously retrieves the sku information.
        /// </summary>
        /// <returns>The sku information.</returns>
        new Task<Sku> GetAsync();

        /// <summary>
        /// Retrieves the operations that can be applied on sku id's filtered by a specific reservation scope.
        /// </summary>
        /// <param name="reservationScope">The reservation scope filter.</param>
        /// <returns>The individual sku operations sorted by reservation scope.</returns>
        ISkuByReservationScope ByReservationScope(string reservationScope);

        /// <summary>
        /// Retrieves the operations that can be applied on a customer's sku id's filtered by a specific reservation scope.
        /// </summary>
        /// <param name="reservationScope">The reservation scope filter.</param>
        /// <returns>The individual sku operations sorted by reservation scope.</returns>
        ICustomerSkuByReservationScope ByCustomerReservationScope(string reservationScope);
    }
}
