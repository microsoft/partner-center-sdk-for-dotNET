// -----------------------------------------------------------------------
// <copyright file="IOfferCategoryCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Offers
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Offers;

    /// <summary>
    /// Represents the behavior of offer categories available to partners.
    /// </summary>
    public interface IOfferCategoryCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<OfferCategory, ResourceCollection<OfferCategory>>
    {
        /// <summary>
        /// Retrieves all offer categories available to the partner for the provided country.
        /// </summary>
        /// <returns>All offer categories for the provided country.</returns>
        new ResourceCollection<OfferCategory> Get();

        /// <summary>
        /// Asynchronously retrieves all offer categories available to the partner for the provided country.
        /// </summary>
        /// <returns>All offer categories for the provided country.</returns>
        new Task<ResourceCollection<OfferCategory>> GetAsync();
    }
}
