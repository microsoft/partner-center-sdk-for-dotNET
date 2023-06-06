// -----------------------------------------------------------------------
// <copyright file="ICustomerSkuByReservationScope.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Products;

    /// <summary>
    /// Holds operations that can be performed on a customer's single sku filtered by reservation scope
    /// </summary>
    public interface ICustomerSkuByReservationScope : IPartnerComponent<Tuple<string, string, string, string>>, IEntityGetOperations<Sku>
    {
        /// <summary>
        /// Retrieves the product information.
        /// </summary>
        /// <returns>The product information.</returns>
        new Sku Get();

        /// <summary>
        /// Asynchronously retrieves the product information.
        /// </summary>
        /// <returns>The product information.</returns>
        new Task<Sku> GetAsync();
    }
}
