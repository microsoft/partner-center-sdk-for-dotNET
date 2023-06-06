// -----------------------------------------------------------------------
// <copyright file="CustomerOfferCategoryCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Offers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Offers;
    using Network;

    /// <summary>
    /// Customer Offer Category operations implementation class.
    /// </summary>
    internal class CustomerOfferCategoryCollectionOperations : BasePartnerComponent, ICustomerOfferCategoryCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOfferCategoryCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public CustomerOfferCategoryCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Gets the offer categories available to customer from partner.
        /// </summary>
        /// <returns>Offer categories available to customer from partner.</returns>
        public ResourceCollection<OfferCategory> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<OfferCategory>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the offer categories available to customer from partner.
        /// </summary>
        /// <returns>Offer categories available to customer from partner.</returns>
        public async Task<ResourceCollection<OfferCategory>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ResourceCollection<OfferCategory>, ResourceCollection<OfferCategory>>(
                                            this.Partner,
                                            string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerOfferCategories.Path, this.Context));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
