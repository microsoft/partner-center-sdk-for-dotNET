// -----------------------------------------------------------------------
// <copyright file="OfferOperations.cs" company="Microsoft Corporation">
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
    using Models.Offers;
    using Network;
    using Utilities;

    /// <summary>
    /// Single offer operations implementation.
    /// </summary>
    internal class OfferOperations : BasePartnerComponent<Tuple<string, string>>, IOffer
    {
        /// <summary>
        /// The offer add on operations.
        /// </summary>
        private Lazy<IOfferAddOns> addOns;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="offerId">The offer Id.</param>
        /// <param name="country">The country on which to base the offer.</param>
        public OfferOperations(IPartner rootPartnerOperations, string offerId, string country) :
            base(rootPartnerOperations, new Tuple<string, string>(offerId, country))
        {
            ParameterValidator.Required(offerId, "offerId has to be set.");
            ParameterValidator.ValidateCountryCode(country);

            this.addOns = new Lazy<IOfferAddOns>(() => new OfferAddOnsOperations(this.Partner, offerId, country));
        }

        /// <summary>
        /// Gets the operations for the current offer's add-ons.
        /// </summary>
        public IOfferAddOns AddOns
        {
            get
            {
                return this.addOns.Value;
            }
        }

        /// <summary>
        /// Retrieves the offer details.
        /// </summary>
        /// <returns>The offer details.</returns>
        public Offer Get()
        {
            return PartnerService.Instance.SynchronousExecute<Offer>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the offer details.
        /// </summary>
        /// <returns>The offer details.</returns>
        public async Task<Offer> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Offer, Offer>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOffer.Path, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetOffer.Parameters.Country, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
