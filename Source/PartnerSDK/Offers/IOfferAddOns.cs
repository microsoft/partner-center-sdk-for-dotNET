// <copyright file="IOfferAddOns.cs" company="Microsoft Corporation">
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
    /// Defines the behavior of an offer's add-ons.
    /// </summary>
    public interface IOfferAddOns : IPartnerComponent<Tuple<string, string>>, IEntityCollectionRetrievalOperations<Offer, ResourceCollection<Offer>>
    {
        /// <summary>
        /// Retrieves all the add-ons for the provided offer.
        /// </summary>
        /// <returns>The offer add-ons.</returns>
        new ResourceCollection<Offer> Get();

        /// <summary>
        /// Asynchronously retrieves all add-ons for the provided offer.
        /// </summary>
        /// <returns>The offer add-ons.</returns>
        new Task<ResourceCollection<Offer>> GetAsync();

        /// <summary>
        /// Retrieves a subset of add-ons for the provided offer.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offer add-ons to return.</param>
        /// <returns>The requested segment of the offer add-ons.</returns>
        new ResourceCollection<Offer> Get(int offset, int size);

        /// <summary>
        /// Asynchronously retrieves a subset of add-ons for the provided offer.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offer add-ons to return.</param>
        /// <returns>The requested segment of the offer add-ons.</returns>
        new Task<ResourceCollection<Offer>> GetAsync(int offset, int size);
    }
}
