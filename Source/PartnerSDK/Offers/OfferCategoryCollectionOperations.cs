// -----------------------------------------------------------------------
// <copyright file="OfferCategoryCollectionOperations.cs" company="Microsoft Corporation">
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
    /// Offers categories implementation.
    /// </summary>
    internal class OfferCategoryCollectionOperations : BasePartnerComponent, IOfferCategoryCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferCategoryCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="country">The country on which to base the offer categories.</param>
        public OfferCategoryCollectionOperations(IPartner rootPartnerOperations, string country)
            : base(rootPartnerOperations, country)
        {
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <summary>
        /// Retrieves all offer categories available to the partner for the provided country.
        /// </summary>
        /// <returns>All offer categories for the provided country.</returns>
        public ResourceCollection<OfferCategory> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<OfferCategory>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves all offer categories available to the partner for the provided country.
        /// </summary>
        /// <returns>All offer categories for the provided country.</returns>
        public async Task<ResourceCollection<OfferCategory>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<OfferCategory, ResourceCollection<OfferCategory>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetOfferCategories.Path);

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOfferCategories.Parameters.Country, this.Context));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
