// -----------------------------------------------------------------------
// <copyright file="OfferCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Offers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.Offers;
    using Network;
    using Utilities;

    /// <summary>
    /// Offer Collection operations implementation.
    /// </summary>
    internal class OfferCollectionOperations : BasePartnerComponent, IOfferCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="country">The country on which to base the offers.</param>
        public OfferCollectionOperations(IPartner rootPartnerOperations, string country) :
            base(rootPartnerOperations, country)
        {
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <summary>
        /// Gets the operations tied with a specific offer.
        /// </summary>
        /// <param name="offerId">The offer id.</param>
        /// <returns>The offer operations.</returns>
        public IOffer this[string offerId]
        {
            get
            {
                return this.ById(offerId);
            }
        }

        /// <summary>
        /// Retrieves the operations tied with a specific offer.
        /// </summary>
        /// <param name="offerId">The offer id.</param>
        /// <returns>The offer operations.</returns>
        public IOffer ById(string offerId)
        {
            return new OfferOperations(this.Partner, offerId, this.Context);
        }

        /// <summary>
        /// Retrieves all the offers for the provided country.
        /// </summary>
        /// <returns>All offers for the provided country.</returns>
        public ResourceCollection<Offer> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Offer>>(() => this.GetAsync());
        }
         
        /// <summary>
        /// Asynchronously retrieves all the offers for the provided country.
        /// </summary>
        /// <returns>All offers for the provided country.</returns>
        public async Task<ResourceCollection<Offer>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Offer, ResourceCollection<Offer>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetOffers.Path);

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffers.Parameters.Country, this.Context));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a subset of offers for the provided country.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offers to return.</param>
        /// <returns>The requested segment of the offers  for the provided country.</returns>
        public ResourceCollection<Offer> Get(int offset, int size)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Offer>>(() => this.GetAsync(offset, size));
        }

        /// <summary>
        /// Retrieves a subset of offers  for the provided country.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offers to return.</param>
        /// <returns>The requested segment of the offers for the provided country.</returns>
        public async Task<ResourceCollection<Offer>> GetAsync(int offset, int size)
        {
            ParameterValidator.IsInclusive<int>(0, int.MaxValue, offset, "offset has to be non-negative.");
            ParameterValidator.IsInclusive<int>(1, int.MaxValue, size, "size has to be positive.");

            var partnerServiceProxy = new PartnerServiceProxy<Offer, ResourceCollection<Offer>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetOffers.Path);

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffers.Parameters.Country, this.Context));
            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffers.Parameters.Offset, offset.ToString()));
            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffers.Parameters.Size, size.ToString()));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Retrieves the operations that can be applied on offers the belong to an offer category.
        /// </summary>
        /// <param name="categoryId">The offer category Id.</param>
        /// <returns>The category offers operations.</returns>
        public ICategoryOffersCollection ByCategory(string categoryId)
        {
            return new CategoryOffersCollectionOperations(this.Partner, categoryId, this.Context);
        }
    }
}
