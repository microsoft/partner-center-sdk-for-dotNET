// -----------------------------------------------------------------------
// <copyright file="IOfferCollection.cs" company="Microsoft Corporation">
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
    /// Holds operations that can be performed on offers.
    /// </summary>
    public interface IOfferCollection : IPartnerComponent, IEntityCollectionRetrievalOperations<Offer, ResourceCollection<Offer>>, IEntitySelector<IOffer>
    {
        /// <summary>
        /// Gets the operations tied with a specific offer.
        /// </summary>
        /// <param name="offerId">The offer id.</param>
        /// <returns>The offer operations.</returns>
        new IOffer this[string offerId] { get; }

        /// <summary>
        /// Retrieves the operations tied with a specific offer.
        /// </summary>
        /// <param name="offerId">The offer id.</param>
        /// <returns>The offer operations.</returns>
        new IOffer ById(string offerId);

        /// <summary>
        /// Retrieves all the offers for the provided country.
        /// </summary>
        /// <returns>The offers for the provided country.</returns>
        new ResourceCollection<Offer> Get();

        /// <summary>
        /// Asynchronously retrieves all the offers for the provided country.
        /// </summary>
        /// <returns>The offers for the provided country.</returns>
        new Task<ResourceCollection<Offer>> GetAsync();

        /// <summary>
        /// Retrieves a subset of offers for the provided country.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offers to return.</param>
        /// <returns>The requested segment of the offers for the given country.</returns>
        new ResourceCollection<Offer> Get(int offset, int size);

        /// <summary>
        /// Asynchronously retrieves a subset of offers for the provided country.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offers to return.</param>
        /// <returns>The requested segment of the offers for the given country.</returns>
        new Task<ResourceCollection<Offer>> GetAsync(int offset, int size);

        /// <summary>
        /// Retrieves the operations that can be applied on offers that belong to an offer category.
        /// </summary>
        /// <param name="categoryId">The offer category Id.</param>
        /// <returns>The category offers operations.</returns>
        ICategoryOffersCollection ByCategory(string categoryId);
    }
}