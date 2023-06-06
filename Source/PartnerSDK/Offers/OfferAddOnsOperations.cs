// <copyright file="OfferAddOnsOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Offers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Offers;
    using Network;
    using Utilities;

    /// <summary>
    /// Implements offer add-ons behavior.
    /// </summary>
    internal class OfferAddOnsOperations : BasePartnerComponent<Tuple<string, string>>, IOfferAddOns
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferAddOnsOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="offerId">The offer Id to get its add on offers.</param>
        /// <param name="country">The country on which to base the offer add-ons.</param>
        public OfferAddOnsOperations(IPartner rootPartnerOperations, string offerId, string country)
            : base(rootPartnerOperations, new Tuple<string, string>(offerId, country))
        {
            ParameterValidator.Required(offerId, "offerId must be set");
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <summary>
        /// Retrieves the add-ons for the given offer.
        /// </summary>
        /// <returns>The offer add-ons.</returns>
        public ResourceCollection<Offer> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Offer>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the add-ons for the given offer.
        /// </summary>
        /// <returns>The offer add-ons.</returns>
        public async Task<ResourceCollection<Offer>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ResourceCollection<Offer>, ResourceCollection<Offer>>(
               this.Partner,
               string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOfferAddons.Path, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
               PartnerService.Instance.Configuration.Apis.GetOfferAddons.Parameters.Country, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a subset of offers  for the provided country.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The maximum number of offers to return.</param>
        /// <returns>The requested segment of the offers for the provided country.</returns>
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

            var partnerServiceProxy = new PartnerServiceProxy<ResourceCollection<Offer>, ResourceCollection<Offer>>(
               this.Partner,
               string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOfferAddons.Path, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOfferAddons.Parameters.Country, this.Context.Item2));
            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOfferAddons.Parameters.Offset, offset.ToString()));
            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOfferAddons.Parameters.Size, size.ToString()));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
