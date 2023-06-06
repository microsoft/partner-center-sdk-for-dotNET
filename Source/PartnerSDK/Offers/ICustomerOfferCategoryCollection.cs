// -----------------------------------------------------------------------
// <copyright file="ICustomerOfferCategoryCollection.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on OfferCategories available for the Customer.
    /// </summary>
    public interface ICustomerOfferCategoryCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<OfferCategory, ResourceCollection<OfferCategory>>
    {
        /// <summary>
        /// Retrieves all the offer categories for the provided Customer.
        /// </summary>
        /// <returns>The offers for the provided customer.</returns>
        new ResourceCollection<OfferCategory> Get();

        /// <summary>
        /// Asynchronously retrieves all the offer categories for the provided Customer.
        /// </summary>
        /// <returns>The offers for the provided customer.</returns>
        new Task<ResourceCollection<OfferCategory>> GetAsync();
    }
}
