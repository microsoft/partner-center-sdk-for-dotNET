// -----------------------------------------------------------------------
// <copyright file="IServiceCostLineItemsCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.ServiceCosts
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.ServiceCosts;

    /// <summary>
    /// Represents the behavior of the customer service cost line items as a whole.
    /// </summary>
    public interface IServiceCostLineItemsCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<ServiceCostLineItem, ResourceCollection<ServiceCostLineItem>>
    {
        /// <summary>
        /// Retrieves a customer's service cost line items.
        /// </summary>
        /// <returns>The service cost line items.</returns>
        new ResourceCollection<ServiceCostLineItem> Get();

        /// <summary>
        /// Asynchronously retrieves a customer's service cost line items.
        /// </summary>
        /// <returns>The service cost line items.</returns>
        new Task<ResourceCollection<ServiceCostLineItem>> GetAsync();
    }
}
