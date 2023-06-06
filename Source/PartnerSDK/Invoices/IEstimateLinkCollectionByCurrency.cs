// -----------------------------------------------------------------------
// <copyright file="IEstimateLinkCollectionByCurrency.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Microsoft.Store.PartnerCenter.Models.Invoices;
    using Models;

    /// <summary>
    /// Holds operations that can be performed on estimate link collection that belong to a given currency.
    /// </summary>
    public interface IEstimateLinkCollectionByCurrency :
        IPartnerComponent, IEntityGetOperations<ResourceCollection<EstimateLink>>
    {
        /// <summary>
        /// Retrieves all the products in the given country and catalog view.
        /// </summary>
        /// <returns>The products in the given country and catalog view.</returns>
        new ResourceCollection<EstimateLink> Get();

        /// <summary>
        /// Asynchronously retrieves all the products in the given country and catalog view.
        /// </summary>
        /// <returns>The products in the given country and catalog view.</returns>
        new Task<ResourceCollection<EstimateLink>> GetAsync();
    }
}
