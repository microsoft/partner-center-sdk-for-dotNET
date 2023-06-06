// -----------------------------------------------------------------------
// <copyright file="CategoryOffersCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Offers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.Offers;
    using Network;
    using Utilities;

    /// <summary>
    /// Category offers operations implementation class.
    /// </summary>
    internal class CategoryOffersCollectionOperations : BasePartnerComponent<Tuple<string, string>>, ICategoryOffersCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryOffersCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="categoryId">The category ID which contains the offers.</param>
        /// <param name="country">The country on which to base the offers.</param>
        public CategoryOffersCollectionOperations(IPartner rootPartnerOperations, string categoryId, string country) :
            base(rootPartnerOperations, new Tuple<string, string>(categoryId, country))
        {
            ParameterValidator.Required(categoryId, "categoryId must be set");
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <summary>
        /// Retrieves all the offers in the given offer category.
        /// </summary>
        /// <returns>The offers in the given offer category.</returns>
        public ResourceCollection<Offer> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Offer>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves all the offers in the given offer category.
        /// </summary>
        /// <returns>The offers in the given offer category.</returns>
        public async Task<ResourceCollection<Offer>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Offer, ResourceCollection<Offer>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetOffers.Path);

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffers.Parameters.OfferCategoryId, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffers.Parameters.Country, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Retrieves a subset of offers in the given offer category.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offers to return.</param>
        /// <returns>The requested segment of the offers in the given offer category.</returns>
        public ResourceCollection<Offer> Get(int offset, int size)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Offer>>(() => this.GetAsync(offset, size));
        }

        /// <summary>
        /// Asynchronously retrieves a subset of offers in the given offer category.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offers to return.</param>
        /// <returns>The requested segment of the offers in the given offer category.</returns>
        public async Task<ResourceCollection<Offer>> GetAsync(int offset, int size)
        {
            var partnerServiceProxy = new PartnerServiceProxy<Offer, ResourceCollection<Offer>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetOffers.Path);

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffers.Parameters.OfferCategoryId, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffers.Parameters.Country, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffers.Parameters.Offset, offset.ToString()));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffers.Parameters.Size, size.ToString()));

            return await partnerServiceProxy.GetAsync();
        }
    }
}