// -----------------------------------------------------------------------
// <copyright file="ICategoryOffersCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Offers
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Offers;

    /// <summary>
    /// Holds operations that can be performed on offers that belong to an offer category.
    /// </summary>
    public interface ICategoryOffersCollection :
        IPartnerComponent<Tuple<string, string>>, IEntityCollectionRetrievalOperations<Offer, ResourceCollection<Offer>>
    {
        /// <summary>
        /// Retrieves all the offers in the given offer category.
        /// </summary>
        /// <returns>The offers in the given offer category.</returns>
        new ResourceCollection<Offer> Get();

        /// <summary>
        /// Asynchronously retrieves all the offers in the given offer category.
        /// </summary>
        /// <returns>The offers in the given offer category.</returns>
        new Task<ResourceCollection<Offer>> GetAsync();

        /// <summary>
        /// Retrieves a subset of offers in the given offer category.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offers to return.</param>
        /// <returns>The requested segment of the offers in the given offer category.</returns>
        new ResourceCollection<Offer> Get(int offset, int size);

        /// <summary>
        /// Asynchronously retrieves a subset of offers in the given offer category.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offers to return.</param>
        /// <returns>The requested segment of the offers in the given offer category.</returns>
        new Task<ResourceCollection<Offer>> GetAsync(int offset, int size);
    }
}