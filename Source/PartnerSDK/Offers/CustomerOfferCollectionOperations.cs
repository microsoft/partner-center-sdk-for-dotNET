// -----------------------------------------------------------------------
// <copyright file="CustomerOfferCollectionOperations.cs" company="Microsoft Corporation">
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
    /// Customer Offer operations implementation class.
    /// </summary>
    internal class CustomerOfferCollectionOperations : BasePartnerComponent, ICustomerOfferCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOfferCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public CustomerOfferCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Gets the offers available to customer from partner.
        /// </summary>
        /// <returns>Offers available to customer from partner.</returns>
        public ResourceCollection<Offer> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Offer>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the offers available to customer from partner.
        /// </summary>
        /// <returns>Offers available to customer from partner.</returns>
        public async Task<ResourceCollection<Offer>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ResourceCollection<Offer>, ResourceCollection<Offer>>(
                                            this.Partner,
                                            string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerOffers.Path, this.Context));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Gets a segment of the offers available to customer from partner.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The desired segment size.</param>
        /// <returns>The required offers segment.</returns>
        public ResourceCollection<Offer> Get(int offset, int size)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Offer>>(() => this.GetAsync(offset, size));
        }

        /// <summary>
        /// Asynchronously gets a segment of the offers available to customer from partner.
        /// </summary>
        /// <param name="offset">The starting index.</param>
        /// <param name="size">The desired segment size.</param>
        /// <returns>The required offers segment.</returns>
        public async Task<ResourceCollection<Offer>> GetAsync(int offset, int size)
        {
            ParameterValidator.IsInclusive<int>(0, int.MaxValue, offset, "offset has to be non-negative.");
            ParameterValidator.IsInclusive<int>(1, int.MaxValue, size, "size has to be positive.");

            var partnerApiServiceProxy = new PartnerServiceProxy<ResourceCollection<Offer>, ResourceCollection<Offer>>(
            this.Partner,
            string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerOffers.Path, this.Context));

            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(PartnerService.Instance.Configuration.Apis.GetCustomerOffers.Parameters.Offset, offset.ToString()));
            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(PartnerService.Instance.Configuration.Apis.GetCustomerOffers.Parameters.Size, size.ToString()));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
